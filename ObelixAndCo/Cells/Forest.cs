namespace ObelixAndCo.Cells;

public class Forest : Cell
{
    public double Density { get; set; }

    public Forest()
    {
        Density = 1.0;
        _symbol = 'F';
    }

    public void DecreaseDensity()
    {
        Density = Math.Max(0, Density - 0.05);
    }
}


