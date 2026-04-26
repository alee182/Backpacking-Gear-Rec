using System.Text.Json;

namespace DefaultNamespace;

public class BackpackFactory : GearFactory
{
    public BackpackFactory(string filePath) : base(filePath)
    {
    }

    public override Gear CreateGear(string name, JsonElement data)
    {
        string price = data.TryGetProperty("Price", out var p) ? p.GetString() ?? "" : "";
        string weight = data.TryGetProperty("Measured Weight", out var w) ? w.GetString() ?? "" : "";
        string volume = data.TryGetProperty("Advertised Volume", out var v) ? v.GetString() ?? "" : "";
        string rating = data.TryGetProperty("Overall Score", out var r) ? r.GetString() ?? "" : "";
        string? imagePath = data.TryGetProperty("image_path", out var img) ? img.GetString() : null;
        string synopsis = data.TryGetProperty("Bottom Line", out var bl) ? bl.GetString() ?? "" : "";
        string pros = data.TryGetProperty("Pros", out var pr) ? pr.GetString() ?? "" : "";
        string cons = data.TryGetProperty("Cons", out var co) ? co.GetString() ?? "" : "";
        return new Backpack(name, price, weight, volume, rating, imagePath, synopsis, pros, cons);
    }
}
