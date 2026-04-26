namespace DefaultNamespace;

public class Tent : Gear
{
    public string Comfort { get; set; }

    public Tent(string name, string price, string weight, string comfort)
        : base(name, price, weight, "Tent")
    {
        Comfort = comfort;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Tent: {Name} | Price: {Price} | Weight: {Weight}kg | Comfort: {Comfort}");
    }
}