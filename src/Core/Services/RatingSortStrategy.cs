namespace DefaultNamespace;

public class RatingSortStrategy : ISortStrategy
{
    public List<Gear> SortItems(List<Gear> gearList)
    {
        var top8 = gearList
            .OrderByDescending(g => ParseRating(g.Rating))
            .Take(8)
            .ToList();

        var repo = GearRepository.GetInstance();
        repo.ClearRepository();
        foreach (var gear in top8) repo.AddGear(gear);

        return top8;
    }

    private static int ParseRating(string rating)
    {
        return int.TryParse(rating.Trim(), out var result) ? result : 0;
    }
}