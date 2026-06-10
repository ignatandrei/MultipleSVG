
# MultipleSVG

`MSvg.All` is a .NET icon package that ships generated 23,071  SVG icons from multiple sources:


        akveo_eva_icons - number icons (244 icons)
    
        AndflettCascade - number icons (127 icons)
    
        ant_design_icons - number icons (447 icons)
    
        artcoholic_akar_icons - number icons (454 icons)
    
        Azure_Public_Service_Icons - number icons (628 icons)
    
        Bootstrap - number icons (2,037 icons)
    
        box_icons_boxicons_core - number icons (2,179 icons)
    
        catppuccin_vscode_icons - number icons (655 icons)
    
        codex_teamicons - number icons (78 icons)
    
        cyberalien_line_md - number icons (1,222 icons)
    
        danklammer_bytesize_icons - number icons (101 icons)
    
        devicons_devicon - number icons (1,881 icons)
    
        Dynamics_365_App_Icons - number icons (16 icons)
    
        element_plus_element_plus_icons - number icons (293 icons)
    
        familyjs_famicons - number icons (1,342 icons)
    
        feathericon_feathericon - number icons (255 icons)
    
        fernandcf_duo_icons - number icons (91 icons)
    
        glinckerthesvg - number icons (1,170 icons)
    
        HatScripts_circle_flags - number icons (417 icons)
    
        IconNoir - number icons (1,383 icons)
    
        icons8_flat_Color_icons - number icons (329 icons)
    
        jaynewey_charm_icons - number icons (261 icons)
    
        Leungwensen - number icons (369 icons)
    
        lipis_flag_icons - number icons (271 icons)
    
        Lucide - number icons (1,695 icons)
    
        Microsoft_365_content_icons - number icons (954 icons)
    
        SUSE_UIUX_eos_icons - number icons (1,967 icons)
    
        SustyIcons - number icons (530 icons)
    
        TailwindlabsHeroicons - number icons (316 icons)
    
        vmware_archive_clarity_assets - number icons (1,105 icons)
    
        Yummygum_flagpack_core - number icons (254 icons)
    
The package exposes icons as strongly typed `SvgIconGenerator.IconDto` values in the `MSvg.All` namespace.

See in action at https://ignatandrei.github.io/MultipleSVG/icons    

## Install

```bash
dotnet add package MSvg.All
```

The current package targets **.NET 10**.

## Blazor package

`MSvg.Blazor` is a separate Razor Class Library package that builds on `MSvg.All` and provides:

- `SvgIconPreview` for rendering `IconDto` values
- a packaged `/icons` browser page that can be added to a Blazor app through `Router.AdditionalAssemblies`

## Use a generated icon directly

Each icon set is exposed through a static class:


        - `akveo_eva_icons`
    
        - `AndflettCascade`
    
        - `ant_design_icons`
    
        - `artcoholic_akar_icons`
    
        - `Azure_Public_Service_Icons`
    
        - `Bootstrap`
    
        - `box_icons_boxicons_core`
    
        - `catppuccin_vscode_icons`
    
        - `codex_teamicons`
    
        - `cyberalien_line_md`
    
        - `danklammer_bytesize_icons`
    
        - `devicons_devicon`
    
        - `Dynamics_365_App_Icons`
    
        - `element_plus_element_plus_icons`
    
        - `familyjs_famicons`
    
        - `feathericon_feathericon`
    
        - `fernandcf_duo_icons`
    
        - `glinckerthesvg`
    
        - `HatScripts_circle_flags`
    
        - `IconNoir`
    
        - `icons8_flat_Color_icons`
    
        - `jaynewey_charm_icons`
    
        - `Leungwensen`
    
        - `lipis_flag_icons`
    
        - `Lucide`
    
        - `Microsoft_365_content_icons`
    
        - `SUSE_UIUX_eos_icons`
    
        - `SustyIcons`
    
        - `TailwindlabsHeroicons`
    
        - `vmware_archive_clarity_assets`
    
        - `Yummygum_flagpack_core`
    
    
Example:

```csharp
using MSvg.All;

var icon = LucideIcons.Activity;

Console.WriteLine(icon.Name);           // activity
Console.WriteLine(icon.InnerContent);   // SVG inner markup
```

## Resolve an icon by name

Each icon set also supports lookup by name:

```csharp
using MSvg.All;

var icon1 = LucideIcons.FromName("activity");
var icon2 = LucideIcons.FromName("Activity");
```

To search across all packaged icon sets:

```csharp
using MSvg.All;

var matches = AllIconNames.GetFromName("activity");
var suggestions = AllIconNames.MaybeIs("activty");
```

## Render an `IconDto` as SVG

`IconDto` contains:

- `Name`
- `DefaultAttributes`
- `InnerContent`

You can turn it into an `<svg>` string like this:

```csharp
using MSvg.All;

var icon = LucideIcons.Activity;
var attributes = string.Join(
    " ",
    icon.DefaultAttributes.Select(kvp => $"{kvp.Key}=\"{kvp.Value}\""));

var svg = $"<svg {attributes}>{icon.InnerContent}</svg>";
```

## Browse available names

Each source exposes its available icon names:

```csharp
using MSvg.All;

var lucideNames = LucideIcons.IconNames;
var bootstrapNames = BootStrapIcons.IconNames;
var heroiconsNames = TailwindlabsHeroicons.IconNames;
var allNames = AllIconNames.IconNames;
```
