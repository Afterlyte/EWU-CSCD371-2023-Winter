using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace LambdaExpressions;

[TestClass]
public class Lambdas
{
    public static bool AlphabeticalGreaterThan(string a, string b)
    {
        return a.CompareTo(b) > 0;
    }

    void DoSomething(string text)
    {
        Console.WriteLine(text);
    }

    public static string[] Sort(Func<string, string, bool> greaterThan, string[] items)
    {
        return null!;
    }

    public static void ForEach(Action<string> action, string[] items)
    {
        foreach(string item in items)
        {
            action(item);
        }
    }

    [TestMethod]
    public void Sort_GivenAB_Success()
    {
        string[] items = new[] { "a", "b" };
        string[] sorted = Sort(AlphabeticalGreaterThan, items);
        ForEach(DoSomething, sorted);
    }

    [TestMethod]
    public void ForEach_GivenAB_Success()
    {
        string[] items = new[] { "a", "b" };
        ForEach(DoSomething, items);
    }

    [TestMethod]
    public void ForEach_LambdaExpression_Success()
    {
        string[] items = new[] { "a", "b" };
        string sentence = "This is a really long string.";

        Action<string> action = DoSomething;
        action = text =>
        {
            Console.WriteLine(text);
            Console.WriteLine(sentence);
        };
        Action takeAction = () => Console.WriteLine(sentence);

        ForEach(DoSomething, items);
    }

    object GetThing() => new();
    string GetText() => "InigoMontoya";

    static T Calculate<T>(Func<T> getData) => getData();

    [TestMethod]
    public void MyTestMethod()
    {
        _ = Calculate(GetThing);
        _ = Calculate(GetText);
    }
}
