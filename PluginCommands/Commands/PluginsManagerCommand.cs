using CommandSystem;
using System;
using NorthwoodLib.Pools;
using PluginAPI.Loader;

namespace PluginCommands.Commands;

/// <summary>
/// Main command provided by the plugin.
/// </summary>
[CommandHandler(typeof(RemoteAdminCommandHandler))]
[CommandHandler(typeof(GameConsoleCommandHandler))]
public class PluginsManagerCommand : ParentCommand, IUsageProvider
{
    /// <summary>
    /// Permission required by this command and any of its subcommands.
    /// </summary>
    public const PlayerPermissions PluginsManagementPermissions = PlayerPermissions.ServerConsoleCommands;

    /// <summary>
    /// Checks if command sender has required permissions for plugins management.
    /// </summary>
    /// <param name="sender">Sender to verify.</param>
    /// <returns>Error message if sender does not have permissions or <see langword="null"/> otherwise.</returns>
    public static string CheckPluginsManagementPerms(ICommandSender sender)
    {
        if (sender is null)
        {
            return "Command sender is null.";
        }
        
        if (!sender.CheckPermission(PluginsManagementPermissions, out var response))
        {
            return response;
        }

        return null;
    }

    /// <summary>
    /// Contains command name.
    /// </summary>
    public override string Command { get; } = "plugincommands";

    /// <summary>
    /// Defines command aliases.
    /// </summary>
    public override string[] Aliases { get; } = new[] { "plcommands", "plcmds" };

    /// <summary>
    /// Contains command description.
    /// </summary>
    public override string Description { get; } = "Provides subcommands for plugins management at runtime and displays the list of installed plugins.";

    /// <summary>
    /// Defines command usage prompts.
    /// </summary>
    public string[] Usage { get; } = new[] { "load/reload/unload",  "Plugin Name" };

    /// <summary>
    /// Initializes the command.
    /// </summary>
    public PluginsManagerCommand() => LoadGeneratedCommands();

    /// <summary>
    /// Loads subcommands.
    /// </summary>
    public override void LoadGeneratedCommands()
    {
        RegisterCommand(new LoadPluginCommand());
        RegisterCommand(new ReloadPluginCommand());
        RegisterCommand(new UnloadPluginCommand());
    }

    /// <summary>
    /// Executes the parent command.
    /// </summary>
    /// <param name="arguments">Command arguments provided by sender.</param>
    /// <param name="sender">Command sender.</param>
    /// <param name="response">Response to display in sender's console.</param>
    /// <returns><see langword="true"/> if command executed successfully, <see langword="false"/> otherwise.</returns>
    protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        response = CheckPluginsManagementPerms(sender);

        if (response is not null)
        {
            return false;
        }

        var isConsole = sender is ServerConsoleSender;
        var sb = StringBuilderPool.Shared.Rent("Currently installed plugins:\n");

        foreach (var plugin in AssemblyLoader.InstalledPlugins)
        {
            sb.AppendLine(isConsole ? $" - {plugin.PluginName} v{plugin.PluginVersion} @{plugin.PluginAuthor}" :
                $" - {plugin.PluginName} <color=#808080ff>v{plugin.PluginVersion}</color> <color=orange>@{plugin.PluginAuthor}</color>");
        }

        response = StringBuilderPool.Shared.ToStringReturn(sb);
        return true;
    }
}
