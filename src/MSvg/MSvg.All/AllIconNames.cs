
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
        new(GlinckerTheSvgIcons.NameLibrary, GlinckerTheSvgIcons.IconNames, GlinckerTheSvgIcons.FromName,GlinckerTheSvgIcons.MaybeIs),
        new(Azure_Public_Service_Icons.NameLibrary,Azure_Public_Service_Icons.IconNames,Azure_Public_Service_Icons.FromName,Azure_Public_Service_Icons.MaybeIs)
    ];
}

public static class AllIconNames
{
    public static HashSet<string> IconNames { get; set; }
    private static Func<string, IconFrom?>[] IconGenerator;
    static AllIconNames()
    {
        IconNames = IconLibraryDefinition.Libraries.SelectMany(l => l.IconNames).ToHashSet();
        IconGenerator =
            IconLibraryDefinition.Libraries
            .Select(l => new Func<string, IconFrom?>(x => FromNameLibrary(l.Name, x, l.FromName)))
            .ToArray();
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
        foreach(var f in IconLibraryDefinition.Libraries)
        {
            foreach(var item in f.MaybeIs(name))
            {
                yield return (f.Name, item);
            }
        }
        
    }
}
