using CommandSystem;
using LabApi.Loader;
using LabApi.Loader.Features.Plugins;
using System;
using System.Linq;

namespace PluginCommands.Commands;

/// <summary>
/// Base class for plugin commands.
/// </summary>
public abstract class PluginCommandBase : IUsageProvider
{
    /// <summary>
    /// Defines command usage prompts.
    /// </summary>
    public string[] Usage { get; } = ["Plugin Name"];

    /// <summary>
    /// Executes the command.
    /// </summary>
    /// <param name="arguments">Command arguments provided by sender.</param>
    /// <param name="sender">Command sender.</param>
    /// <param name="response">Response to display in sender's console.</param>
    /// <returns><see langword="true"/> if command executed successfully, <see langword="false"/> otherwise.</returns>
    public bool Execute(ArraySegment<string?> arguments, ICommandSender? sender, out string response)
    {
        var problem = PluginsManagerCommand.CheckPluginsManagementPerms(sender);

        if (problem is not null)
        {
            response = problem;
            return false;
        }

        if (arguments.Count < 1)
        {
            response = $"Please specify a valid argument.\nUsage: {this.DisplayCommandUsage()}";
            return false;
        }

        var pluginName = string.Join(" ", arguments);
        var plugin = PluginLoader.Plugins.Keys.FirstOrDefault(p => p.Name.Equals(pluginName));

        if (plugin is null)
        {
            response = $"Plugin '{pluginName}' not found.";
            return false;
        }

        response = HandlePluginCommand(plugin);
        return true;
    }

    /// <summary>
    /// Handles the plugin command functionality.
    /// </summary>
    /// <param name="plugin">Found plugin.</param>
    /// <returns>Response to display in sender's console.</returns>
    protected abstract string HandlePluginCommand(Plugin plugin);
}
