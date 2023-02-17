using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace LambdaExpressions;

[TestClass]
public class LazyTests
{
    [TestMethod]
    public void InvokeLazy()
    {
        string text = "Very hard thing to create";
        Lazy<string> textFactory = new(
            () => text
            );
        Assert.AreEqual(text, textFactory.Value);
    }

    readonly int NonNullableNumber = 0;
    readonly int? NullableNumber = 0;
    [TestMethod]
    public void NullableDefaultsTo()
    {
        Assert.AreEqual<int?>(null, NullableNumber);
        Assert.AreEqual<int?>(0, NonNullableNumber);
    }
}
