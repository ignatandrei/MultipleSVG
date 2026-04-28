
using SvgIconGenerator;

namespace MSvg.All;

public sealed record IconLibraryDefinition(
        string Name,
        IReadOnlyList<string> IconNames,
        Func<string, IconDto?> FromName,
        Func<string ,IEnumerable<string>> MaybeIs
        )
{
    public static readonly IconLibraryDefinition[] Libraries = [
        new(BootStrapIcons.NameLibrary, BootStrapIcons.IconNames, BootStrapIcons.FromName,BootStrapIcons.MaybeIs),
        new(LucideIcons.NameLibrary, LucideIcons.IconNames, LucideIcons.FromName,LucideIcons.MaybeIs),
        new(TailwindlabsHeroicons.NameLibrary, TailwindlabsHeroicons.IconNames, TailwindlabsHeroicons.FromName,TailwindlabsHeroicons.MaybeIs),
        new(GlinckerTheSvgIcons.NameLibrary, GlinckerTheSvgIcons.IconNames, GlinckerTheSvgIcons.FromName,GlinckerTheSvgIcons.MaybeIs)
    ];
}

public static class AllIconNames
{
    public static HashSet<string> IconNames { get; set; }
    private static Func<string, IconFrom?>[] IconGenerator;
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
            x=>FromNameLibrary(LucideIcons.NameLibrary, x, LucideIcons.FromName),
            x=>FromNameLibrary(BootStrapIcons.NameLibrary, x, BootStrapIcons.FromName),
            x=>FromNameLibrary(TailwindlabsHeroicons.NameLibrary, x, TailwindlabsHeroicons.FromName),
            x=>FromNameLibrary(GlinckerTheSvgIcons.NameLibrary, x, GlinckerTheSvgIcons.FromName)
        ];
    }
    private static IconFrom? FromNameLibrary(string nameLibrary ,string name, Func<string, IconDto?> generator)
    {
        var icon = generator(name);
        if(icon == null) return null;
        return new IconFrom(nameLibrary, icon);
    }
    public static IEnumerable<IconFrom> FromName(string name)
    {
        foreach (var f in IconGenerator)
        {
            var data= f(name);
            if(data != null) yield return data;
        }

    }
    public static IEnumerable<(string nameLibrary,string name)> MaybeIs(string name)
    {
        foreach(var item in LucideIcons.MaybeIs(name))
        {
            yield return (LucideIcons.NameLibrary, item);
        }
        foreach (var item in BootStrapIcons.MaybeIs(name))
        {
            yield return (BootStrapIcons.NameLibrary,item);
        }
        foreach (var item in TailwindlabsHeroicons.MaybeIs(name))
        {
            yield return (TailwindlabsHeroicons.NameLibrary,item);
        }
        foreach (var item in GlinckerTheSvgIcons.MaybeIs(name))
        {
            yield return (GlinckerTheSvgIcons.NameLibrary,item);
        }
    }
}
