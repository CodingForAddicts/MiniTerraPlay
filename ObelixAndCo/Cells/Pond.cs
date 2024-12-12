namespace ObelixAndCo.Cells;

public class Pond : Cell
{
    public int Seed { get; }

    public Pond(int x, int y)
    {
        _symbol = 'P';
        Seed = 5 * x + 5000 * y;
    }
}
