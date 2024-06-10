using CommandSystem;
using FluentAssertions;
using NUnit.Framework;
using PluginAPI.Core.Attributes;
using PluginCommands.Commands;
using System.Collections.Generic;
using System.Linq;

namespace PluginCommands.UnitTests.Commands;

[TestFixture]
public class PluginCommandsTests
{
    #region Tests Static Utils
    private static readonly object[] _plugins = [new TPlugin(), new TtPlugin(), new ExamplePlugin()];

    private static readonly ICommand[] _testedCommands = [new LoadPluginCommand(), new ReloadPluginCommand(), new UnloadPluginCommand()];

    private static readonly string[] _invalidPluginNames = ["", "test", "test test"];

    private static readonly string[] _validPluginNames = ["TPlugin", "T Plugin", "Example Plugin"];

    private static IEnumerable<object[]> InvalidPluginTestCases => MergeCommandsAndPlugins(_testedCommands, _invalidPluginNames);

    private static IEnumerable<object[]> ValidPluginTestCases => MergeCommandsAndPlugins(_testedCommands, _validPluginNames);

    private static IEnumerable<object[]> MergeCommandsAndPlugins(ICommand[] commands, string[] plugins) =>
        commands.SelectMany(c => plugins.Select<string, object[]>(p => [c, p]));
    #endregion

    [OneTimeSetUp]
    public void OneTimeSetUp() => PluginsManagerCommandTests.InstallTestPlugins(_plugins);

    #region Execute Tests
    [TestCaseSource(nameof(_testedCommands))]
    public void Execute_ShouldFail_WhenCommandSenderIsNull(ICommand command) => PluginsManagerCommandTests.TestCommand_WithNullSender(command);

    [TestCaseSource(nameof(_testedCommands))]
    public void Execute_ShouldFail_WhenCommandSenderHasMissingPermissions(ICommand command) => PluginsManagerCommandTests.TestCommand_WithInvalidSender(command);

    [TestCaseSource(nameof(_testedCommands))]
    public void Execute_ShouldFail_WhenNoArgumentsWereProvided(ICommand command)
    {
        // Arrange
        var senderMock = PluginsManagerCommandTests.GetValidSender();

        // Act
        var result = command.Execute(new(), senderMock.Object, out var response);

        // Assert
        result.Should().BeFalse();
        response.Should().Be("Please specify a valid argument.\nUsage: [Plugin Name] ");
        senderMock.VerifyAll();
        senderMock.VerifyNoOtherCalls();
    }

    [TestCaseSource(nameof(InvalidPluginTestCases))]
    public void Execute_ShouldFail_WhenPluginDoesNotExist(ICommand command, string pluginName)
    {
        // Arrange
        var senderMock = PluginsManagerCommandTests.GetValidSender();

        // Act
        var result = command.Execute(new(pluginName.Split(' ')), senderMock.Object, out var response);

        // Assert
        result.Should().BeFalse();
        response.Should().Be($"Plugin '{pluginName}' not found.");
        senderMock.VerifyAll();
        senderMock.VerifyNoOtherCalls();
    }

    [TestCaseSource(nameof(ValidPluginTestCases))]
    public void Execute_ShouldSucceed_WhenGoldFlow(ICommand command, string pluginName)
    {
        // Arrange
        var senderMock = PluginsManagerCommandTests.GetValidSender();

        // Act
        var result = command.Execute(new(pluginName.Split(' ')), senderMock.Object, out var response);

        // Assert
        result.Should().BeTrue();
        response.Should().Match($"Plugin '{pluginName}' *");
        senderMock.VerifyAll();
        senderMock.VerifyNoOtherCalls();
    }
    #endregion
}

public class TPlugin
{
    [PluginEntryPoint("TPlugin", "1.0.0", "Plugin for testing purposes only", "Test")]
    private void Load() {}
}

public class TtPlugin
{
    [PluginEntryPoint("T Plugin", "1.0.0", "Plugin for testing purposes only", "Test")]
    private void Load() {}
}

public class ExamplePlugin
{
    [PluginEntryPoint("Example Plugin", "1.0.0", "Plugin for testing purposes only", "Test")]
    private void Load() {}
}
