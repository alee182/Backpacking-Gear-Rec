public class ServiceLayer : IServiceLayer
{
    public string UserStrategyChoice;

    public string UserGearChoice;

    public string UserExtraInfo;

    public ISortStrategy SortStrategy;

    public IGearManager GearManager;

    public IRepository gearRepositoryInstance;


    public void resetParams()
    {
        
    }

    public Dictionary<string> LLMGeneration()
    {
        
    }

    public Dictionary<string> outputCleaning()
    {
        
    }

    public ServiceLayer(string userStrategyChoice, string userGearChoice, string userExtraInfo)
    {
        UserStrategyChoice = userStrategyChoice;
        UserGearChoice = userGearChoice;
        UserExtraInfo = userExtraInfo;
    }
}