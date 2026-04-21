namespace DefaultNamespace;

public class SleepingBag : Gear
{
    public string Warmth { get; set; }

    public SleepingBag(string name, double price, string link, double weight, string warmth)
        : base(name, price, weight, TypeEnum.SleepingBag, link)
    {
        Warmth = warmth;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Sleeping Bag: {Name} | Price: {Price} | Weight: {Weight}kg | Warmth: {Warmth} | Link: {Link}");
    }
}