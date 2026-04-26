namespace DefaultNamespace;

public class Tent : Gear
{
    public string Comfort { get; set; }

    public Tent(string name, string price, string weight, string comfort, string rating, string? imagePath, string synopsis, string pros, string cons)
        : base(name, price, weight, "Tent", rating, imagePath, synopsis, pros, cons)
    {
        Comfort = comfort;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Tent: {Name} | Price: {Price} | Weight: {Weight}kg | Comfort: {Comfort}");
    }
}