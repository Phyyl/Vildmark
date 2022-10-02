using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Xml.Serialization;

namespace Vildmark.Resources;

public abstract class ResourceLoadContext
{
    public abstract Stream GetStream(string name);

    public TResource Load<TResource>(string name) => ResourceLoader.Load<TResource>(name, this);

    public TResource Load<TResource, TOptions>(string name, TOptions options) => ResourceLoader.Load<TResource, TOptions>(name, options, this);

    public byte[] LoadAllBytes(string name) => GetStream(name).ReadAllBytes();

    public string LoadAllText(string name) => GetStream(name).ReadAllText();

    public string[] LoadAllLines(string name) => GetStream(name).ReadAllLines();

    public TResource LoadJson<TResource>(string name) => JsonSerializer.Deserialize<TResource>(LoadAllText(name)) ?? throw new SerializationException($@"Could not deserialize object of type {typeof(TResource).Name} from json resource ""{name}""");

    public TResource LoadXml<TResource>(string name) where TResource : class => new XmlSerializer(typeof(TResource)).Deserialize(GetStream(name)) as TResource ?? throw new SerializationException($@"Could not deserialize object of type {typeof(TResource).Name} from xml resource ""{name}""");
}

public class EmbeddedResourceLoadContext : ResourceLoadContext
{
    public Assembly Assembly { get; }

    public EmbeddedResourceLoadContext(Assembly assembly)
    {
        Assembly = assembly;
    }

    public override Stream GetStream(string name)
    {
        return EmbeddedResources.GetEmbeddedStream(name, Assembly);
    }
}

public class FileResourceLoadContext : ResourceLoadContext
{
    public string RootPath { get; }

    public FileResourceLoadContext(string? rootPath = default)
    {
        RootPath = rootPath ?? Directory.GetCurrentDirectory();
    }

    public override Stream GetStream(string name)
    {
        return File.OpenRead(Path.Combine(RootPath, name));
    }
}

