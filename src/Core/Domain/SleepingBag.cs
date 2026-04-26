namespace DefaultNamespace;

public class SleepingBag : Gear
{
    public string Warmth { get; set; }

    public SleepingBag(string name, string price, string weight, string warmth)
        : base(name, price, weight, "SleepingBag")
    {
        Warmth = warmth;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Sleeping Bag: {Name} | Price: {Price} | Weight: {Weight}kg | Warmth: {Warmth}");
    }
}