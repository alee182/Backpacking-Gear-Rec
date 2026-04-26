namespace DefaultNamespace;

public class ServiceLayer
{
    public string UserStrategyChoice;
    public string UserGearChoice;
    public string UserExtraInfo;
    public ISortStrategy? SortStrategy;
    public GearManager? GearManager;

    public ServiceLayer(string userStrategyChoice, string userGearChoice, string userExtraInfo)
    {
        UserStrategyChoice = userStrategyChoice;
        UserGearChoice = userGearChoice;
        UserExtraInfo = userExtraInfo;
    }
}