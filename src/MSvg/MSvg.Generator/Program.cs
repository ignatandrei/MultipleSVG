string folderRoot= Environment.CurrentDirectory;
while (!string.IsNullOrEmpty(folderRoot))
{
    string file = Path.Combine(folderRoot, "MSvg.slnx");
    if(File.Exists(file))
    {
        Console.WriteLine($"Found solution file at {file}");
        break;
    }
    folderRoot = Directory.GetParent(folderRoot)?.FullName;
}
Console.WriteLine($"Using solution root {folderRoot}");
while (true)
{
Console.WriteLine("Enter name:");
string name = Console.ReadLine();
if(name.Contains("https://github.com"))
{
    name = name.Replace("https://github.com/", "");
    name = name.Replace("/", "_");
    name= name.Replace("-", "_");
    if(name.EndsWith("_"))
    {
        name = name.Substring(0, name.Length - 1);
    }
}
//create icons folder if it doesn't exist
var newFolder = Path.Combine(folderRoot, "MSvg.All", "Icons", name);
if(Directory.Exists(newFolder))
{
    Console.WriteLine($"Directory {newFolder} already exists. Exiting.");
    return;
}

Directory.CreateDirectory(newFolder);
var readmeFile = Path.Combine(newFolder, "README.md");
File.WriteAllText(readmeFile, """
From  , when  License was

# The MIT License (MIT)

""");
//create .cs file
var csFile = Path.Combine(folderRoot, "MSvg.All", $"{name}.cs");
File.WriteAllText(csFile, $"""
namespace MSvg.All;

[SvgIconGenerator.GenerateIcons("**/{name}/*.svg")]
[RSCG_TemplatingCommon.IGenerateDataFromAdditionalFiles("IconLookupShared")]
public static partial class {name};
""");

//modify MSvg.All.csproj
var csprojFile = Path.Combine(folderRoot, "MSvg.All", "MSvg.All.csproj");
var csprojLines = File.ReadAllLines(csprojFile).ToList();
int insertIndex = -1;
foreach (var line in csprojLines.Index())
{
    //Console.WriteLine(line.Item);
    if(line.Item.Contains("AdditionalFiles Include="))
    {
        insertIndex = line.Index;
        //Console.WriteLine("Found AdditionalFiles Include");
        break;
    }
}
if(insertIndex == -1)
{
    Console.WriteLine("Could not find AdditionalFiles Include in MSvg.All.csproj. Exiting.");
    return;
}
csprojLines.Insert(insertIndex, $@"    <AdditionalFiles Include=""Icons/{name}/*.svg"" />");
File.WriteAllLines(csprojFile, csprojLines);
//modify MSvg.All/AllIconNames.cs
var allIconNamesFile = Path.Combine(folderRoot, "MSvg.All", "AllIconNames.cs");
var allIconNamesLines = File.ReadAllLines(allIconNamesFile).ToList();
int insertIndex2 = -1;
foreach(var line in allIconNamesLines.Index())
{
    if (line.Item.Contains("public static readonly IconLibraryDefinition[] Libraries"))
    {
        insertIndex2 = line.Index;
        break;
    }
}
if (insertIndex2 == -1)
{
    Console.WriteLine("Could not find Libraries definition in AllIconNames.cs. Exiting.");
    return;
}
allIconNamesLines.Insert(insertIndex2 + 1, $@"         new ({name}.NameLibrary,{name}.IconNames,{name}.FromName,{name}.MaybeIs,{name}.FromNameFileLookup),");
File.WriteAllLines(allIconNamesFile, allIconNamesLines);
}