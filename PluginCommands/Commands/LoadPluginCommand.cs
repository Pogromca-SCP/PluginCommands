using CommandSystem;
using LabApi.Loader;
using LabApi.Loader.Features.Plugins;

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
    public string Description { get; } = "Forces an installed plugin to enable itself.";

    /// <inheritdoc />
    protected override string HandlePluginCommand(Plugin plugin)
    {
        plugin.Enable();
        var props = plugin.Properties;
        props?.IsEnabled = true;
        PluginLoader.EnabledPlugins.Add(plugin);
        return $"Enabled plugin '{plugin.Name}'.";
    }
}
