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
        return new Backpack(name, price, weight, volume);
    }
}
