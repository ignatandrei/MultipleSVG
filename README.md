# MultipleSVG

`MSvg.All` is a .NET icon package that ships generated SVG icons from multiple sources:

- Lucide
- Bootstrap Icons
- Tailwind Heroicons
- glinckerthesvg

The package exposes icons as strongly typed `SvgIconGenerator.IconDto` values in the `MSvg.All` namespace.

## Install

```bash
dotnet add package MSvg.All
```

The current package targets **.NET 10**.

## Use a generated icon directly

Each icon set is exposed through a static class:

- `LucideIcons`
- `BootStrapIcons`
- `TailwindlabsHeroicons`
- `GlinckerTheSvg`

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
