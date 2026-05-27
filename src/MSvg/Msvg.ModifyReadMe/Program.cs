using Msvg.ModifyReadMe.Template;

Console.WriteLine("Writing README.md...");
var templateModel = new ReadmeTemplate();
var template = new Readme(templateModel);

var result = template.Render();
var curdir= Directory.GetCurrentDirectory();
while(!File.Exists(Path.Combine(curdir, "README.md")))
{
    var parent = Directory.GetParent(curdir);
    if (parent == null)
    {
        Console.WriteLine("README.md not found in any parent directory.");
        return;
    }
    curdir = parent.FullName;
}
var f= Path.Combine(curdir, "README.md");
File.WriteAllText(f, result);
