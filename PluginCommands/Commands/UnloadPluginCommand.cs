using CommandSystem;
using PluginAPI.Core;

namespace PluginCommands.Commands;

/// <summary>
/// Command for plugin unloading.
/// </summary>
public class UnloadPluginCommand : PluginCommandBase, ICommand
{
    /// <summary>
    /// Contains command name.
    /// </summary>
    public string Command { get; } = "unload";

    /// <summary>
    /// Defines command aliases.
    /// </summary>
    public string[] Aliases { get; } = ["disable", "off"];

    /// <summary>
    /// Contains command description.
    /// </summary>
    public string Description { get; } = "Unloads an installed plugin.";

    /// <inheritdoc />
    protected override string HandlePluginCommand(PluginHandler plugin, bool isConsole)
    {
        plugin.Unload();
        return isConsole ? $"Plugin '{plugin.PluginName}' unloaded." : $"Plugin '<color=green>{plugin.PluginName}</color>' unloaded.";
    }
}
