using CommandSystem;
using FluentAssertions;
using Moq;
using PluginAPI.Core;
using PluginAPI.Loader;
using PluginCommands.Commands;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace PluginCommands.UnitTests.Commands;

public static class Shared
{
    public const string NullSenderMessage = "Command sender is null.";

    public const string MissingPermsMessage = "You don't have permissions to execute this command.\nRequired permission: ServerConsoleCommands";

    public static Mock<CommandSender> GetInvalidSender() => GetSender(0uL);

    public static Mock<CommandSender> GetValidSender() => GetSender((ulong) PluginsManagerCommand.PluginsManagementPermissions);

    public static void TestCommand_WithNullSender(ICommand command)
    {
        // Act
        var result = command.Execute(new(), null, out var response);

        // Assert
        result.Should().BeFalse();
        response.Should().Be(NullSenderMessage);
    }

    public static void TestCommand_WithInvalidSender(ICommand command)
    {
        // Arrange
        var senderMock = GetInvalidSender();

        // Act
        var result = command.Execute(new(), senderMock.Object, out var response);

        // Assert
        result.Should().BeFalse();
        response.Should().Be(MissingPermsMessage);
        senderMock.VerifyAll();
        senderMock.VerifyNoOtherCalls();
    }

    public static void InstallTestPlugins(IEnumerable<object> pluginsToInstall)
    {
        var plugins = new Dictionary<Type, PluginHandler>();

        foreach (var pl in pluginsToInstall)
        {
            var pluginType = pl.GetType();
            plugins.Add(pluginType, new(new("./"), pl, pluginType, []));
        }

        AssemblyLoader.Plugins.Add(Assembly.GetExecutingAssembly(), plugins);
    }

    public static void UninstallTestPlugins() => AssemblyLoader.Plugins.Clear();

    private static Mock<CommandSender> GetSender(ulong perms)
    {
        var mock = new Mock<CommandSender>(MockBehavior.Strict);
        mock.Setup(s => s.FullPermissions).Returns(false);
        mock.Setup(s => s.Permissions).Returns(perms);
        return mock;
    }
}
