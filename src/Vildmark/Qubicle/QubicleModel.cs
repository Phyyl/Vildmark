using Vildmark.Resources;

namespace Vildmark.Qubicle;

[ResourceLoader(typeof(QubicleModelResourceLoader))]
public class QubicleModel(IEnumerable<QubicleMatrix> matrices)
{
    private readonly Dictionary<string, QubicleMatrix> matrices = matrices.ToDictionary(m => m.Name);

    public IEnumerable<QubicleMatrix> Matrices => [.. matrices.Values];
    public QubicleMatrix? this[string name] => matrices.GetValueOrDefault(name);
}
