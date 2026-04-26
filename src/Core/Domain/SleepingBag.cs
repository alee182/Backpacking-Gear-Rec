namespace DefaultNamespace;

public class SleepingBag : Gear
{
    public string Warmth { get; set; }

    public SleepingBag(string name, string price, string weight, string warmth, string rating, string? imagePath, string synopsis, string pros, string cons)
        : base(name, price, weight, "SleepingBag", rating, imagePath, synopsis, pros, cons)
    {
        Warmth = warmth;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Sleeping Bag: {Name} | Price: {Price} | Weight: {Weight}kg | Warmth: {Warmth}");
    }
}