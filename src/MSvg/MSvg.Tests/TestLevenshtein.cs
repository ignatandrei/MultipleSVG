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
        Assert.IsTrue(similar.Select(it=>it.name).Contains("arrow-right"));

    }
    [TestMethod]
    public void Similar()
    {
        var name = "arrow-rigt";
        var similar = AllIconNames.MaybeIs(name);
        Assert.IsTrue(similar.Select(it => it.name).Contains("arrow-right"));
    }
}
