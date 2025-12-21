# PluginCommands
[![GitHub release](https://flat.badgen.net/github/release/Pogromca-SCP/PluginCommands)](https://github.com/Pogromca-SCP/PluginCommands/releases/)
[![GitHub license](https://flat.badgen.net/github/license/Pogromca-SCP/PluginCommands)](https://github.com/Pogromca-SCP/PluginCommands/blob/main/LICENSE)
![GitHub downloads](https://flat.badgen.net/github/assets-dl/Pogromca-SCP/PluginCommands)
![GitHub last commit](https://flat.badgen.net/github/last-commit/Pogromca-SCP/PluginCommands/main)
![GitHub checks](https://flat.badgen.net/github/checks/Pogromca-SCP/PluginCommands/main)

LabAPI based plugin for SCP:Secret Laboratory. Provides custom commands for managing other LabAPI based plugins at runtime.
 
This plugin was created using [official Northwood Lab API](https://github.com/northwood-studios/LabAPI). No additional dependencies need to be installed in order to run it.
 
## Installation
### Automatic
1. Run `p install Pogromca-SCP/PluginCommands` in the server console.
2. Restart the server.

### Manual
1. Download `PluginCommands.dll` file from [latest release](https://github.com/Pogromca-SCP/PluginCommands/releases/latest).
2. Place downloaded file in your server's plugins folder (default `{ServerDirectory}/LabAPI/plugins/{port|global}`).
3. Restart the server.

## Commands
Commands from this plugin can be accessed from remote admin or server console.
| Command                              | Usage                                           | Aliases           | Description                                                                                                                        | Required permissions  |
| ------------------------------------ | ----------------------------------------------- | ----------------- | ---------------------------------------------------------------------------------------------------------------------------------- | --------------------- |
| plugincommands <a name="plcmds"></a> | [load/reload/unload/configreload] [Plugin Name] | plcommands plcmds | Provides subcommands for plugins management at runtime. Displays the list of installed plugins if no valid subcommand is selected. | ServerConsoleCommands |

### [`plugincommands`](#plcmds) subcommands
| Command      | Usage         | Aliases       | Description                                                          | Required permissions  |
| ------------ | ------------- | ------------- | -------------------------------------------------------------------- | --------------------- |
| load         | [Plugin Name] | enable on     | Forces an installed plugin to enable itself.                         | ServerConsoleCommands |
| reload       | [Plugin Name] | refresh reset | Forces an installed plugin to restart and reloads its configuration. | ServerConsoleCommands |
| unload       | [Plugin Name] | disable off   | Forces an installed plugin to disable itself.                        | ServerConsoleCommands |
| configreload | [Plugin Name] | confrld       | Reloads plugin configuration.                                        | ServerConsoleCommands |
