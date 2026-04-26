using System.Text.RegularExpressions;

namespace DefaultNamespace;

public class WeightSortStrategy : ISortStrategy
{
    public List<Gear> SortItems(List<Gear> gearList)
    {
        var top8 = gearList
            .OrderBy(g => ParseWeightOz(g.Weight))
            .Take(8)
            .ToList();

        var repo = GearRepository.GetInstance();
        repo.ClearRepository();
        foreach (var gear in top8) repo.AddGear(gear);

        return top8;
    }

    private static double ParseWeightOz(string weight)
    {
        string lower = weight.ToLower().Trim();

        // Handle "X lb" or "X lbs"
        var lbMatch = Regex.Match(lower, @"([\d.]+)\s*lb");
        if (lbMatch.Success && double.TryParse(lbMatch.Groups[1].Value, out var lbs))
            return lbs * 16.0;

        // Handle "X oz"
        var ozMatch = Regex.Match(lower, @"([\d.]+)\s*oz");
        if (ozMatch.Success && double.TryParse(ozMatch.Groups[1].Value, out var oz))
            return oz;

        // Plain number fallback
        if (double.TryParse(lower, out var plain))
            return plain;

        return double.MaxValue;
    }
}