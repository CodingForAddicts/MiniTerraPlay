namespace ObelixAndCo;

public class RandomPrice
{
	private int _basePrice;
	private double _inflationRate;
	private Random _random;

	public RandomPrice(int basePrice, double inflationRate)
	{
		_basePrice = basePrice;
		_inflationRate = inflationRate;
		_random = new Random(12345);
	}

	public int GetMenhirPrice(int currentTurn)
	{
		double price = (_basePrice * (1+ _inflationRate * currentTurn));
		double randomFactor = price * (_random.NextDouble() * 0.1 - 0.05);
		return (int)(price + randomFactor);
	}
	
	
}