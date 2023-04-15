using NUnit.Framework;
using System;
using Moq;
using PluginCommands.Commands;
using CommandSystem;
using FluentAssertions;
using System.Collections.Generic;
using PluginAPI.Loader;
using PluginAPI.Core;
using PluginAPI.Loader.Features;
using System.Reflection;
using System.Linq;
using PluginAPI.Core.Attributes;

namespace PluginCommands.UnitTests.Commands
{
    [TestFixture]
    public class PluginsManagerCommandTests
    {
        #region Tests Constants
        public const string NullSenderMessage = "Command sender is null.";

        public const string MissingPermsMessage = "You don't have permissions to execute this command.\nRequired permission: ServerConsoleCommands";
        #endregion

        #region Tests Static Utils
        private static readonly Type[] _emptyTypesArray = new Type[0];

        public static Mock<CommandSender> GetInvalidSender() => GetSender(0uL);

        public static Mock<CommandSender> GetValidSender() => GetSender((ulong) PluginsManagerCommand.PluginsManagementPermissions);

        public static void TestCommand_WithNullSender(ICommand command)
        {
            // Act
            var result = command.Execute(new ArraySegment<string>(), null, out var response);

            // Assert
            result.Should().BeFalse();
            response.Should().Be(NullSenderMessage);
        }

        public static void TestCommand_WithInvalidSender(ICommand command)
        {
            // Arrange
            var senderMock = GetInvalidSender();

            // Act
            var result = command.Execute(new ArraySegment<string>(), senderMock.Object, out var response);

            // Assert
            result.Should().BeFalse();
            response.Should().Be(MissingPermsMessage);
            senderMock.VerifyAll();
            senderMock.VerifyNoOtherCalls();
        }

        public static Mock<ServerConsoleSender> GetConsoleSender()
        {
            var mock = new Mock<ServerConsoleSender>(MockBehavior.Strict);
            mock.Setup(s => s.FullPermissions).Returns(true);
            return mock;
        }

        public static void InstallTestPlugins(IEnumerable<object> pluginsToInstall)
        {
            AssemblyLoader.Plugins.Clear();
            var plugins = new Dictionary<Type, PluginHandler>();

            foreach (var pl in pluginsToInstall)
            {
                var pluginType = pl.GetType();
                plugins.Add(pluginType, new PluginHandler(new PluginDirectory("./"), pl, pluginType, _emptyTypesArray));
            }

            AssemblyLoader.Plugins.Add(Assembly.GetExecutingAssembly(), plugins);
        }

        private static Mock<CommandSender> GetSender(ulong perms)
        {
            var mock = new Mock<CommandSender>(MockBehavior.Strict);
            mock.Setup(s => s.FullPermissions).Returns(false);
            mock.Setup(s => s.Permissions).Returns(perms);
            return mock;
        }
        #endregion

        [OneTimeSetUp]
        public void OneTimeSetUp() => InstallTestPlugins(new[] { new TestPlugin() });

        #region CheckPluginsManagementPerms Tests
        [Test]
        public void CheckPluginsManagementPerms_ShouldReturnProperMessage_WhenCommandSenderIsNull()
        {
            // Act
            var result = PluginsManagerCommand.CheckPluginsManagementPerms(null);

            // Assert
            result.Should().Be(NullSenderMessage);
        }

        [Test]
        public void CheckPluginsManagementPerms_ShouldReturnProperMessage_WhenCommandSenderHasMissingPermissions()
        {
            // Arrange
            var senderMock = GetInvalidSender();

            // Act
            var result = PluginsManagerCommand.CheckPluginsManagementPerms(senderMock.Object);

            // Assert
            result.Should().Be(MissingPermsMessage);
            senderMock.VerifyAll();
            senderMock.VerifyNoOtherCalls();
        }

        [Test]
        public void CheckPluginsManagementPerms_ShouldReturnNull_WhenCommandSenderHasRequiredPermissions()
        {
            // Arrange
            var senderMock = GetValidSender();

            // Act
            var result = PluginsManagerCommand.CheckPluginsManagementPerms(senderMock.Object);

            // Assert
            result.Should().BeNull();
            senderMock.VerifyAll();
            senderMock.VerifyNoOtherCalls();
        }
        #endregion

        [Test]
        public void PluginsManagerCommand_ShouldProperlyInitialize()
        {
            // Act
            var command = new PluginsManagerCommand();

            // Assert
            command.AllCommands.Count().Should().Be(3);
        }

        #region ExecuteParent Tests
        [Test]
        public void ExecuteParent_ShouldFail_WhenCommandSenderIsNull() => TestCommand_WithNullSender(new PluginsManagerCommand());

        [Test]
        public void ExecuteParent_ShouldFail_WhenCommandSenderHasMissingPermissions() => TestCommand_WithInvalidSender(new PluginsManagerCommand());

        [Test]
        public void ExecuteParent_ShouldSucceed_WhenGoldFlowInGame()
        {
            // Arrange
            var command = new PluginsManagerCommand();
            var senderMock = GetValidSender();

            // Act
            var result = command.Execute(new ArraySegment<string>(), senderMock.Object, out var response);

            // Assert
            result.Should().BeTrue();
            response.Should().Be("Currently installed plugins:\n - TestPlugin <color=grey>v1.0.0</color> <color=orange>@Test</color>\r\n");
            senderMock.VerifyAll();
            senderMock.VerifyNoOtherCalls();
        }

        [Test]
        public void ExecuteParent_ShouldSucceed_WhenGoldFlowInConsole()
        {
            // Arrange
            var command = new PluginsManagerCommand();
            var senderMock = GetConsoleSender();

            // Act
            var result = command.Execute(new ArraySegment<string>(), senderMock.Object, out var response);

            // Assert
            result.Should().BeTrue();
            response.Should().Be("Currently installed plugins:\n - TestPlugin v1.0.0 @Test\r\n");
            senderMock.VerifyAll();
            senderMock.VerifyNoOtherCalls();
        }
        #endregion
    }

    public class TestPlugin
    {
        [PluginEntryPoint("TestPlugin", "1.0.0", "Plugin for testing purposes only", "Test")]
        void Load() {}
    }
}
