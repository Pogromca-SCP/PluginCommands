using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;

namespace PluginCommands;

/// <summary>
/// Defines plugin functionality.
/// </summary>
public class Plugin
{
    /// <summary>
    /// Contains current plugin version.
    /// </summary>
    public const string PluginVersion = "3.0.0";

    /// <summary>
    /// Contains plugin description.
    /// </summary>
    public const string PluginDescription = "Provides commands for NwPluginAPI based plugins management at runtime.";

    /// <summary>
    /// Contains plugin author.
    /// </summary>
    public const string PluginAuthor = "Adam Szerszenowicz";

    /// <summary>
    /// Loads and initializes the plugin.
    /// </summary>
    [PluginPriority(LoadPriority.Highest)]
    [PluginEntryPoint("Plugin Commands", PluginVersion, PluginDescription, PluginAuthor)]
    void LoadPlugin()
    {
        Log.Info("Plugin is loaded.", "PluginCommands: ");
    }
}
