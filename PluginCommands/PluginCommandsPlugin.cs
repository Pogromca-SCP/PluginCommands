using LabApi.Features;
using LabApi.Features.Console;
using LabApi.Loader.Features.Plugins;
using PluginCommands.Commands;
using RemoteAdmin;
using System;

namespace PluginCommands;

/// <summary>
/// Defines plugin functionality.
/// </summary>
public class PluginCommandsPlugin : Plugin
{
    /// <summary>
    /// Contains plugin name to display.
    /// </summary>
    public const string PluginName = "PluginCommands";

    /// <summary>
    /// Contains current plugin version.
    /// </summary>
    public const string PluginVersion = "4.1.0";

    /// <summary>
    /// Contains plugin description.
    /// </summary>
    public const string PluginDescription = "Provides commands for LabAPI based plugins management at runtime.";

    /// <summary>
    /// Contains plugin author.
    /// </summary>
    public const string PluginAuthor = "Adam Szerszenowicz";

    /// <inheritdoc />
    public override string Name { get; } = PluginName;

    /// <inheritdoc />
    public override string Description { get; } = PluginDescription;

    /// <inheritdoc />
    public override string Author { get; } = PluginAuthor;

    /// <inheritdoc />
    public override Version Version { get; } = new(PluginVersion);

    /// <inheritdoc />
    public override Version RequiredApiVersion { get; } = new(LabApiProperties.CompiledVersion);

    /// <inheritdoc />
    public override bool IsTransparent => true;

    /// <summary>
    /// Parent command reference for manual commands registration.
    /// </summary>
    private PluginsManagerCommand? _cmd = null;

    /// <inheritdoc />
    public override void Enable()
    {
        if (_cmd is not null)
        {
            return;
        }

        Logger.Info("Registering plugin commands...");
        var cmd = new PluginsManagerCommand();
        _cmd = cmd;
        CommandProcessor.RemoteAdminCommandHandler.RegisterCommand(cmd);
        GameCore.Console.singleton?.ConsoleCommandHandler.RegisterCommand(cmd);
        Logger.Info("Plugin commands are registered.");
    }

    /// <inheritdoc />
    public override void Disable() {}
}
