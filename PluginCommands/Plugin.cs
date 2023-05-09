using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PluginAPI.Core;

namespace PluginCommands
{
    /// <summary>
    /// Defines plugin functionality.
    /// </summary>
    public class Plugin
    {
        public const string PluginVersion = "2.1.0";
        public const string PluginDescription = "Commands for plugins management at runtime.";
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
}
