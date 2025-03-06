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
    public string[] Aliases { get; } = ["refresh", "reset"];

    /// <summary>
    /// Contains command description.
    /// </summary>
    public string Description { get; } = "Reloads an installed plugin.";

    /// <inheritdoc />
    protected override string HandlePluginCommand(PluginHandler plugin)
    {
        plugin.Unload();
        plugin.Load();
        return $"Plugin '{plugin.PluginName}' reloaded.";
    }
}
