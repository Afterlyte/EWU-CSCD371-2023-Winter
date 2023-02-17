using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace LambdaExpressions;

[TestClass]
public class Lambdas
{
    private readonly bool text = false;

    public bool AlphabeticalGreaterThan(string a, int b)
    {
        return a.CompareTo(b) > 0;
    }

    public bool AlphabeticalGreaterThan(string a, string b)
    {
        return a.CompareTo(b) > 0;
    }

    public string[] Sort(Func<string,string, bool> greaterThan, string[] items )
    {
        return null!;
    }



    [TestMethod]
    public void Sort_AB_Sucess()
    {
        string[] items = new[] { "A", "B" };
        string[] sortedItems = Sort(AlphabeticalGreaterThan, items);
        foreach (string item in sortedItems)
        {
            Console.WriteLine(item);
        }
    }

    public void Foreach(Action<string> action, string[] items)
    {
        foreach (string item in items)
        {
            action(item);
        }
    }

    [TestMethod]
    public void ForEach_AB_Sucess()
    {
        string[] items = new[] { "A", "B" };
        Foreach(DoSomethingString, items);
    }

    void DoSomethingString(string text) => Console.WriteLine(text);
    void DoSomethingObject(object thing) => Console.WriteLine(text);
    void DoSomethingInt(int thing) => Console.WriteLine(text);

    [TestMethod]
    public void ForEach_LambdaExpresion_Sucess()
    {
        string[] items = new[] { "A", "B" };
        string sentence = "This is a really long string.";
        Action<string> action = DoSomethingString;
        action = (string text) => Console.WriteLine(text);
        action = (text) => Console.WriteLine(text);
        action = text => Console.WriteLine(text);
        action = Console.WriteLine;
        action = text =>
        {
            Console.WriteLine(text);
            Console.WriteLine(sentence);
        };
        Action takeAction = () => Console.WriteLine(sentence);
        takeAction = Console.WriteLine;

        Foreach(DoSomethingString, items);
        Foreach(DoSomethingObject, items);
        // Foreach(DoSomethingInt, items);
    }

    
    object GetThing(string thing) => thing;
    string? GetText(object text) => text.ToString();
    
    string? Calculate(Func<object, string?> getData) => getData(null!);

    [TestMethod]
    public void MyTestMethod()
    {
        Func<object, string?> func;
        func = GetText;
            
    }

    

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void ExceptionHandling()
    {
        try
        {

        }
        catch (Exception)
        {

            throw;
        }
        
    }
}
