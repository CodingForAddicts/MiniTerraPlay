using ObelixAndCo.Cells;

namespace ObelixAndCo.People;

public class Hunter : Person
{
    public Hunter(Cell cell) : base(cell)
    {
        if (cell is not Forest) throw new ArgumentException("The cell was not a Forest");
    }

    public bool Hunt()
    {
        if (Cell is Forest forest && forest.Density > 0)
        {
            forest.DecreaseDensity();
            return true;
        }

        return false;
    }
}