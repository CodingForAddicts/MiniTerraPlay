namespace ObelixAndCo.Cells;

public class Hut : Cell
{
    private int _nbWorkers;

    public Hut()
    {
        _symbol = 'H';
        _nbWorkers = 0;
    }

    public void AddWorker()
    {
        if (IsOccupied) throw new ArgumentException("The hut is already full...");
        _nbWorkers += 1;
        if (_nbWorkers == 5) IsOccupied = true;
    }
    
}
