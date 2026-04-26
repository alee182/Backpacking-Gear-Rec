namespace DefaultNamespace;

public abstract class Gear
{
    public string Name { get; set; }
    public string Price { get; set; }
    public string Weight { get; set; }
    public string Type { get; set; }
    protected Gear(string name, string price, string weight, string type)
    {
        Name = name;
        Price = price;
        Weight = weight;
        Type = type;
    }
    
}