# PluginCommands
[![GitHub release](https://flat.badgen.net/github/release/Pogromca-SCP/PluginCommands)](https://github.com/Pogromca-SCP/PluginCommands/releases/)
[![GitHub license](https://flat.badgen.net/github/license/Pogromca-SCP/PluginCommands)](https://github.com/Pogromca-SCP/PluginCommands/blob/main/LICENSE)
![GitHub downloads](https://flat.badgen.net/github/assets-dl/Pogromca-SCP/PluginCommands)
![GitHub last commit](https://flat.badgen.net/github/last-commit/Pogromca-SCP/PluginCommands/main)

NwPluginAPI based plugin for SCP:Secret Laboratory. Provides custom commands for managing other NwPluginAPI based plugins from remote admin console at runtime.
 
This plugin was created using [official Northwood Plugin API](https://github.com/northwood-studios/NwPluginAPI). No additional dependencies need to be installed in order to run it.
 
## Installation
Simply put plugin `.dll` file inside your server's plugins folder.

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

## Additional build dependencies
The following SCP:SL assemblies must be referenced when building the project:
- `Assembly-CSharp.dll`
- `CommandSystem.Core.dll`
