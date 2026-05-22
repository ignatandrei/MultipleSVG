using MSvg.All;

namespace MSvg.Tests;

[TestClass]
public sealed class TestNumberIcons
{
    [TestMethod]
    public void NumberIcons()
    {
        Assert.IsNotNull(BootStrapIcons.Activity);
        Assert.HasCount(1695, LucideIcons.IconNames);
        Assert.HasCount(2037, BootStrapIcons.IconNames);
        Assert.HasCount(316, TailwindlabsHeroicons.IconNames);
        Assert.HasCount(1170, GlinckerTheSvgIcons.IconNames);
        Assert.HasCount(628, Azure_Public_Service_Icons.IconNames);
        Assert.HasCount(16, Dynamics_365_App_Icons.IconNames);
        Assert.HasCount(954, Microsoft_365_content_icons.IconNames);
    }

    [DataTestMethod]
    [DataRow("MonitorPlay")]
    [DataRow("monitor-play")]
    public void FindIconFromName(string iconName)
    {
        var icons= AllIconNames.FromName(iconName).ToArray();
        Assert.IsGreaterThan(0, icons.Length);
        bool existLucide = icons.Any(it=>it.library == LucideIcons.NameLibrary);
        Assert.IsTrue(existLucide,$"{iconName} should be lucide icon ");

    }
}
