namespace ObelixAndCo;

public class IoManager
{
	public bool IsFileInput;
	public bool IsFileOutput;
	public Queue<string> InputsQueue = new Queue<string>();
	public StreamWriter OutputFile;
    
	public IoManager(string inputFile, string outputFile)
	{
		IsFileInput = true;
		IsFileOutput = true;
        
		if (inputFile == "")
		{
			IsFileInput = false;
		}
        
		if (outputFile == "")
		{
			IsFileOutput = false;
			OutputFile = new StreamWriter(Console.OpenStandardOutput());
		}
		else
			OutputFile = new StreamWriter(outputFile);

		if (IsFileInput)
		{
			string[] lines = File.ReadAllLines(inputFile);
			foreach (string line in lines)
			{
				foreach (string c in line.Split(" "))
				{
					InputsQueue.Enqueue(c);
				}
			}
		}
	}
    
	public string? GetNextInput()
	{
		if (!IsFileInput)
		{
			return Console.ReadLine();
		}
		if (InputsQueue.Count == 0)
		{
			return "exit";
		}
		return InputsQueue.Dequeue();
	}
    
	public void WriteLine(string line)
	{
		OutputFile.WriteLine(line);
		OutputFile.Flush();
	}
    
	public void Write(string text)
	{
		OutputFile.Write(text);
		OutputFile.Flush();
	}
    
	public void Close()
	{
		OutputFile.Close();
	}
}
