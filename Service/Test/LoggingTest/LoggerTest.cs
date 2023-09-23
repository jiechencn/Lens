// <copyright file="LoggerTests.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Test.JieChen.Lens.Logging;

using MsLogging = Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using Me.JieChen.Lens.Logging;

[TestClass]
public class LoggerTests
{
    private readonly Mock<MsLogging.ILogger<object>> mockLogger = new Mock<MsLogging.ILogger<object>>();
    private ILogger<object> optLogger = null;

    [TestInitialize]
    public void Initialize()
    {
        optLogger = new Logger<object>(mockLogger.Object);
    }

    [TestMethod]
    public void If_LogDebug_Then_Execute()
    {
        // Arrange
        mockLogger.Setup(x => x.IsEnabled(MsLogging.LogLevel.Debug)).Returns(true);

        // Act
        optLogger.LogDebug("message");

        // Assert
        mockLogger.Verify(
            x => x.Log(
                MsLogging.LogLevel.Debug,
                10000,
                It.Is<It.IsAnyType>((obj, type) => true),
                null,
                It.Is<Func<It.IsAnyType, Exception, string>>((obj, type) => true)));
    }

    [TestMethod]
    public void If_LogInformation_Then_Execute()
    {
        // Arrange
        mockLogger.Setup(x => x.IsEnabled(MsLogging.LogLevel.Information)).Returns(true);

        // Act
        optLogger.LogInformation("message");

        // Assert
        mockLogger.Verify(
            x => x.Log(
                MsLogging.LogLevel.Information,
                20000,
                It.Is<It.IsAnyType>((obj, type) => true),
                null,
                It.Is<Func<It.IsAnyType, Exception, string>>((obj, type) => true)));
    }

    [TestMethod]
    public void If_LogWarning_Then_Execute()
    {
        // Arrange
        mockLogger.Setup(x => x.IsEnabled(MsLogging.LogLevel.Warning)).Returns(true);

        // Act
        optLogger.LogWarning("message", new Exception());

        // Assert
        mockLogger.Verify(
            x => x.Log(
                MsLogging.LogLevel.Warning,
                30000,
                It.Is<It.IsAnyType>((obj, type) => true),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((obj, type) => true)));
    }

    [TestMethod]
    public void If_LogError_Then_Execute()
    {
        // Arrange
        mockLogger.Setup(x => x.IsEnabled(MsLogging.LogLevel.Error)).Returns(true);

        // Act
        optLogger.LogError("message");

        // Assert
        mockLogger.Verify(
            x => x.Log(
                MsLogging.LogLevel.Error,
                90000,
                It.Is<It.IsAnyType>((obj, type) => true),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((obj, type) => true)));
    }
}

