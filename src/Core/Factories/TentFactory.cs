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
        return new Tent(name, price, weight, comfort);
    }
}
