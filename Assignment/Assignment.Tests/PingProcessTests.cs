﻿using IntelliTect.TestTools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment.Tests;

[TestClass]
public class PingProcessTests
{
    PingProcess Sut { get; set; } = new();

    [TestInitialize]
    public void TestInitialize()
    {
        Sut = new();
    }

    [TestMethod]
    public void Start_PingProcess_Success()
    {
        Process process = Process.Start("ping", "localhost");
        process.WaitForExit();
        Assert.AreEqual<int>(0, process.ExitCode);
    }

    [TestMethod]
    public void Run_GoogleDotCom_Success()
    {
        int exitCode = Sut.Run("google.com").ExitCode;
        Assert.AreEqual<int>(0, exitCode);
    }


    [TestMethod]
    public void Run_InvalidAddressOutput_Success()
    {
        (int exitCode, string? stdOutput) = Sut.Run("badaddress");
        Assert.IsFalse(string.IsNullOrWhiteSpace(stdOutput));
        stdOutput = WildcardPattern.NormalizeLineEndings(stdOutput!.Trim());
        Assert.AreEqual<string?>(
            "Ping request could not find host badaddress. Please check the name and try again.".Trim(),
            stdOutput,
            $"Output is unexpected: {stdOutput}");
        Assert.AreEqual<int>(1, exitCode);
    }

    [TestMethod]
    public void Run_CaptureStdOutput_Success()
    {
        PingResult result = Sut.Run("localhost");
        AssertValidPingOutput(result);
    }

    [TestMethod]
    public void RunTaskAsync_Success()
    {
        // Do NOT use async/await in this test.
        PingResult result = Sut.RunTaskAsync("localhost").Result;
        // Test Sut.RunTaskAsync("localhost");
        AssertValidPingOutput(result);
    }

    [TestMethod]
    public void RunAsync_UsingTaskReturn_Success()
    {
        // Do NOT use async/await in this test.
        PingResult result = Sut.RunAsync("localhost").Result;
        // Test Sut.RunAsync("localhost");
        AssertValidPingOutput(result);
    }

    [TestMethod]
    async public Task RunAsync_UsingTpl_Success()
    {
        // DO use async/await in this test.
        PingResult result = await Sut.RunAsync("localhost");
        // Test Sut.RunAsync("localhost");
        AssertValidPingOutput(result);
    }


    [TestMethod]
    [ExpectedException(typeof(AggregateException))]
    public void RunAsync_UsingTplWithCancellation_CatchAggregateExceptionWrapping()
    {
        CancellationTokenSource source = new();
        source.Cancel();
        Task<PingResult> result = Sut.RunAsync("localhost", source.Token);
        _ = result.Result;
    }

    [TestMethod]
    [ExpectedException(typeof(TaskCanceledException))]
    public void RunAsync_UsingTplWithCancellation_CatchAggregateExceptionWrappingTaskCanceledException()
    {
        CancellationTokenSource source = new();
        source.Cancel();
        Task<PingResult> result = Sut.RunAsync("localhost", source.Token);
        try
        {
            _ = result.Result;
        }
        catch (AggregateException ex)
        {
            throw ex.Flatten().InnerException!;
        }
    }

    [TestMethod]
    async public Task RunAsync_MultipleHostAddresses_True()
    {
        // Pseudo Code - don't trust it!!!
        string[] hostNames = new string[] { "localhost", "localhost", "localhost", "localhost" };
        int expectedLineCount = PingOutputLikeExpression.Split(Environment.NewLine).Length * hostNames.Length;
        PingResult result = await Sut.RunAsync(hostNames);
        int? lineCount = result.StdOutput?.Split(Environment.NewLine).Length;
        AssertValidPingOutput(result);
    }

    [TestMethod]
    async public Task RunLongRunningAsync_UsingTpl_Success()
    {
        PingResult result = await Sut.RunLongRunningAsync("localhost");
        // Test Sut.RunLongRunningAsync("localhost");
        AssertValidPingOutput(result);
    }

    [TestMethod]
    public void StringBuilderAppendLine_InParallel_IsNotThreadSafe()
    {
        IEnumerable<int> numbers = Enumerable.Range(0, short.MaxValue);
        System.Text.StringBuilder stringBuilder = new();
        numbers.AsParallel().ForAll(item => stringBuilder.AppendLine(""));
        int lineCount = stringBuilder.ToString().Split(Environment.NewLine).Length;
        Assert.AreNotEqual(lineCount, numbers.Count() + 1);
    }

    readonly string PingOutputLikeExpression = @"
Pinging * with 32 bytes of data:
Reply from ::1: time<*
Reply from ::1: time<*
Reply from ::1: time<*
Reply from ::1: time<*

Ping statistics for ::1:
    Packets: Sent = *, Received = *, Lost = 0 (0% loss),
Approximate round trip times in milli-seconds:
    Minimum = *, Maximum = *, Average = *".Trim();
    private void AssertValidPingOutput(int exitCode, string? stdOutput)
    {
        Assert.IsFalse(string.IsNullOrWhiteSpace(stdOutput));
        stdOutput = WildcardPattern.NormalizeLineEndings(stdOutput!.Trim());
        Assert.IsTrue(stdOutput?.IsLike(PingOutputLikeExpression) ?? false,
            $"Output is unexpected: {stdOutput}");
        Assert.AreEqual<int>(0, exitCode);
    }
    private void AssertValidPingOutput(PingResult result) =>
        AssertValidPingOutput(result.ExitCode, result.StdOutput);
}