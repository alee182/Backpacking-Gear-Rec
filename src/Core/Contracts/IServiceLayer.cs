namespace DefaultNamespace;

public interface IServiceLayer
{
    List<Gear> GetFilteredGear();
    void ResetToDefault();
}