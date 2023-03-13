using CommandSystem;
using System;
using System.Text;
using PluginAPI.Loader;
using System.Linq;
using PluginAPI.Core;

namespace PluginCommands.Commands
{
    /// <summary>
    /// Main command provided by the plugin
    /// </summary>
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class PluginsManagerCommand : ParentCommand, IUsageProvider
    {
        /// <summary>
        /// Permission required by this command and any of its subcommands
        /// </summary>
        public const PlayerPermissions PluginsManagementPermissions = PlayerPermissions.ServerConsoleCommands;

        /// <summary>
        /// Checks if command sender has required permissions for plugins management
        /// </summary>
        /// <param name="sender">Sender to verify</param>
        /// <returns>Error message if sender does not have permissions or null otherwise</returns>
        public static string CheckPluginsManagementPerms(ICommandSender sender)
        {
            if (sender is null)
            {
                return "Command sender is null.";
            }

            if (!sender.CheckPermission(PluginsManagementPermissions))
            {
                return "You do not have permissions to run this command.";
            }

            return null;
        }

        /// <summary>
        /// Contains command name
        /// </summary>
        public override string Command { get; } = "plugincommands";

        /// <summary>
        /// Defines command aliases
        /// </summary>
        public override string[] Aliases { get; } = new[] { "plcommands", "plcmds" };

        /// <summary>
        /// Contains command description
        /// </summary>
        public override string Description { get; } = "Provides subcommands for plugins management at runtime and displays the list of installed plugins.";

        /// <summary>
        /// Defines command usage prompts
        /// </summary>
        public string[] Usage { get; } = new[] { "load/reload/unload",  "Plugin Name" };

        /// <summary>
        /// Initializes the command
        /// </summary>
        public PluginsManagerCommand() => LoadGeneratedCommands();

        /// <summary>
        /// Loads subcommands
        /// </summary>
        public override void LoadGeneratedCommands()
        {
            RegisterCommand(new LoadPluginCommand());
            RegisterCommand(new ReloadPluginCommand());
            RegisterCommand(new UnloadPluginCommand());
        }

        /// <summary>
        /// Executes the parent command
        /// </summary>
        /// <param name="arguments">Command arguments provided by sender</param>
        /// <param name="sender">Command sender</param>
        /// <param name="response">Response to display in sender's console</param>
        /// <returns>True if command executed successfully, false otherwise</returns>
        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = CheckPluginsManagementPerms(sender);

            if (!(response is null))
            {
                return false;
            }

            var sb = new StringBuilder("Currently installed plugins:\n");

            foreach (var plugin in AssemblyLoader.InstalledPlugins)
            {
                sb.AppendLine($" - {plugin.PluginName}");
            }

            response = sb.ToString();
            return true;
        }
    }

    /// <summary>
    /// Base class for plugin commands
    /// </summary>
    public abstract class PluginCommandBase : IUsageProvider
    {
        /// <summary>
        /// Defines command usage prompts
        /// </summary>
        public string[] Usage { get; } = new[] { "Plugin Name" };

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <param name="arguments">Command arguments provided by sender</param>
        /// <param name="sender">Command sender</param>
        /// <param name="response">Response to display in sender's console</param>
        /// <returns>True if command executed successfully, false otherwise</returns>
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = PluginsManagerCommand.CheckPluginsManagementPerms(sender);

            if (!(response is null))
            {
                return false;
            }

            if (arguments.Count == 0)
            {
                response = $"Please specify a valid argument\nUsage: {this.DisplayCommandUsage()}";
                return false;
            }

            var pluginName = string.Join(" ", arguments);
            var plugin = AssemblyLoader.InstalledPlugins.FirstOrDefault(p => p.PluginName.Equals(pluginName));

            if (plugin is null)
            {
                response = $"Plugin '<color=green>{pluginName}</color>' not found.";
                return false;
            }

            response = HandlePluginCommand(plugin);
            return true;
        }

        /// <summary>
        /// Handles the plugin command functionality
        /// </summary>
        /// <param name="plugin">Found plugin</param>
        /// <returns>Response to display in sender's console</returns>
        protected abstract string HandlePluginCommand(PluginHandler plugin);
    }
}
