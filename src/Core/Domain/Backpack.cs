namespace DefaultNamespace;

public class Backpack : Gear
{
    public string Volume { get; set; }

    public Backpack(string name, string price, string weight, string volume, string rating, string? imagePath, string synopsis, string pros, string cons)
        : base(name, price, weight, "Backpack", rating, imagePath, synopsis, pros, cons)
    {
        Volume = volume;
    }
    
    public void DisplayInfo()
    {
        Console.WriteLine($"Backpack: {Name} | Price: {Price} | Weight: {Weight}kg | Size: {Volume}L");
    }
    
}