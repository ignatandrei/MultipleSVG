namespace MSvg.All;

internal static partial class IconLookupShared
{
    public static IEnumerable<string> MaybeIs(string name, Dictionary<string, string[]> aliases)
    {
        foreach (var kvp in aliases)
        {
            if(kvp.Key.Contains(name,StringComparison.InvariantCultureIgnoreCase))
                yield return kvp.Key;
            if(kvp.Value.Any(alias => alias.Contains(name,StringComparison.InvariantCultureIgnoreCase)))
                yield return kvp.Key;
        }
        foreach(var item in MaybeIsLevel(name, aliases.Keys.ToArray()))
        {
            yield return item;
        }    
    }
    private static string[] MaybeIsLevel(string name, string[] iconNames)
    {
        var min = int.MaxValue;
        var matches = new List<string>();

        foreach (var iconName in iconNames)
        {
            var distance = global::Fastenshtein.Levenshtein.Distance(name, iconName);

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

        return [.. matches];
    }

    public static global::SvgIconGenerator.IconDto? FromName(
        string name,
        IReadOnlyDictionary<string, global::SvgIconGenerator.IconDto?> iconsByName)
    {
        return iconsByName.TryGetValue(name, out var icon) ? icon : null;
    }
}

