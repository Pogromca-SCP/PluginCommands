# PluginCommands
[![GitHub release](https://flat.badgen.net/github/release/Pogromca-SCP/PluginCommands)](https://github.com/Pogromca-SCP/PluginCommands/releases/)
[![GitHub license](https://flat.badgen.net/github/license/Pogromca-SCP/PluginCommands)](https://github.com/Pogromca-SCP/PluginCommands/blob/main/LICENSE)
![GitHub downloads](https://flat.badgen.net/github/assets-dl/Pogromca-SCP/PluginCommands)
![GitHub last commit](https://flat.badgen.net/github/last-commit/Pogromca-SCP/PluginCommands/main)
![GitHub checks](https://flat.badgen.net/github/checks/Pogromca-SCP/PluginCommands/main)

NwPluginAPI based plugin for SCP:Secret Laboratory. Provides custom commands for managing other NwPluginAPI based plugins at runtime.
 
This plugin was created using [official Northwood Plugin API](https://github.com/northwood-studios/NwPluginAPI). No additional dependencies need to be installed in order to run it.
 
## Installation
### Automatic
1. Run `p install Pogromca-SCP/PluginCommands` in the server console.
2. Restart the server.

### Manual
1. Download `PluginCommands.dll` file from [latest release](https://github.com/Pogromca-SCP/PluginCommands/releases/latest).
2. Place downloaded file in your server's plugins folder `{SecretLabDirectory}/PluginAPI/plugins/{port|global}`.
3. Restart the server.

## Commands
Commands from this plugin can be accessed from remote admin or server console.
| Command                              | Usage                              | Aliases           | Description                                | Required permissions  |
| ------------------------------------ | ---------------------------------- | ----------------- | ------------------------------------------ | --------------------- |
| plugincommands <a name="plcmds"></a> | [load/reload/unload] [Plugin Name] | plcommands plcmds | Provides subcommands for plugins management at runtime and displays the list of installed plugins. | ServerConsoleCommands |

### [`plugincommands`](#plcmds) subcommands
| Command | Usage         | Aliases       | Description                  | Required permissions  |
| ------- | ------------- | ------------- | ---------------------------- | --------------------- |
| load    | [Plugin Name] | enable on     | Loads an installed plugin.   | ServerConsoleCommands |
| reload  | [Plugin Name] | refresh reset | Reloads an installed plugin. | ServerConsoleCommands |
| unload  | [Plugin Name] | disable off   | Unloads an installed plugin. | ServerConsoleCommands |
