using CommandSystem;
using PluginAPI.Core;

namespace PluginCommands.Commands;

/// <summary>
/// Command for plugin loading.
/// </summary>
public class LoadPluginCommand : PluginCommandBase, ICommand
{
    /// <summary>
    /// Contains command name.
    /// </summary>
    public string Command { get; } = "load";

    /// <summary>
    /// Defines command aliases.
    /// </summary>
    public string[] Aliases { get; } = ["enable", "on"];

    /// <summary>
    /// Contains command description.
    /// </summary>
    public string Description { get; } = "Loads an installed plugin.";

    /// <inheritdoc />
    protected override string HandlePluginCommand(PluginHandler plugin)
    {
        plugin.Load();
        return $"Plugin '{plugin.PluginName}' loaded.";
    }
}
