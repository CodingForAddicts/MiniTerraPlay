using System.Text;
using ObelixAndCo.Cells;
using ObelixAndCo.People;

namespace ObelixAndCo;

public class Grid
{
    public Cell[][] Cells;
    public int Wallet;
    public int Food;
    public int Menhirs;
    private int _objective;
    private int _currentTurn;
    public List<Person> People;
    private RandomPrice _randomPrice;

    public Grid(int rows, int cols, int objective)
    {
        Cells = new Cell[rows][];
        for (int i = 0; i < rows; i++)
        {
            Cells[i] = new Cell[cols];
            for (int j = 0; j < cols; j++)
            {
                Cells[i][j] = new Cell();
            }
        }
        
        Wallet = 0;
        Food = 0;
        Menhirs = 0;
        _objective = objective;
        _currentTurn = 0;
        People = new List<Person>();
        _randomPrice = new RandomPrice(10, 2);
    }
    
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0 ; i < Cells[0].Length + 2; i++)
        {
            sb.Append("-");
        }

        sb.AppendLine();
    
        for (int i = 0; i < Cells.Length; i++)
        {
            sb.Append("|");
            for (int j = 0; j < Cells[i].Length; j++)
            {
                Cell cell = Cells[i][j];
                sb.Append(cell.Symbol);
            }
            sb.AppendLine("|");
        }

        for (int i = 0 ; i < Cells[0].Length + 2; i++)
        {
            sb.Append("-");
        }

        sb.AppendLine();
        sb.AppendLine($"Money: {Wallet} Sestertius");
        sb.AppendLine($"Menhirs: {Menhirs}");
        sb.AppendLine($"Food: {Food}");
        sb.AppendLine($"Turn: {_currentTurn}");

        return sb.ToString();
    }
    
    public Hut? FindFreeHut()
    {
        for (int i = 0; i < Cells.Length; i++)
        {
            Cell[] row = Cells[i];
            for (int j = 0; j < row.Length; j++)
            {
                Cell value = row[j];
                if (value is Hut hut && !hut.IsOccupied)
                {
                    return hut;
                }
            }
        }
        return null; 
    }

    public bool AddPerson(Person person)
    {
        Hut? hut = FindFreeHut();
        if (hut == null) return false;
        People.Add(person);
        hut.AddWorker();
        return true;
    }

    public void NextTurn()
    {
        foreach (var person in People)
        {
            if (person is Hunter hunter) if (hunter.Hunt()) Food++;
            if (person is Sculptor sculptor) if (sculptor.Sculpt()) Menhirs++;
            if (person is Fisher fisher) Food += fisher.Fish();
        }

        _currentTurn += 1;
    }

    private bool ItemExists(string item)
    {
        bool exists = true;
        switch (item)
        {
            case "forest" :
                break;
            case "hut" :
                break;
            case "pond" :
                break;
            case "quarry" :
                break;
            case "fisher" :
                break;
            case "hunter" :
                break;
            case "sculptor" :
                break;
            default:
                exists = false;
                break;
        }

        return exists;
    }
    
    public bool IsCoordInBounds(int row, int col)
    {
        if (row < 0 || row >= Cells.Length)
            return false;
        
        if (col < 0 || col >= Cells[row].Length)
            return false;

        return true;
    }

    public bool Buy(int x, int y, string item)
    {
        if (!ItemExists(item)) return false;

        if (!IsCoordInBounds(x, y)) return false;

        if (item == "hunter" && Cells[x][y] is not Forest ) return false;
        if (item == "fisher" && Cells[x][y] is not Pond ) return false;
        if (item == "sculptor" && Cells[x][y] is not Quarry ) return false;
        
        switch (item)
        {
            case "forest" :
                Cells[x][y] = new Forest();
                break;
            case "hut" :
                Cells[x][y] = new Hut();
                break;
            case "pond" :
                Cells[x][y] = new Pond(x,y);
                break;
            case "quarry" :
                Cells[x][y] = new Quarry();
                break;
            case "fisher" :
                Fisher fisher = new Fisher(Cells[x][y]);
                AddPerson(fisher);
                break;
            case "hunter" :
                Hunter hunter = new Hunter(Cells[x][y]);
                AddPerson(hunter);
                break;
            case "sculptor" :
                Sculptor sculptor = new Sculptor(Cells[x][y]);
                AddPerson(sculptor);
                break;
        }

        return true;
    }

    public bool Sell(int menhirs)
    {
        if (menhirs > Menhirs) return false;
        Wallet += menhirs * _randomPrice.GetMenhirPrice(_currentTurn);
        Menhirs -= menhirs;
        return true;

    }

    public string ShowMenu()
    {
        return "buy/sell/next/exit";
    }

    public string DisplayEnd()
    {
        if (Wallet >= _objective)
        {
            return "WIN";
        } else
        {
            return "GAME OVER";
        }
    }

    public void HandleBuy(IoManager ioManager)
    {
        string? xcoord = null;
        string? ycoord = null;
        bool incorrectCoords = true;
        ioManager.WriteLine("What do you want to buy?");
        string? itemToBuy = ioManager.GetNextInput();
        if (itemToBuy == null || !ItemExists(itemToBuy))
        {
            string closestMatch = Utils.FindClosest(itemToBuy ?? "");
            ioManager.WriteLine($"Invalid item, did you mean '{closestMatch}'?");
            return;
        }
        ioManager.WriteLine("Where do you want to place it?");
        while (incorrectCoords)
        {
            ioManager.WriteLine("X-Coordinate:");
            xcoord = ioManager.GetNextInput();
            ioManager.WriteLine("Y-Coordinate:");
            ycoord = ioManager.GetNextInput();

            if (xcoord == null || ycoord == null)
            {
                ioManager.WriteLine("Please enter valid coordinates.");
                continue;
            }
            
            try
            {
                if (IsCoordInBounds(int.Parse(xcoord), int.Parse(ycoord)))
                {
                    incorrectCoords = false ;
                    break;
                }
            }
            catch
            {
                continue;
            }
            
            ioManager.WriteLine("Please enter valid coordinates.");
        }

        if (xcoord != null)
            if (ycoord != null)
                if (Buy(int.Parse(xcoord), int.Parse(ycoord), itemToBuy))
                {
                    ioManager.WriteLine("Successfully bought");
                    Wallet -= 5;
                }
                else
                {
                    ioManager.WriteLine("Failed to buy");
                }
    }

    public void HandleSell(IoManager ioManager)
    {
        ioManager.WriteLine("How many menhirs should be sold?");
        string? number = ioManager.GetNextInput();
        int parsedNum = 0;
        bool isNum = true;
        try
        {
            if (number != null) parsedNum = int.Parse(number);
            if (number == null) isNum = false;
        }
        catch
        {
            isNum = false;
        }
        if (isNum)
            if (Sell(parsedNum))
            {
                ioManager.WriteLine("Successfully sold");
            }
            else
            {
                ioManager.WriteLine( "Failed to sell");
            }
        else
        {
            ioManager.WriteLine("Please input a valid number");
        }
    }

    public int HandleInput(IoManager ioManager)
    {
        string? inputCommand = ioManager.GetNextInput();
        int toReturn = 0;
        switch (inputCommand)
        {
            case "buy":
                HandleBuy(ioManager);
                break;
            case "sell":
                HandleSell(ioManager);
                break;
            case "next":
                toReturn = 2;
                break;
            case "exit":
                toReturn = 1;
                break;
        }

        return toReturn;
    }
    
    

    public static void Play(int rows, int cols, int turns, string inputFile, string outputFile, int objective = 100)
    {
        // Step 1 :  Check dimensions
        if (rows < 4 || cols < 4) throw new ArgumentException("There is not enough space to play");
        
        // Step 2 :  Initialization of the starting map
        Grid grid = new Grid(rows, cols, objective);
        grid.Cells[0][0] = new Hut();
        grid.Cells[0][cols-1] = new Pond(0,cols-1);
        grid.Cells[rows-1][cols-1] = new Forest();
        grid.Cells[rows-1][0] = new Quarry();
        grid.Wallet = 50;

        IoManager ioManager = new IoManager(inputFile, outputFile);
        
        // Step 3 : Main game loop
        for (int i = 0; i < turns; i++)
        {
            
            ioManager.WriteLine(grid.ToString());
            ioManager.WriteLine(grid.ShowMenu());
            
            int processInput = 0;
            while (processInput == 0)
            {
                 processInput = grid.HandleInput(ioManager);
            }
           
            if (processInput == 1) break;
            grid.NextTurn();

        }

        // STep 4
        ioManager.WriteLine(grid.DisplayEnd());
        
        ioManager.Close();


    }

    public int CountLakes()
    {
        throw new NotImplementedException();
    }

    
}