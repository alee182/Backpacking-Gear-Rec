namespace DefaultNamespace;

public class Backpack : Gear
{
    public int Volume { get; set; }

    public Backpack(string name, double price, double weight, string link, int volume)
        : base(name, price, weight, GearType.Backpack, link)
    {
        Volume = volume;
    }
    
    public void DisplayInfo()
    {
        Console.WriteLine($"Backpack: {Name} | Price: {Price} | Weight: {Weight}kg | Size: {Size}L | Link: {Link}");
    }
    
}