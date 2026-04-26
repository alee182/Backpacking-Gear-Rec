using System.Text.Json;

namespace DefaultNamespace;

public abstract class GearFactory
{
    public string FilePath { get; set; }

    protected GearFactory(string filePath)
    {
        FilePath = filePath;
    }

    public abstract Gear CreateGear(string name, JsonElement data);
}
