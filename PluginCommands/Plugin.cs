using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PluginAPI.Core;

namespace PluginCommands
{
    /// <summary>
    /// Defines plugin functionality
    /// </summary>
    public class Plugin
    {
        /// <summary>
        /// Loads and initializes the plugin
        /// </summary>
        [PluginPriority(LoadPriority.Highest)]
        [PluginEntryPoint("Plugin Commands", "1.1.0", "Commands for plugins management at runtime", "Adam Szerszenowicz")]
        void LoadPlugin()
        {
            Log.Info("Plugin is loaded.", "PluginCommands: ");
        }
    }
}
