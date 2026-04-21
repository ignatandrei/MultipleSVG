using MSvg.All;

namespace MSvg.Tests;

[TestClass]
public sealed class TestNumberIcons
{
    [TestMethod]
    public void NumberIcons()
    {
        var x = LucideIcons.IconNames.Length;
        Assert.AreEqual(1695, x);
    }
}
