# MSvg.Blazor

`MSvg.Blazor` packages reusable Blazor UI for `MSvg.All`.

It currently includes:

- `SvgIconPreview` for rendering an `SvgIconGenerator.IconDto`
- `Pages/Icons.razor` for browsing all packaged icon libraries

## Install

```bash
dotnet add package MSvg.Blazor
```

## Use in a Blazor WebAssembly app

Add the namespace in your app's `_Imports.razor` if you want to use the preview component directly:

```razor
@using MSvg.Blazor.Components
```

To expose the packaged `/icons` page, include the assembly in your router:

```razor
<Router AppAssembly="@typeof(App).Assembly"
        AdditionalAssemblies="new[] { typeof(MSvg.Blazor.Pages.Icons).Assembly }">
    ...
</Router>
```
