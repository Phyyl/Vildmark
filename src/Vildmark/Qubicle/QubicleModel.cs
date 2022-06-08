using Vildmark.Resources;

namespace Vildmark.Qubicle;

[ResourceLoader(typeof(QubicleModelResourceLoader))]
public class QubicleModel
{
    private readonly Dictionary<string, QubicleMatrix> matrices;

    public IEnumerable<QubicleMatrix> Matrices => matrices.Values.ToArray();
    public QubicleMatrix? this[string name] => matrices.GetValueOrDefault(name);

    public QubicleModel(IEnumerable<QubicleMatrix> matrices)
    {
        this.matrices = matrices.ToDictionary(m => m.Name);
    }
}
