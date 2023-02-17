using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LambdaExpressions;
[TestClass]
public class PatternMatchingTests
{
    [TestMethod]
    public void SetName_InigoMontoya_Success()
    {
        FullName testName = new(fullName: "Inigo Montoya");
        var actual = (testName.FirstName, testName.LastName);
        Assert.AreEqual<(string, string)>(("Inigo", "Montoya"), (actual.FirstName, actual.LastName));
    }

    [TestMethod]
    public void UsingSwitch_InitialsInigoMontoya_Success()
    {
        FullName testName = new(fullName: "Inigo Montoya");
        string actual = testName.Initials;
        Assert.AreEqual<string>("IM", actual);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Create_EmptyStrings_Fails()
    {
        _ = new FullName("");
    }

}