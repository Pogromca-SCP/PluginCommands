using CommandSystem;
using PluginAPI.Core;

namespace PluginCommands.Commands;

/// <summary>
/// Command for plugin reloading.
/// </summary>
public class ReloadPluginCommand : PluginCommandBase, ICommand
{
    /// <summary>
    /// Contains command name.
    /// </summary>
    public string Command { get; } = "reload";

    /// <summary>
    /// Defines command aliases.
    /// </summary>
    public string[] Aliases { get; } = new[] { "refresh", "reset" };

    /// <summary>
    /// Contains command description.
    /// </summary>
    public string Description { get; } = "Reloads an installed plugin.";

    /// <summary>
    /// Handles the plugin command functionality.
    /// </summary>
    /// <param name="plugin">Found plugin.</param>
    /// <param name="isConsole">Tells whether or not the command is executed by server console.</param>
    /// <returns>Response to display in sender's console.</returns>
    protected override string HandlePluginCommand(PluginHandler plugin, bool isConsole)
    {
        plugin.Unload();
        plugin.Load();
        return isConsole ? $"Plugin '{plugin.PluginName}' reloaded." : $"Plugin '<color=green>{plugin.PluginName}</color>' reloaded.";
    }
}
