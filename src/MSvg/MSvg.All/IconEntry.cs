using SvgIconGenerator;

namespace MSvg.All;

public sealed record IconEntry(
    string Library,
    IconDto Icon,
    HashSet<string> SearchKeys,
    string NormalizedName)
{
    public void AddSearchKeys(string key)
    {
        if(string.IsNullOrWhiteSpace(key)) return;
        SearchKeys.Add(key);
        SearchKeys.Add(key.NormalizeMe());

    }
    public bool Contains(string name)
    {
        if(SearchKeys == null)   return false;
        if (SearchKeys.Count == 0) return false;
        return SearchKeys.Any(k => k.Contains(name, StringComparison.OrdinalIgnoreCase));
    }
    public int Distance(string name)
    {
        if (SearchKeys == null) return int.MaxValue;
        if (SearchKeys.Count == 0) return int.MaxValue;
        return SearchKeys.Min(k => Fastenshtein.Levenshtein.Distance(name, k));
    }   
}
