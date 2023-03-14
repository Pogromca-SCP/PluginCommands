# PluginCommands
NwPluginAPI based plugin for SCP:Secret Laboratory. Provides custom commands for managing other NwPluginAPI based plugins from remote admin console at runtime.
 
This plugin was created using [official Northwood Plugin API](https://github.com/northwood-studios/NwPluginAPI). No additional dependencies need to be installed in order to run it.
 
## Installation
Simply put plugin `.dll` file inside your server's plugins folder.

## Commands
Commands from this plugin can be accessed from remote admin or server console.
|Command|Usage|Aliases|Description|Required permissions|
|---|---|---|---|---|
|plugincommands|[load/reload/unload] [Plugin Name]|plcommands plcmds|Provides subcommands for plugins management at runtime and displays the list of installed plugins.|ServerConsoleCommands|

### `plugincommands` subcommands
|Command|Usage|Aliases|Description|Required permissions|
|---|---|---|---|---|
|load|[Plugin Name]|enable on|Loads an installed plugin.|ServerConsoleCommands|
|reload|[Plugin Name]|refresh reset|Reloads an installed plugin.|ServerConsoleCommands|
|unload|[Plugin Name]|disable off|Unloads an installed plugin.|ServerConsoleCommands|
