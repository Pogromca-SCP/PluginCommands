# PluginCommands
NwPluginAPI based plugin for SCP:Secret Laboratory. Provides custom commands for managing other NwPluginAPI based plugins from remote admin console at runtime.
 
This plugin was created using [official Northwood Plugin API](https://github.com/northwood-studios/NwPluginAPI). No additional dependencies need to be installed in order to run it.
 
## Installation
Simply put plugin `.dll` file inside your server's plugins folder.

## Commands
|Command|Usage|Aliases|Description|
|---|---|---|---|
|plugincommands|[load/reload/unload] [Plugin Name]|plcommands plcmds|Provides subcommands for plugins management at runtime and displays the list of installed plugins.|

### `plugincommands` subcommands
|Command|Usage|Aliases|Description|
|---|---|---|---|
|load|[Plugin Name]|enable on|Loads an installed plugin.|
|reload|[Plugin Name]|refresh reset|Reloads an installed plugin.|
|unload|[Plugin Name]|disable off|Unloads an installed plugin.|
