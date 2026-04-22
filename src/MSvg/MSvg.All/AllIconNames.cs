
namespace MSvg.All;

public static class AllIconNames
{
    public static HashSet<string> IconNames { get; set; }
    private static Func<string, SvgIconGenerator.IconDto?>[] IconGenerator;
    static AllIconNames()
    {
        IconNames = [
            .. LucideIcons.IconNames, 
            .. BootStrapIcons.IconNames, 
            .. TailwindlabsHeroicons.IconNames,
            .. GlinckerTheSvg.IconNames
            ];
        IconGenerator =
        [
            LucideIcons.FromName,
            BootStrapIcons.FromName,
            TailwindlabsHeroicons.FromName,
            GlinckerTheSvg.FromName
        ];
    }
    public static IEnumerable<SvgIconGenerator.IconDto> GetFromName(string name)
    {
        foreach (var f in IconGenerator)
        {
            var data= f(name);
            if(data != null) yield return data;
        }

    }
    public static string[] MaybeIs(string name)
    {
        var distances = IconNames.ToDictionary(it => it, it => Fastenshtein.Levenshtein.Distance(name, it));
        var min  = distances.OrderBy(it=>it.Value).First().Value;
        return distances.Where(it => it.Value == min).Select(it => it.Key).ToArray();
    }
}
