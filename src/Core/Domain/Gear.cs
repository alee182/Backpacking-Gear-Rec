namespace DefaultNamespace;

public abstract class Gear
{
    public string Name { get; set; }
    public string Price { get; set; }
    public string Weight { get; set; }
    public string Type { get; set; }
    public string Rating { get; set; }
    public string? ImagePath { get; set; }
    public string Synopsis { get; set; }
    public string Pros { get; set; }
    public string Cons { get; set; }

    protected Gear(string name, string price, string weight, string type, string rating, string? imagePath, string synopsis, string pros, string cons)
    {
        Name = name;
        Price = price;
        Weight = weight;
        Type = type;
        Rating = rating;
        ImagePath = imagePath;
        Synopsis = synopsis;
        Pros = pros;
        Cons = cons;
    }
}