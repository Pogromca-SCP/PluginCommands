using CommandSystem;
using LabApi.Loader;
using LabApi.Loader.Features.Plugins;

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
    public string Description { get; } = "Forces an installed plugin to disable itself.";

    /// <inheritdoc />
    protected override string HandlePluginCommand(Plugin plugin)
    {
        plugin.Disable();
        var props = plugin.Properties;

        if (props is not null)
        {
            props.IsEnabled = false;
        }

        PluginLoader.EnabledPlugins.Remove(plugin);
        return $"Disabled plugin '{plugin.Name}'.";
    }
}
