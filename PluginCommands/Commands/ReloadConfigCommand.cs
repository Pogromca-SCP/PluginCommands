using CommandSystem;
using LabApi.Loader.Features.Plugins;

namespace PluginCommands.Commands;

/// <summary>
/// Command for config reloading.
/// </summary>
public class ReloadConfigCommand : PluginCommandBase, ICommand
{
    /// <summary>
    /// Contains command name.
    /// </summary>
    public string Command { get; } = "configreload";

    /// <summary>
    /// Defines command aliases.
    /// </summary>
    public string[] Aliases { get; } = ["confrld"];

    /// <summary>
    /// Contains command description.
    /// </summary>
    public string Description { get; } = "Reloads plugin configuration.";

    /// <inheritdoc />
    protected override string HandlePluginCommand(Plugin plugin)
    {
        plugin.LoadConfigs();
        return $"Plugin configuration for '{plugin.Name}' is reloaded.";
    }
}
