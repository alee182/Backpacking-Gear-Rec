using System.Text.Json;

namespace DefaultNamespace;

public class GearManager : IGearManager
{
    private static string DbPath(string file) =>
        Path.Combine(AppContext.BaseDirectory, "Gear-Database", file);

    public List<GearFactory> Factories { get; set; } = new List<GearFactory>
    {
        new BackpackFactory(Path.Combine(AppContext.BaseDirectory, "Gear-Database", "backpacks.json")),
        new TentFactory(Path.Combine(AppContext.BaseDirectory, "Gear-Database", "tents.json")),
        new SleepingBagFactory(Path.Combine(AppContext.BaseDirectory, "Gear-Database", "sleepingbag.json"))
    };

    public void LoadJson(string userGearChoice)
    {
        GearFactory factory = userGearChoice.ToLower() switch
        {
            "backpack"    => Factories[0],
            "tent"        => Factories[1],
            "sleepingbag" => Factories[2],
            _ => throw new ArgumentException($"Unknown gear type: {userGearChoice}")
        };

        string jsonText = File.ReadAllText(factory.FilePath);
        using JsonDocument doc = JsonDocument.Parse(jsonText);

        GearRepository repo = GearRepository.GetInstance();
        repo.ClearRepository();

        foreach (JsonElement page in doc.RootElement.EnumerateArray())
        {
            JsonElement products = page.GetProperty("products");
            foreach (JsonProperty product in products.EnumerateObject())
            {
                Gear gear = factory.CreateGear(product.Name, product.Value);
                repo.AddGear(gear);
            }
        }
    }
}
