using FluentAssertions;
using LabApi.Loader.Features.Plugins;
using Moq;
using NUnit.Framework;
using PluginCommands.Commands;

namespace PluginCommands.UnitTests.Commands;

[TestFixture]
public class PluginsManagerCommandTests
{
    private readonly PluginsManagerCommand _command = new();

    [OneTimeTearDown]
    public void OneTimeTearDown() => Shared.UninstallTestPlugins();

    #region CheckPluginsManagementPerms Tests
    [Test]
    public void CheckPluginsManagementPerms_ShouldReturnProperMessage_WhenCommandSenderIsNull()
    {
        // Act
        var result = PluginsManagerCommand.CheckPluginsManagementPerms(null);

        // Assert
        result.Should().Be(Shared.NullSenderMessage);
    }

    [Test]
    public void CheckPluginsManagementPerms_ShouldReturnProperMessage_WhenCommandSenderHasMissingPermissions()
    {
        // Arrange
        var senderMock = Shared.GetInvalidSender();

        // Act
        var result = PluginsManagerCommand.CheckPluginsManagementPerms(senderMock.Object);

        // Assert
        result.Should().Be(Shared.MissingPermsMessage);
        senderMock.VerifyAll();
    }

    [Test]
    public void CheckPluginsManagementPerms_ShouldReturnNull_WhenCommandSenderHasRequiredPermissions()
    {
        // Arrange
        var senderMock = Shared.GetValidSender();

        // Act
        var result = PluginsManagerCommand.CheckPluginsManagementPerms(senderMock.Object);

        // Assert
        result.Should().BeNull();
        senderMock.VerifyAll();
    }
    #endregion

    #region Constructor Tests
    [Test]
    public void PluginsManagerCommand_ShouldProperlyInitialize() => _command.AllCommands.Should().HaveCount(4);
    #endregion

    #region ExecuteParent Tests
    [Test]
    public void ExecuteParent_ShouldFail_WhenCommandSenderIsNull() => Shared.TestCommand_WithNullSender(_command);

    [Test]
    public void ExecuteParent_ShouldFail_WhenCommandSenderHasMissingPermissions() => Shared.TestCommand_WithInvalidSender(_command);

    [Test]
    public void ExecuteParent_ShouldSucceed_WhenGoldFlow()
    {
        // Arrange
        const string pluginString = "'TestPlugin', Version: 1.0.0, Author: 'Test'";
        var pluginMock = new Mock<Plugin>(MockBehavior.Strict);
        pluginMock.Setup(p => p.ToString()).Returns(pluginString);
        Shared.InstallTestPlugin(pluginMock.Object);
        var senderMock = Shared.GetValidSender();

        // Act
        var result = _command.Execute(new(), senderMock.Object, out var response);

        // Assert
        result.Should().BeTrue();
        response.Should().Be($"Currently installed plugins:\n- {pluginString}, Status: Unknown\n");
        senderMock.VerifyAll();
        pluginMock.VerifyAll();
    }
    #endregion
}
