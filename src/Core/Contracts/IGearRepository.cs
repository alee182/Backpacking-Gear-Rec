namespace DefaultNamespace;

public interface IGearRepository
{
    void AddGear(Gear gear) ;
    bool RemoveGear(Gear gear);
    List<Gear> GetAllForType(string type);
    bool ClearRepository();
}