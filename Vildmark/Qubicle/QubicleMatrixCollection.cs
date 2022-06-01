using Vildmark.Resources;

namespace Vildmark.Qubicle;

public class QubicleMatrixCollection : IResource<QubicleMatrixCollection>
{
    public static IResourceLoader<QubicleMatrixCollection> Loader { get; } = new QubicleMatrixResourceLoader();

    private readonly Dictionary<string, QubicleMatrix> matrices;

		public IEnumerable<QubicleMatrix> Matrices => matrices.Values.ToArray();
    public QubicleMatrix? this[string name] => matrices.GetValueOrDefault(name);

		public QubicleMatrixCollection(IEnumerable<QubicleMatrix> matrices)
		{
			this.matrices = matrices.ToDictionary(m => m.Name);
		}
	}
