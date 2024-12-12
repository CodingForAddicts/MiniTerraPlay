using ObelixAndCo.Cells;

namespace ObelixAndCo.People;

public class Person
{
    protected Cell Cell;

    public Person(Cell cell)
    {
        Cell = cell;
        Cell.IsOccupied = true;
    }
}