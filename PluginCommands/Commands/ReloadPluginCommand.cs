using CommandSystem;
using LabApi.Loader;
using LabApi.Loader.Features.Plugins;

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
    public string Description { get; } = "Forces an installed plugin to restart and reloads its configuration.";

    /// <inheritdoc />
    protected override string HandlePluginCommand(Plugin plugin)
    {
        plugin.Disable();
        plugin.LoadConfigs();
        plugin.Enable();
        var props = plugin.Properties;

        if (props is not null)
        {
            props.IsEnabled = true;
        }

        PluginLoader.EnabledPlugins.Add(plugin);
        return $"Reloaded plugin '{plugin.Name}'.";
    }
}
