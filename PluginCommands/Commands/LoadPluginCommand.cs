﻿using CommandSystem;
using PluginAPI.Core;

namespace PluginCommands.Commands
{
    /// <summary>
    /// Command for plugin loading
    /// </summary>
    public class LoadPluginCommand : PluginCommandBase, ICommand
    {
        /// <summary>
        /// Contains command name
        /// </summary>
        public string Command { get; } = "load";

        /// <summary>
        /// Defines command aliases
        /// </summary>
        public string[] Aliases { get; } = new[] { "enable", "on" };

        /// <summary>
        /// Contains command description
        /// </summary>
        public string Description { get; } = "Loads an installed plugin.";

        /// <summary>
        /// Handles the plugin command functionality
        /// </summary>
        /// <param name="plugin">Found plugin</param>
        /// <returns>Response to display in sender's console</returns>
        protected override string HandlePluginCommand(PluginHandler plugin)
        {
            plugin.Load();
            return $"Plugin '<color=green>{plugin.PluginName}</color>' loaded.";
        }
    }
}