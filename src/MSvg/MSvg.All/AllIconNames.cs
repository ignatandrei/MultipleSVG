
using SvgIconGenerator;

namespace MSvg.All;

public sealed record IconLibraryDefinition(
        string Name,
        IReadOnlyList<string> IconNames,
        Func<string, IconDto?> FromName,
        Func<string , IEnumerable<(HowIsFound, string)>> MaybeIs,
        Func<string,string?> FromNameFile
   )
{
       public static readonly IconLibraryDefinition[] Libraries = [
         new (icons8_WPF_UI_Framework.NameLibrary,icons8_WPF_UI_Framework.IconNames,icons8_WPF_UI_Framework.FromName,icons8_WPF_UI_Framework.MaybeIs,icons8_WPF_UI_Framework.FromNameFileLookup),
         new (gravity_ui_icons.NameLibrary,gravity_ui_icons.IconNames,gravity_ui_icons.FromName,gravity_ui_icons.MaybeIs,gravity_ui_icons.FromNameFileLookup),
         new (phosphor_icons_core.NameLibrary,phosphor_icons_core.IconNames,phosphor_icons_core.FromName,phosphor_icons_core.MaybeIs,phosphor_icons_core.FromNameFileLookup),
         new (oclero_qlementine_icons.NameLibrary,oclero_qlementine_icons.IconNames,oclero_qlementine_icons.FromName,oclero_qlementine_icons.MaybeIs,oclero_qlementine_icons.FromNameFileLookup),
         new (icons8_windows_10_icons.NameLibrary,icons8_windows_10_icons.IconNames,icons8_windows_10_icons.FromName,icons8_windows_10_icons.MaybeIs,icons8_windows_10_icons.FromNameFileLookup),
         new (yessir_web_tech_ginetex_icons.NameLibrary,yessir_web_tech_ginetex_icons.IconNames,yessir_web_tech_ginetex_icons.FromName,yessir_web_tech_ginetex_icons.MaybeIs,yessir_web_tech_ginetex_icons.FromNameFileLookup),
         new (cugos_geoglyphs.NameLibrary,cugos_geoglyphs.IconNames,cugos_geoglyphs.FromName,cugos_geoglyphs.MaybeIs,cugos_geoglyphs.FromNameFileLookup),
         new (vscode_icons_vscode_icons.NameLibrary,vscode_icons_vscode_icons.IconNames,vscode_icons_vscode_icons.FromName,vscode_icons_vscode_icons.MaybeIs,vscode_icons_vscode_icons.FromNameFileLookup),
         new (basmilius_meteocons.NameLibrary,basmilius_meteocons.IconNames,basmilius_meteocons.FromName,basmilius_meteocons.MaybeIs,basmilius_meteocons.FromNameFileLookup),
         new (siemens_ix_icons.NameLibrary,siemens_ix_icons.IconNames,siemens_ix_icons.FromName,siemens_ix_icons.MaybeIs,siemens_ix_icons.FromNameFileLookup),
         new (resolvetosavelives_healthicons.NameLibrary,resolvetosavelives_healthicons.IconNames,resolvetosavelives_healthicons.FromName,resolvetosavelives_healthicons.MaybeIs,resolvetosavelives_healthicons.FromNameFileLookup),
         new (framework7io_framework7_icons.NameLibrary,framework7io_framework7_icons.IconNames,framework7io_framework7_icons.FromName,framework7io_framework7_icons.MaybeIs,framework7io_framework7_icons.FromNameFileLookup),
         new (microsoft_fluentui_emoji.NameLibrary,microsoft_fluentui_emoji.IconNames,microsoft_fluentui_emoji.FromName,microsoft_fluentui_emoji.MaybeIs,microsoft_fluentui_emoji.FromNameFileLookup),
         new (themesberg_flowbite_icons.NameLibrary,themesberg_flowbite_icons.IconNames,themesberg_flowbite_icons.FromName,themesberg_flowbite_icons.MaybeIs,themesberg_flowbite_icons.FromNameFileLookup),
         new (icons8_flat_Color_icons.NameLibrary,icons8_flat_Color_icons.IconNames,icons8_flat_Color_icons.FromName,icons8_flat_Color_icons.MaybeIs,icons8_flat_Color_icons.FromNameFileLookup),
         new (Yummygum_flagpack_core.NameLibrary,Yummygum_flagpack_core.IconNames,Yummygum_flagpack_core.FromName,Yummygum_flagpack_core.MaybeIs,Yummygum_flagpack_core.FromNameFileLookup),
         new (lipis_flag_icons.NameLibrary,lipis_flag_icons.IconNames,lipis_flag_icons.FromName,lipis_flag_icons.MaybeIs,lipis_flag_icons.FromNameFileLookup),
         new (feathericon_feathericon.NameLibrary,feathericon_feathericon.IconNames,feathericon_feathericon.FromName,feathericon_feathericon.MaybeIs,feathericon_feathericon.FromNameFileLookup),
        new(BootStrapIcons.NameLibrary, BootStrapIcons.IconNames, BootStrapIcons.FromName,BootStrapIcons.MaybeIs,BootStrapIcons.FromNameFileLookup),
        new(LucideIcons.NameLibrary, LucideIcons.IconNames, LucideIcons.FromName,LucideIcons.MaybeIs,LucideIcons.FromNameFileLookup),
        new(TailwindlabsHeroicons.NameLibrary, TailwindlabsHeroicons.IconNames, TailwindlabsHeroicons.FromName,TailwindlabsHeroicons.MaybeIs,TailwindlabsHeroicons.FromNameFileLookup),
        new(GlinckerTheSvgIcons.NameLibrary, GlinckerTheSvgIcons.IconNames, GlinckerTheSvgIcons.FromName,GlinckerTheSvgIcons.MaybeIs,GlinckerTheSvgIcons.FromNameFileLookup),
        new(Azure_Public_Service_Icons.NameLibrary,Azure_Public_Service_Icons.IconNames,Azure_Public_Service_Icons.FromName,Azure_Public_Service_Icons.MaybeIs,Azure_Public_Service_Icons.FromNameFileLookup),
        new(Dynamics_365_App_Icons.NameLibrary,Dynamics_365_App_Icons.IconNames,Dynamics_365_App_Icons.FromName,Dynamics_365_App_Icons.MaybeIs,Dynamics_365_App_Icons.FromNameFileLookup),
        new(Microsoft_365_content_icons.NameLibrary,Microsoft_365_content_icons.IconNames,Microsoft_365_content_icons.FromName,Microsoft_365_content_icons.MaybeIs,Microsoft_365_content_icons.FromNameFileLookup),
        new(IconNoir.NameLibrary,IconNoir.IconNames,IconNoir.FromName,IconNoir.MaybeIs,IconNoir.FromNameFileLookup),
        new(Leungwensen.NameLibrary,Leungwensen.IconNames,Leungwensen.FromName,Leungwensen.MaybeIs,Leungwensen.FromNameFileLookup),
        new (SustyIcons.NameLibrary,SustyIcons.IconNames,SustyIcons.FromName,SustyIcons.MaybeIs,SustyIcons.FromNameFileLookup),
        new (AndflettCascade.NameLibrary,AndflettCascade.IconNames,AndflettCascade.FromName,AndflettCascade.MaybeIs,AndflettCascade.FromNameFileLookup),
        new (artcoholic_akar_icons.NameLibrary,artcoholic_akar_icons.IconNames,artcoholic_akar_icons.FromName,artcoholic_akar_icons.MaybeIs,artcoholic_akar_icons.FromNameFileLookup),
        new (ant_design_icons.NameLibrary,ant_design_icons.IconNames,ant_design_icons.FromName,ant_design_icons.MaybeIs,ant_design_icons.FromNameFileLookup),
        new (cyberalien_line_md.NameLibrary,cyberalien_line_md.IconNames,cyberalien_line_md.FromName,cyberalien_line_md.MaybeIs,cyberalien_line_md.FromNameFileLookup),
        new (box_icons_boxicons_core.NameLibrary,box_icons_boxicons_core.IconNames,box_icons_boxicons_core.FromName,box_icons_boxicons_core.MaybeIs,box_icons_boxicons_core.FromNameFileLookup),
        new (danklammer_bytesize_icons.NameLibrary,danklammer_bytesize_icons.IconNames,danklammer_bytesize_icons.FromName,danklammer_bytesize_icons.MaybeIs,danklammer_bytesize_icons.FromNameFileLookup),
        new (catppuccin_vscode_icons.NameLibrary,catppuccin_vscode_icons.IconNames,catppuccin_vscode_icons.FromName,catppuccin_vscode_icons.MaybeIs,catppuccin_vscode_icons.FromNameFileLookup),
        new (jaynewey_charm_icons.NameLibrary,jaynewey_charm_icons.IconNames,jaynewey_charm_icons.FromName,jaynewey_charm_icons.MaybeIs,jaynewey_charm_icons.FromNameFileLookup),
        new (HatScripts_circle_flags.NameLibrary,HatScripts_circle_flags.IconNames,HatScripts_circle_flags.FromName,HatScripts_circle_flags.MaybeIs,HatScripts_circle_flags.FromNameFileLookup),
        new (vmware_archive_clarity_assets.NameLibrary,vmware_archive_clarity_assets.IconNames,vmware_archive_clarity_assets.FromName,vmware_archive_clarity_assets.MaybeIs,vmware_archive_clarity_assets.FromNameFileLookup),
        new (codex_teamicons.NameLibrary,codex_teamicons.IconNames,codex_teamicons.FromName,codex_teamicons.MaybeIs,codex_teamicons.FromNameFileLookup),
        new (devicons_devicon.NameLibrary,devicons_devicon.IconNames,devicons_devicon.FromName,devicons_devicon.MaybeIs,devicons_devicon.FromNameFileLookup),
        new (fernandcf_duo_icons.NameLibrary,fernandcf_duo_icons.IconNames,fernandcf_duo_icons.FromName,fernandcf_duo_icons.MaybeIs,fernandcf_duo_icons.FromNameFileLookup),
        new (element_plus_element_plus_icons.NameLibrary,element_plus_element_plus_icons.IconNames,element_plus_element_plus_icons.FromName,element_plus_element_plus_icons.MaybeIs,element_plus_element_plus_icons.FromNameFileLookup),
        new (SUSE_UIUX_eos_icons.NameLibrary,SUSE_UIUX_eos_icons.IconNames,SUSE_UIUX_eos_icons.FromName,SUSE_UIUX_eos_icons.MaybeIs,SUSE_UIUX_eos_icons.FromNameFileLookup),
        new (akveo_eva_icons.NameLibrary,akveo_eva_icons.IconNames,akveo_eva_icons.FromName,akveo_eva_icons.MaybeIs,akveo_eva_icons.FromNameFileLookup),
        new (familyjs_famicons.NameLibrary,familyjs_famicons.IconNames,familyjs_famicons.FromName,familyjs_famicons.MaybeIs,familyjs_famicons.FromNameFileLookup),
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
        return new IconFrom(nameLibrary, icon, HowIsFound.ExactStringInKey);
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
    public static IEnumerable<(string nameLibrary, HowIsFound found,string name)> MaybeIs(string name)
    {
        foreach(var f in IconLibraryDefinition.Libraries)
        {
            foreach(var item in f.MaybeIs(name))
            {
                yield return (f.Name, item.Item1,item.Item2);
            }
        }
        
    }
}
