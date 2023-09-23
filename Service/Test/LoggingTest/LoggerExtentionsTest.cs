namespace Test.JieChen.Lens.Logging;

using MsLogging = Microsoft.Extensions.Logging;
using Me.JieChen.Lens.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

[TestClass]
public class LoggerExtentionsTests
{
    private Mock<MsLogging.ILogger<object>> mockLogger = null;

    [TestInitialize]
    public void Initialize()
    {
        mockLogger = new Mock<MsLogging.ILogger<object>>();
    }

    [TestMethod]
    public void If_DebugIsCalled_Shoud_Called()
    {
        // arrange
        mockLogger.Setup(x => x.IsEnabled(MsLogging.LogLevel.Debug)).Returns(true);

        // act
        mockLogger.Object.LogDebug("caller", "message");

        // assert
        mockLogger.Verify(
            x => x.Log(
                MsLogging.LogLevel.Debug,
                10000,
                It.Is<It.IsAnyType>((obj, type) => true),
                null,
                It.Is<Func<It.IsAnyType, Exception, string>>((obj, type) => true)));
    }

    [TestMethod]
    public void If_InformationIsCalled_Shoud_Called()
    {
        // arrange
        mockLogger.Setup(x => x.IsEnabled(MsLogging.LogLevel.Information)).Returns(true);

        // act
        mockLogger.Object.LogInformation("caller", "message");

        // assert
        mockLogger.Verify(
            x => x.Log(
                MsLogging.LogLevel.Information,
                20000,
                It.Is<It.IsAnyType>((obj, type) => true),
                null,
                It.Is<Func<It.IsAnyType, Exception, string>>((obj, type) => true)));
    }

    [TestMethod]
    public void If_WarningIsCalled_Shoud_Called()
    {
        // arrange
        mockLogger.Setup(x => x.IsEnabled(MsLogging.LogLevel.Warning)).Returns(true);

        // act
        mockLogger.Object.LogWarning("caller", "message", new Exception());

        // assert
        mockLogger.Verify(
            x => x.Log(
                MsLogging.LogLevel.Warning,
                30000,
                It.Is<It.IsAnyType>((obj, type) => true),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((obj, type) => true)));
    }

    [TestMethod]
    public void If_ErrorIsCalled_Shoud_Called()
    {
        // arrange
        mockLogger.Setup(x => x.IsEnabled(MsLogging.LogLevel.Error)).Returns(true);

        // act
        mockLogger.Object.LogError("caller", "message", new Exception());

        // assert
        mockLogger.Verify(
            x => x.Log(
                MsLogging.LogLevel.Error,
                90000,
                It.Is<It.IsAnyType>((obj, type) => true),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((obj, type) => true)));
    }
}