namespace ObelixAndCo.Cells;

public class Cell
{
	public bool IsOccupied;
	protected char _symbol;
	public char Symbol
	{
		get
		{
			if (!IsOccupied) return char.ToLower(_symbol);
			else return char.ToUpper(_symbol);
		}
	}
	
	public Cell()
	{
		IsOccupied = false;
		_symbol = ' ';
	}
}