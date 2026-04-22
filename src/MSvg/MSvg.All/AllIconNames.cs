
namespace MSvg.All;

public static class AllIconNames
{
    public static HashSet<string> IconNames { get; set; }
    private static Func<string, SvgIconGenerator.IconDto?>[] IconGenerator;
    static AllIconNames()
    {
        IconNames = [.. LucideIcons.IconNames, .. BootStrapIcons.IconNames, .. TailwindlabsHeroicons.IconNames];
        IconGenerator =
        [
            LucideIcons.FromName,
            BootStrapIcons.FromName,
            TailwindlabsHeroicons.FromName
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
}
