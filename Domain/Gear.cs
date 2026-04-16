namespace DefaultNamespace;

public abstract class Gear
{
    public String Name { get; set; }
    public double Price { get; set; }
    public double Weight { get; set; }
    public TypeEnum Type { get; set; }
    public string Link { get; set; }
    
    protected Gear(string name, double price, double weight, GearType type, string link)
    {
        Name = name;
        Price = price;
        Weight = weight;
        Type = type;
        Link = link;
    }
    
}