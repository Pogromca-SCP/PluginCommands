using AwesomeAssertions;
using CommandSystem;
using LabApi.Loader;
using LabApi.Loader.Features.Plugins;
using Moq;
using NUnit.Framework;
using PluginCommands.Commands;
using System.Collections.Generic;
using System.Linq;

namespace PluginCommands.UnitTests.Commands;

[TestFixture]
public class PluginCommandsTests
{
    private static readonly ICommand[] _testedCommands =
        [new LoadPluginCommand(), new ReloadPluginCommand(), new UnloadPluginCommand(), new ReloadConfigCommand()];

    private static IEnumerable<object[]> InvalidPluginTestCases => MergeCommandsAndPlugins(_testedCommands, ["", "test", "test test"]);

    private static IEnumerable<object[]> MergeCommandsAndPlugins(ICommand[] commands, string[] plugins) =>
        commands.SelectMany(c => plugins.Select<string, object[]>(p => [c, p]));

    [OneTimeTearDown]
    public void OneTimeTearDown() => Shared.UninstallTestPlugins();

    [TestCaseSource(nameof(_testedCommands))]
    public void Execute_ShouldFail_WhenCommandSenderIsNull(ICommand command) => Shared.TestCommand_WithNullSender(command);

    [TestCaseSource(nameof(_testedCommands))]
    public void Execute_ShouldFail_WhenCommandSenderHasMissingPermissions(ICommand command) => Shared.TestCommand_WithInvalidSender(command);

    [TestCaseSource(nameof(_testedCommands))]
    public void Execute_ShouldFail_WhenNoArgumentsWereProvided(ICommand command)
    {
        // Arrange
        var senderMock = Shared.GetValidSender();

        // Act
        var result = command.Execute(new(), senderMock.Object, out var response);

        // Assert
        result.Should().BeFalse();
        response.Should().Be("Please specify a valid argument.\nUsage: [Plugin Name] ");
        senderMock.VerifyAll();
    }

    [TestCaseSource(nameof(InvalidPluginTestCases))]
    public void Execute_ShouldFail_WhenPluginDoesNotExist(ICommand command, string pluginName)
    {
        // Arrange
        var senderMock = Shared.GetValidSender();

        // Act
        var result = command.Execute(new(pluginName.Split(' ')), senderMock.Object, out var response);

        // Assert
        result.Should().BeFalse();
        response.Should().Be($"Plugin '{pluginName}' not found.");
        senderMock.VerifyAll();
    }

    [Test]
    public void Execute_ShouldSucceed_WhenLoadPluginCommand()
    {
        // Arrange
        const string pluginName = "Test Plugin";
        var command = new LoadPluginCommand();
        var pluginMock = new Mock<Plugin>(MockBehavior.Strict);
        pluginMock.Setup(p => p.Name).Returns(pluginName);
        pluginMock.Setup(p => p.Enable());
        Shared.InstallTestPlugin(pluginMock.Object);
        var senderMock = Shared.GetValidSender();

        // Act
        var result = command.Execute(new(pluginName.Split(' ')), senderMock.Object, out var response);

        // Assert
        result.Should().BeTrue();
        response.Should().Be($"Enabled plugin '{pluginName}'.");
        PluginLoader.EnabledPlugins.Should().Contain(pluginMock.Object);
        senderMock.VerifyAll();
        pluginMock.VerifyAll();
    }

    [Test]
    public void Execute_ShouldSucceed_WhenReloadPluginCommand()
    {
        // Arrange
        const string pluginName = "TPlugin";
        var command = new ReloadPluginCommand();
        var pluginMock = new Mock<Plugin>(MockBehavior.Strict);
        pluginMock.Setup(p => p.Name).Returns(pluginName);
        pluginMock.Setup(p => p.Disable());
        pluginMock.Setup(p => p.LoadConfigs());
        pluginMock.Setup(p => p.Enable());
        Shared.InstallTestPlugin(pluginMock.Object);
        var senderMock = Shared.GetValidSender();

        // Act
        var result = command.Execute(new(pluginName.Split(' ')), senderMock.Object, out var response);

        // Assert
        result.Should().BeTrue();
        response.Should().Be($"Reloaded plugin '{pluginName}'.");
        PluginLoader.EnabledPlugins.Should().Contain(pluginMock.Object);
        senderMock.VerifyAll();
        pluginMock.VerifyAll();
    }

    [Test]
    public void Execute_ShouldSucceed_WhenUnloadPluginCommand()
    {
        // Arrange
        const string pluginName = "T Plugin";
        var command = new UnloadPluginCommand();
        var pluginMock = new Mock<Plugin>(MockBehavior.Strict);
        pluginMock.Setup(p => p.Name).Returns(pluginName);
        pluginMock.Setup(p => p.Disable());
        Shared.InstallTestPlugin(pluginMock.Object);
        var senderMock = Shared.GetValidSender();

        // Act
        var result = command.Execute(new(pluginName.Split(' ')), senderMock.Object, out var response);

        // Assert
        result.Should().BeTrue();
        response.Should().Be($"Disabled plugin '{pluginName}'.");
        PluginLoader.EnabledPlugins.Should().NotContain(pluginMock.Object);
        senderMock.VerifyAll();
        pluginMock.VerifyAll();
    }

    [Test]
    public void Execute_ShouldSucceed_WhenReloadConfigCommand()
    {
        // Arrange
        const string pluginName = "Example Plugin";
        var command = new ReloadConfigCommand();
        var pluginMock = new Mock<Plugin>(MockBehavior.Strict);
        pluginMock.Setup(p => p.Name).Returns(pluginName);
        pluginMock.Setup(p => p.LoadConfigs());
        Shared.InstallTestPlugin(pluginMock.Object);
        var senderMock = Shared.GetValidSender();

        // Act
        var result = command.Execute(new(pluginName.Split(' ')), senderMock.Object, out var response);

        // Assert
        result.Should().BeTrue();
        response.Should().Be($"Reloaded configuration for '{pluginName}'.");
        senderMock.VerifyAll();
        pluginMock.VerifyAll();
    }
}
