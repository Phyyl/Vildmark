using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Vildmark.Graphics.FontGenerator
{
    public class GenerateFont : Microsoft.Build.Utilities.Task
    {
        private string fontFolder = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);

        private string msbuildThisFileDirectory;
        private string generatorPath;

        [Required]
        public ITaskItem[] Input { get; set; }

        [Output]
        public ITaskItem[] Output { get; set; }

        public override bool Execute()
        {
            msbuildThisFileDirectory = Path.GetDirectoryName(BuildEngine.ProjectFileOfTaskNode);
            generatorPath = Path.Combine(msbuildThisFileDirectory, GetFontGeneratorPath());

            Output = ProcessItems().ToArray();

            foreach (var item in Output)
            {
                Log.LogMessage(MessageImportance.High, $"Output: {item.ItemSpec}, {item.GetMetadata("LogicalName")}");
            }

            return true;
        }

        private IEnumerable<ITaskItem> ProcessItems()
        {
            foreach (var item in Input)
            {
                if (TryGetFontPath(item, out string path))
                {
                    Log.LogMessage($@"Found font ""{item.ItemSpec}"": {path}");

                    if (TryProcessFont(item, path, out ITaskItem[] outputs))
                    {
                        foreach (var output in outputs)
                        {
                            yield return output;
                        }
                    }
                }
                else
                {
                    Log.LogError($@"Could not find font called ""{item.ItemSpec}""");
                }
            }
        }

        private bool TryProcessFont(ITaskItem item, string path, out ITaskItem[] outputs)
        {
            try
            {
                string outputDirectory = Path.Combine(Path.GetTempPath(), "Vildmark", "FontGenerator", Guid.NewGuid().ToString());
                string outputPath = Path.Combine(outputDirectory, item.GetMetadata("FileName"));

                Directory.CreateDirectory(outputDirectory);

                Process process = Process.Start(new ProcessStartInfo
                {
                    FileName = generatorPath,
                    Arguments = $@"--font-file ""{path}"" --output {outputPath} --chars 0-255",
                    RedirectStandardOutput = true
                });

                process.WaitForExit();

                outputs = Directory.GetFiles(outputDirectory).Select(f=> CreateOutputTaskItem(item, f)).ToArray();
            }
            catch (Exception ex)
            {
                outputs = null;

                Log.LogError($@"Failed to process font: {ex.Message}");
            }

            return outputs?.Length > 0;
        }

        private ITaskItem CreateOutputTaskItem(ITaskItem item, string path)
        {
            List<string> logicalNameParts = new List<string>();
            string recursiveDir = item.GetMetadata("RelativeDir");

            if (!string.IsNullOrWhiteSpace(recursiveDir))
            {
                logicalNameParts.Add(recursiveDir.Trim('\\','/').Replace("\\", ".").Replace("/", "."));
            }

            string fileName = Path.GetFileName(path);

            logicalNameParts.Add(fileName);

            string logicalName = string.Join(".", logicalNameParts);

            TaskItem result = new TaskItem(fileName);

            result.SetMetadata("LogicalName", logicalName);

            return result;
        }

        private bool TryGetFontPath(ITaskItem item, out string path)
        {
            return File.Exists(path = item.GetMetadata("FullPath")) ||
                File.Exists(path = Path.Combine(fontFolder, item.ItemSpec)) ||
                File.Exists(path = Path.Combine(fontFolder, item.ItemSpec + ".ttf")) ||
                File.Exists(path = Path.Combine(fontFolder, item.ItemSpec + ".otf"));
        }

        private string GetFontGeneratorPath()
        {
            //if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            //{
            //    return "macos/fontbm.exe";
            //}

            //if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            //{
            //    return "linux";
            //}

            return "tools/windows/fontbm.exe";
        }
    }
}
