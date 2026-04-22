using MSvg.All;

namespace MSvg.Tests;

[TestClass]
public sealed class TestLevenshtein
{
    [TestMethod]
    public void Exact()
    {
        var name = "arrow-right";
        var similar = AllIconNames.MaybeIs(name);
        Assert.IsTrue(similar.Contains("arrow-right"));

    }
    [TestMethod]
    public void Similar()
    {
        var name = "azure";
        var similar = AllIconNames.MaybeIs(name);
        Assert.IsGreaterThan(0, similar.Length);
    }
}
