namespace DefaultNamespace;

public class ServiceLayer
{
    public string UserStrategyChoice;
    public string UserGearChoice;
    public string UserExtraInfo;
    public ISortStrategy? SortStrategy;
    public GearManager GearManager;

    public ServiceLayer(string userGearChoice, string userExtraInfo, string userStrategyChoice)
    {
        UserStrategyChoice = userStrategyChoice;
        UserGearChoice = userGearChoice;
        UserExtraInfo = userExtraInfo;
        GearManager = new GearManager();
        SortStrategy = userStrategyChoice.ToLower() switch
        {
            "price"  => new PriceSortStrategy(),
            "weight" => new WeightSortStrategy(),
            "rating" => new RatingSortStrategy(),
            _ => throw new ArgumentException($"Unknown sort strategy: {userStrategyChoice}")
        };
    }

    public List<Gear> GetFilteredGear()
    {
        GearManager.LoadJson(UserGearChoice);
        var repo = GearRepository.GetInstance();
        var allGear = repo.GetAllForType(UserGearChoice);
        return SortStrategy!.SortItems(allGear);
    }
}