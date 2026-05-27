using System;
using System.Collections.Generic;
using System.Text;

namespace Msvg.ModifyReadMe.Template;

internal class ReadmeTemplate
{
    public KeyValuePair<string, int>[] LibrariesWithCount;
    public int TotalIcons => LibrariesWithCount.Sum(kv => kv.Value);
    public ReadmeTemplate()
    {
        LibrariesWithCount= [.. MSvg.All.IconLibraryDefinition.Libraries
            .Select(l => new KeyValuePair<string, int>(l.Name, l.IconNames.Count))
            .OrderBy(it=>it.Key)];

    }

}
