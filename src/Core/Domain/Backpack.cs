namespace DefaultNamespace;

public class Backpack : Gear
{
    public string Volume { get; set; }

    public Backpack(string name, string price, string weight, string volume)
        : base(name, price, weight, "Backpack")
    {
        Volume = volume;
    }
    
    public void DisplayInfo()
    {
        Console.WriteLine($"Backpack: {Name} | Price: {Price} | Weight: {Weight}kg | Size: {Volume}L");
    }
    
}