using NUnit.Framework;
using CommandSystem;
using PluginCommands.Commands;
using System.Collections.Generic;
using System.Linq;
using System;
using FluentAssertions;
using PluginAPI.Core.Attributes;

namespace PluginCommands.UnitTests.Commands
{
    [TestFixture]
    public class PluginCommandsTests
    {
        #region Tests Static Utils
        private static readonly object[] _plugins = new object[] { new TPlugin(), new TtPlugin(), new ExamplePlugin() };

        private static readonly ICommand[] _testedCommands = new ICommand[] { new LoadPluginCommand(), new ReloadPluginCommand(), new UnloadPluginCommand() };

        private static readonly string[] _invalidPluginNames = new[] { "", "test", "test test" };

        private static readonly string[] _validPluginNames = new[] { "TPlugin", "T Plugin", "Example Plugin" };

        private static IEnumerable<object[]> InvalidPluginTestCases => MergeCommandsAndPlugins(_testedCommands, _invalidPluginNames);

        private static IEnumerable<object[]> ValidPluginTestCases => MergeCommandsAndPlugins(_testedCommands, _validPluginNames);

        private static IEnumerable<object[]> MergeCommandsAndPlugins(ICommand[] commands, string[] plugins) =>
            commands.SelectMany(c => plugins.Select(p => new object[] { c, p }));
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
            var result = command.Execute(new ArraySegment<string>(), senderMock.Object, out var response);

            // Assert
            result.Should().BeFalse();
            response.Should().Be("Please specify a valid argument\nUsage: [Plugin Name] ");
            senderMock.VerifyAll();
            senderMock.VerifyNoOtherCalls();
        }

        [TestCaseSource(nameof(InvalidPluginTestCases))]
        public void Execute_ShouldFail_WhenPluginDoesNotExist_InGame(ICommand command, string pluginName)
        {
            // Arrange
            var senderMock = PluginsManagerCommandTests.GetValidSender();

            // Act
            var result = command.Execute(new ArraySegment<string>(pluginName.Split(' ')), senderMock.Object, out var response);

            // Assert
            result.Should().BeFalse();
            response.Should().Be($"Plugin '<color=green>{pluginName}</color>' not found.");
            senderMock.VerifyAll();
            senderMock.VerifyNoOtherCalls();
        }

        [TestCaseSource(nameof(InvalidPluginTestCases))]
        public void Execute_ShouldFail_WhenPluginDoesNotExist_InConsole(ICommand command, string pluginName)
        {
            // Arrange
            var senderMock = PluginsManagerCommandTests.GetConsoleSender();

            // Act
            var result = command.Execute(new ArraySegment<string>(pluginName.Split(' ')), senderMock.Object, out var response);

            // Assert
            result.Should().BeFalse();
            response.Should().Be($"Plugin '{pluginName}' not found.");
            senderMock.VerifyAll();
            senderMock.VerifyNoOtherCalls();
        }

        [TestCaseSource(nameof(ValidPluginTestCases))]
        public void Execute_ShouldSucceed_WhenGoldFlowInGame(ICommand command, string pluginName)
        {
            // Arrange
            var senderMock = PluginsManagerCommandTests.GetValidSender();

            // Act
            var result = command.Execute(new ArraySegment<string>(pluginName.Split(' ')), senderMock.Object, out var response);

            // Assert
            result.Should().BeTrue();
            response.Should().Match($"Plugin '<color=green>{pluginName}</color>' *");
            senderMock.VerifyAll();
            senderMock.VerifyNoOtherCalls();
        }

        [TestCaseSource(nameof(ValidPluginTestCases))]
        public void Execute_ShouldSucceed_WhenGoldFlowInConsole(ICommand command, string pluginName)
        {
            // Arrange
            var senderMock = PluginsManagerCommandTests.GetConsoleSender();

            // Act
            var result = command.Execute(new ArraySegment<string>(pluginName.Split(' ')), senderMock.Object, out var response);

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
        void Load() {}
    }

    public class TtPlugin
    {
        [PluginEntryPoint("T Plugin", "1.0.0", "Plugin for testing purposes only", "Test")]
        void Load() {}
    }

    public class ExamplePlugin
    {
        [PluginEntryPoint("Example Plugin", "1.0.0", "Plugin for testing purposes only", "Test")]
        void Load() {}
    }
}
