namespace ObelixAndCo;

public class Utils
{
	public static int LevenshteinDistance(string a, string b)
	{
		var al = a.Length;
		var bl = b.Length;
		
		var matrix = new int[al + 1, bl + 1];
		
		if (al == 0)
			return bl;
		if (bl == 0)
			return al;
		
		for (var i = 0; i <= al; i++)
			matrix[i, 0] = i;
		for (var j = 0; j <= bl; j++)
			matrix[0, j] = j;
		
		for (var i = 1; i <= al; i++)
		{
			for (var j = 1; j <= bl; j++)
			{
				var changes = (a[i - 1] == b[j - 1]) ? 0 : 1;
				
				matrix[i, j] = Math.Min(
					Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1),
					matrix[i - 1, j - 1] + changes);
			}
		}
		
		return matrix[al, bl];
		
	}

	public static string FindClosest(string input)
	{
		var options = new[] { "quarry", "forest", "pond", "hut", "fisher", "hunter", "sculptor" };
		string best = "";
		foreach (var option in options)
		{
			if (LevenshteinDistance(option, input) < LevenshteinDistance(best, input)) best = option;
		}
		
		return best;
	}


}