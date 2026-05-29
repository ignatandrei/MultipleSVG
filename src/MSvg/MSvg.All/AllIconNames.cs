
using SvgIconGenerator;

namespace MSvg.All;

public sealed record IconLibraryDefinition(
        string Name,
        IReadOnlyList<string> IconNames,
        Func<string, IconDto?> FromName,
        Func<string ,IEnumerable<string>> MaybeIs,
        Func<string,string?> FromNameFile
        )
{
    public static readonly IconLibraryDefinition[] Libraries = [
        new(BootStrapIcons.NameLibrary, BootStrapIcons.IconNames, BootStrapIcons.FromName,BootStrapIcons.MaybeIs,BootStrapIcons.FromNameFileLookup),
        new(LucideIcons.NameLibrary, LucideIcons.IconNames, LucideIcons.FromName,LucideIcons.MaybeIs,LucideIcons.FromNameFileLookup),
        new(TailwindlabsHeroicons.NameLibrary, TailwindlabsHeroicons.IconNames, TailwindlabsHeroicons.FromName,TailwindlabsHeroicons.MaybeIs,TailwindlabsHeroicons.FromNameFileLookup),
        new(GlinckerTheSvgIcons.NameLibrary, GlinckerTheSvgIcons.IconNames, GlinckerTheSvgIcons.FromName,GlinckerTheSvgIcons.MaybeIs,GlinckerTheSvgIcons.FromNameFileLookup),
        new(Azure_Public_Service_Icons.NameLibrary,Azure_Public_Service_Icons.IconNames,Azure_Public_Service_Icons.FromName,Azure_Public_Service_Icons.MaybeIs,Azure_Public_Service_Icons.FromNameFileLookup),
        new(Dynamics_365_App_Icons.NameLibrary,Dynamics_365_App_Icons.IconNames,Dynamics_365_App_Icons.FromName,Dynamics_365_App_Icons.MaybeIs,Dynamics_365_App_Icons.FromNameFileLookup),
        new(Microsoft_365_content_icons.NameLibrary,Microsoft_365_content_icons.IconNames,Microsoft_365_content_icons.FromName,Microsoft_365_content_icons.MaybeIs,Microsoft_365_content_icons.FromNameFileLookup),
        new(IconNoir.NameLibrary,IconNoir.IconNames,IconNoir.FromName,IconNoir.MaybeIs,IconNoir.FromNameFileLookup),
        new(Leungwensen.NameLibrary,Leungwensen.IconNames,Leungwensen.FromName,Leungwensen.MaybeIs,Leungwensen.FromNameFileLookup),
        new (SustyIcons.NameLibrary,SustyIcons.IconNames,SustyIcons.FromName,SustyIcons.MaybeIs,SustyIcons.FromNameFileLookup   ),
        new (AndflettCascade.NameLibrary,AndflettCascade.IconNames,AndflettCascade.FromName,AndflettCascade.MaybeIs,AndflettCascade.FromNameFileLookup   ),
        new (artcoholic_akar_icons.NameLibrary,artcoholic_akar_icons.IconNames,artcoholic_akar_icons.FromName,artcoholic_akar_icons.MaybeIs,artcoholic_akar_icons.FromNameFileLookup   ),
        new (ant_design_icons.NameLibrary,ant_design_icons.IconNames,ant_design_icons.FromName,ant_design_icons.MaybeIs,ant_design_icons.FromNameFileLookup   ),


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
    public static string? FromNameFileLookup(string library,string? name)
    {
        if(name==null) return null;
        var f = IconLibraryDefinition.Libraries.FirstOrDefault(l => l.Name == library);
        if (f == null) return null;
        return f.FromNameFile(name);
    }
    public static IEnumerable<string> FromNameFileLookup(string name)
    {
        foreach (var f in IconLibraryDefinition.Libraries)
        {
            var file = f.FromNameFile(name);
            if (file != null) yield return file;
        }
    }
     private static IconFrom? FromNameLibrary(string nameLibrary ,string name, Func<string, IconDto?> generator)
    {
        var icon = generator(name);
        if(icon == null) return null;
        return new IconFrom(nameLibrary, icon);
    }
    public static IconFrom? FromNameAndLibrary(string library, string name)
    {
        foreach (var f in IconGenerator)
        {
            var data = f(name);
            if (data != null && data.library == library)
            {
                return data;
            }
        }

        return null;
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
