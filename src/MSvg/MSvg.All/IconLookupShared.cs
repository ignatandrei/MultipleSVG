namespace MSvg.All;
public enum HowIsFound
{
    None =0 ,
    ExactStringInKey,
    ExactStringInAlias,
    KeyStartWith,
    AliasStartWith,
    ContainsInKey,
    ContainsInAlias,
    Levenshtein,

}
public static partial class IconLookupShared
{
    
    public static IEnumerable<(HowIsFound,string)> MaybeIs(string name, Dictionary<string, string[]> aliases)
    {
        foreach (var kvp in aliases)
        {
            if(string.Equals(kvp.Key, name, StringComparison.InvariantCultureIgnoreCase))
                yield return (HowIsFound.ExactStringInKey, kvp.Key);

            if(kvp.Value.Any(alias => string.Equals(alias, name, StringComparison.InvariantCultureIgnoreCase)))
                yield return (HowIsFound.ExactStringInAlias, kvp.Key);
            
            if (kvp.Key.StartsWith(name, StringComparison.InvariantCultureIgnoreCase))
                yield return (HowIsFound.KeyStartWith, kvp.Key);

            if(kvp.Value.Any(alias => alias.StartsWith(name, StringComparison.InvariantCultureIgnoreCase)))
                yield return (HowIsFound.AliasStartWith, kvp.Key);

            if (kvp.Key.Contains(name,StringComparison.InvariantCultureIgnoreCase))
                yield return (HowIsFound.ContainsInKey, kvp.Key);
            
            if(kvp.Value.Any(alias => alias.Contains(name,StringComparison.InvariantCultureIgnoreCase)))
                yield return (HowIsFound.ContainsInAlias, kvp.Key);

        }
        var allnames = aliases.Keys.ToArray();
        allnames = allnames.Union(aliases.Values.SelectMany(x => x).ToArray()).ToArray();
        allnames = allnames.Distinct().ToArray();
        foreach (var item in MaybeIsLevel(name, allnames))
        {
            yield return item;
        }    
    }
    private static (HowIsFound,string)[] MaybeIsLevel(string name, string[] iconNames)
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

        return matches.Select(match => (HowIsFound.Levenshtein, match)).ToArray();
    }

    public static global::SvgIconGenerator.IconDto? FromName(
        string name,
        IReadOnlyDictionary<string, global::SvgIconGenerator.IconDto?> iconsByName)
    {
        
        return iconsByName.TryGetValue(name, out var icon) ? icon : null;
    }
}

