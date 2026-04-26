using System.Text.Json;

namespace DefaultNamespace;

public class TentFactory : GearFactory
{
    public TentFactory(string filePath) : base(filePath)
    {
    }

    public override Gear CreateGear(string name, JsonElement data)
    {
        string price = data.TryGetProperty("Price", out var p) ? p.GetString() ?? "" : "";
        string weight = data.TryGetProperty("Weight", out var w) ? w.GetString() ?? "" : "";
        string comfort = data.TryGetProperty("Comfort (25%)", out var c) ? c.GetString() ?? "" : "";
        string rating = data.TryGetProperty("Overall Score", out var r) ? r.GetString() ?? "" : "";
        string? imagePath = data.TryGetProperty("image_path", out var img) ? img.GetString() : null;
        string synopsis = data.TryGetProperty("Bottom Line", out var bl) ? bl.GetString() ?? "" : "";
        string pros = data.TryGetProperty("Pros", out var pr) ? pr.GetString() ?? "" : "";
        string cons = data.TryGetProperty("Cons", out var co) ? co.GetString() ?? "" : "";
        return new Tent(name, price, weight, comfort, rating, imagePath, synopsis, pros, cons);
    }
}
