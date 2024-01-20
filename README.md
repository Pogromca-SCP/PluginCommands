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
2. Place downloaded file in your server's plugins folder `{ServerDirectory}/PluginAPI/plugins/{port|global}`.
3. Restart the server.

## Commands
Commands from this plugin can be accessed from remote admin or server console.
| Command                              | Usage                              | Aliases           | Description                                                                                                                        | Required permissions  |
| ------------------------------------ | ---------------------------------- | ----------------- | ---------------------------------------------------------------------------------------------------------------------------------- | --------------------- |
| plugincommands <a name="plcmds"></a> | [load/reload/unload] [Plugin Name] | plcommands plcmds | Provides subcommands for plugins management at runtime. Displays the list of installed plugins if no valid subcommand is selected. | ServerConsoleCommands |

### [`plugincommands`](#plcmds) subcommands
| Command | Usage         | Aliases       | Description                  | Required permissions  |
| ------- | ------------- | ------------- | ---------------------------- | --------------------- |
| load    | [Plugin Name] | enable on     | Loads an installed plugin.   | ServerConsoleCommands |
| reload  | [Plugin Name] | refresh reset | Reloads an installed plugin. | ServerConsoleCommands |
| unload  | [Plugin Name] | disable off   | Unloads an installed plugin. | ServerConsoleCommands |

## Plugin integration
In order to make your own plugin work properly with commands you need to implement methods with `PluginEntryPoint` and `PluginUnload` attributes (example provided below). Never assume what is the state of your plugin when these methods are called! Your implementations should be able to handle every possible scenario.
```csharp
using PluginAPI.Core.Attributes;

class ExamplePlugin
{
    [PluginEntryPoint("Plugin name", "1.0.0", "Plugin description", "Plugin author")]
    void LoadPlugin()
    {
        // Initialize plugin here.
        // NOTE: Your plugin can be already loaded when this method is called.
    }

    [PluginUnload]
    void UnloadPlugin()
    {
        // Cleanup plugin resources here.
        // NOTE: Your plugin can be already unloaded when this method is called.
    }
}
```