namespace DefaultNamespace;

public class GearRepository
{
    private static List<Gear> _singletonGearList = new List<Gear>();
    private static GearRepository? _instance = null;

    // Private constructor to enforce Singleton
    private GearRepository(List<Gear> gearList)
    {
        _singletonGearList = gearList;
    }
    
    public static GearRepository GetInstance()
    {
        if (_instance == null)
        {
            _instance = new GearRepository(new List<Gear>());
        }
        return _instance;
    }
    
    public void AddGear(Gear gear)
    {
        _singletonGearList.Add(gear);
    }

    public bool RemoveGear(Gear gear)
    {
        if (_singletonGearList.Contains(gear))
        {
            _singletonGearList.Remove(gear);
            return true;
        }
        return false;
    }
    
    public List<Gear> GetAllForType(string type)
    {
        //Incomplete
        throw new NotImplementedException();
    }
    
    public bool ClearRepository()
    {
        _singletonGearList.Clear();
        return true;
    }

}