using ObelixAndCo.Cells;

namespace ObelixAndCo.People;

public class Fisher : Person
{
    private Random _random;

    public Fisher(Cell cell) : base(cell)
    {
        if (cell is not Pond pond) throw new ArgumentException("The cell was not a pond");
        
        _random = new Random(pond.Seed);
    }

    public int Fish()
    {
        return _random.Next(0, 3);
    }
}