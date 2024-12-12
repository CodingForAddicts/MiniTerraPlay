namespace ObelixAndCo.Cells;

public class Quarry : Cell
{
    public int AmountLeft;

    public Quarry()
    {
        AmountLeft = 20;
        _symbol = 'Q';
    }

    public void Extract()
    {
        if (AmountLeft == 0) throw new ArgumentException("There are no more stones...");
        AmountLeft -= 1;
    }
}
