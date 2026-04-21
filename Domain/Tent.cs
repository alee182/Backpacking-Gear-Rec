namespace DefaultNamespace;

public class Tent : Gear
{
    public double Comfort { get; set; }

    public Tent(string name, double price, string link, double weight, double comfort)
        : base(name, price, weight, TypeEnum.Tent, link)
    {
        Comfort = comfort;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Tent: {Name} | Price: {Price} | Weight: {Weight}kg | Comfort: {Comfort} | Link: {Link}");
    }
}