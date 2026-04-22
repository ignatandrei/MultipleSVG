using MSvg.All;

namespace MSvg.Tests;

[TestClass]
public sealed class TestNumberIcons
{
    [TestMethod]
    public void NumberIcons()
    {
        Assert.HasCount(1695, LucideIcons.IconNames);
        Assert.HasCount(2037, BootStrapIcons.IconNames);
        Assert.HasCount(316, TailwindlabsHeroicons.IconNames);
        Assert.HasCount(1170, GlinckerTheSvg.IconNames);

    }
}
