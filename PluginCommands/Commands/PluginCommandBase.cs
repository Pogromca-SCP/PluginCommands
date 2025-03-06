using CommandSystem;
using PluginAPI.Core;
using PluginAPI.Loader;
using System;
using System.Linq;

namespace PluginCommands.Commands;

/// <summary>
/// Base class for plugin commands.
/// </summary>
public abstract class PluginCommandBase : IUsageProvider
{
    /// <summary>
    /// Tells whether or not command response should be sanitized.
    /// </summary>
    public bool SanitizeResponse => true;

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
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        response = PluginsManagerCommand.CheckPluginsManagementPerms(sender);

        if (response is not null)
        {
            return false;
        }

        if (arguments.Count < 1)
        {
            response = $"Please specify a valid argument.\nUsage: {this.DisplayCommandUsage()}";
            return false;
        }

        var pluginName = string.Join(" ", arguments);
        var plugin = AssemblyLoader.InstalledPlugins.FirstOrDefault(p => p.PluginName.Equals(pluginName));

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
    protected abstract string HandlePluginCommand(PluginHandler plugin);
}
