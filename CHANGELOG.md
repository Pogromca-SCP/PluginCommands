# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [4.0.0] - 2025-05-20

### Added

- Added separate subcommand for plugin config reloading.

### Changed

- Upgraded project to [LabAPI v1.0.2](https://github.com/northwood-studios/LabAPI/releases/tag/1.0.2).
- Reload plugin subcommand now also reloads plugin configuration.

## [3.1.0] - 2024-06-10

### Changed

- Upgraded project to [NwPluginAPI v13.1.2](https://github.com/northwood-studios/NwPluginAPI/releases/tag/13.1.2).
- Enabled commands response sanitization.
- Plugin bind now has explicit access modifier.

## [3.0.0] - 2023-09-16

### Changed

- Upgraded project to NwPluginAPI v13.1.1.

## [2.2.0] - 2023-07-29

### Changed

- String builders are now rented from a pool.

## [2.1.0] - 2023-05-09

### Fixed

- Messages in grey color should now display correctly.

## [2.0.0] - 2023-04-16

### Changed

- Updated project structure and dependencies handling.

## [1.1.2] - 2023-03-30

### Changed

- Server console is now detected by sender type instead of nickname.

## [1.1.1] - 2023-03-28

### Fixed

- Missing permissions error response now displays missing permission names.

## [1.1.0] - 2023-03-14

### Added

- Support for server console.

## [1.0.1] - 2023-03-14

### Changed

- Added plugins authors to installed plugins list display.

## [1.0.0] - 2023-03-13

### Added

- Initial plugin version made with [NwPluginAPI v12.0.0](https://github.com/northwood-studios/NwPluginAPI/releases/tag/12.0.0).
