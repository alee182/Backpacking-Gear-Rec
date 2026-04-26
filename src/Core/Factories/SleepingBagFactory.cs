using System.Text.Json;

namespace DefaultNamespace;

public class SleepingBagFactory : GearFactory
{
    public SleepingBagFactory(string filePath) : base(filePath)
    {
    }

    public override Gear CreateGear(string name, JsonElement data)
    {
        string price = data.TryGetProperty("Price", out var p) ? p.GetString() ?? "" : "";
        string weight = data.TryGetProperty("Weight", out var w) ? w.GetString() ?? "" : "";
        string warmth = data.TryGetProperty("recommended_temp_limit", out var t) ? t.GetString() ?? "" : "";
        return new SleepingBag(name, price, weight, warmth);
    }
}
