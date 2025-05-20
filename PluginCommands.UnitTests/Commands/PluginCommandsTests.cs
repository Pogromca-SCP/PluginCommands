using CommandSystem;
using FluentAssertions;
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
    #region Test Case Sources
    private static readonly Mock<Plugin>[] _plugins =
        [Shared.GetPluginMock("TPlugin"), Shared.GetPluginMock("T Plugin"), Shared.GetPluginMock("Example Plugin")];

    private static readonly ICommand[] _testedCommands =
        [new LoadPluginCommand(), new ReloadPluginCommand(), new UnloadPluginCommand(), new ReloadConfigCommand()];

    private static readonly string[] _invalidPluginNames = ["", "test", "test test"];

    private static readonly string[] _validPluginNames = ["TPlugin", "T Plugin", "Example Plugin"];

    private static IEnumerable<object[]> InvalidPluginTestCases => MergeCommandsAndPlugins(_testedCommands, _invalidPluginNames);

    private static IEnumerable<object[]> ValidPluginTestCases => MergeCommandsAndPlugins(_testedCommands, _validPluginNames);

    private static IEnumerable<object[]> MergeCommandsAndPlugins(ICommand[] commands, string[] plugins) =>
        commands.SelectMany(c => plugins.Select<string, object[]>(p => [c, p]));
    #endregion

    [OneTimeSetUp]
    public void OneTimeSetUp() => Shared.InstallTestPlugins(_plugins.Select(p => p.Object));

    [OneTimeTearDown]
    public void OneTimeTearDown() => Shared.UninstallTestPlugins();

    #region Execute Tests
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

    [TestCaseSource(nameof(ValidPluginTestCases))]
    public void Execute_ShouldSucceed_WhenGoldFlow(ICommand command, string pluginName)
    {
        // Arrange
        var senderMock = Shared.GetValidSender();

        // Act
        var result = command.Execute(new(pluginName.Split(' ')), senderMock.Object, out var response);

        // Assert
        result.Should().BeTrue();
        response.Should().Contain($"'{pluginName}'");
        senderMock.VerifyAll();
    }
    #endregion
}
