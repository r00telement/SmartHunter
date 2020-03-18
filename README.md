# SmartHunter - Monster Hunter: World Overlay

A complete overlay for Monster Hunter: World on PC. Distributed on [Nexus Mods](https://www.nexusmods.com/monsterhunterworld/mods/2556). Features include:

- Monster widget - name, health, parts, status effect buildup, and crown.
- Team widget - name and damage meters.
- Player widget - buff, debuff, and equipment/mantle timers.
- Open source - freely learn from and contribute to the project.
- Skinnable - create and distribute your own rich styles and animations with XAML.
- Easy localization - create and distribute your own translations for our international friends.

## Changelog

- Look [here](https://forums.nexusmods.com/index.php?/topic/8356533-smarthunter-for-iceborne/)

## Requirements

- Windows.
- [The latest .NET Framework runtime](https://dotnet.microsoft.com/download/dotnet-framework-runtime).

## How to install

- Download `SmartHunter.exe` and `Newtonsoft.Json.dll` from [here](https://github.com/gabrielefilipp/SmartHunter/tree/master/SmartHunter/bin/Debug).
- Place them in a new folder.
- IMPORTANT: If you are replacing the new `.exe` in the same folder of the old SmartHunter please just follow the instructions `## How to update`.
- You're ready for the hunt.

## How to use

- Launch `SmartHunter.exe`.
- Hold `LeftAlt` to view widget locations.
- Click and drag widgets to move them.
- Scroll over widgets to rescale them.

## How to update

- Following this [commit](https://github.com/gabrielefilipp/SmartHunter/commit/305b8a55eb4cd40c31a06a30ab862b4f803baa84) I've added an AutoUpdater module to this Application.
- To enable this feature just set the key 'AutomaticallyCheckAndDownloadUpdates' to 'true', if present, otherwise just add it, to the file `Config.json`.
- NOTE that this feature is already active if it's the first time you follow the "How to install" steps and download the mentioned files in a new directory.
- To force an update just delete the file `Versions.json` and restart the application.

## How to create and use new localizations

- Create a copy of `en-US.json` and rename it for the locale you are translating to.
- Translate the strings in the second part of each key value pair. Do not change the keys.
- Ensure the new file is in the SmartHunter folder.
- Open `Config.json` and point `LocalizationFileName` to the new file.

## How to create and use new skins

- Create a copy of `Default.xaml` and rename it appropriately.
- Make changes to the new file.
- Ensure the new file is in the SmartHunter folder.
- Open `Config.json` and point `SkinFileName` to the new file.

## Credits

- [r00telement](https://www.nexusmods.com/monsterhunterworld/users/64406676) for he's original [project](https://github.com/r00telement/SmartHunter).
- [Phil-Pa](https://github.com/Phil-Pa) for he's [idea](https://github.com/gabrielefilipp/SmartHunter/pull/57/commits/4e2394e98ca63626b2ddcb4503a0ba0b0ed70c72)

## Disclaimer

Use with care and at your own risk. It is possible to get banned from games for using mods and overlays. There have been reports of people being banned from other games, such as PUBG, if overlays are accidentally left running in the background. To date, there have been no reports of players being banned from Monster Hunter: World for using mods and overlays, but that doesn't preclude it from happening in the future.
