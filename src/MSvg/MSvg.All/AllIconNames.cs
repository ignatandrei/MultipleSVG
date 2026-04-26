
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
            .. GlinckerTheSvgIcons.IconNames
            ];
        IconGenerator =
        [
            LucideIcons.FromName,
            BootStrapIcons.FromName,
            TailwindlabsHeroicons.FromName,
            GlinckerTheSvgIcons.FromName
        ];
    }
    public static IEnumerable<SvgIconGenerator.IconDto> FromName(string name)
    {
        foreach (var f in IconGenerator)
        {
            var data= f(name);
            if(data != null) yield return data;
        }

    }
    public static string[] MaybeIs(string name)
    {
        if (IconNames.Count == 0)
        {
            return Array.Empty<string>();
        }
        var min = int.MaxValue;
        var matches = new List<string>();

        foreach (var iconName in IconNames)
        {
            var distance = Fastenshtein.Levenshtein.Distance(name, iconName);

            if (distance < min)
            {
                min = distance;
                matches.Clear();
                matches.Add(iconName);
            }
            else if (distance == min)
            {
                matches.Add(iconName);
            }
        }

        return matches.ToArray();
    }
}
