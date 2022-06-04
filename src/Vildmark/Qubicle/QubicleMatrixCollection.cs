using Vildmark.Resources;

namespace Vildmark.Qubicle;

[ResourceLoader(typeof(QubicleMatrixCollection))]
public class QubicleMatrixCollection
{
    private readonly Dictionary<string, QubicleMatrix> matrices;

    public IEnumerable<QubicleMatrix> Matrices => matrices.Values.ToArray();
    public QubicleMatrix? this[string name] => matrices.GetValueOrDefault(name);

    public QubicleMatrixCollection(IEnumerable<QubicleMatrix> matrices)
    {
        this.matrices = matrices.ToDictionary(m => m.Name);
    }
}
