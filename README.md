# Tainted Grail: The Fall of Avalon -- Surround Sound Fix (5.1 / 7.1 / Spatial Audio)

## Overview
* Fixes the Tainted Grail: Fall of Avalon audio bug, where game only runs in 2-channel/Stereo
* By default, mod forces on 7.1 Surround Sound, and Optionally allows one to enable Dolby Atmos for Headphones/Home Theater
  
## Detailed Overview
* Last Tested with Game Version 1.25
* This version of the fix supports both the main branch (bepinex 6), as well as the mono branch (bepinex 5/6) of the game. 
* I Would always show caution about downloading a dll/binary from the internet, but have included a prebuilt dll on the releases page
* Sets Defaults SpeakerMode to 7.1
  * It has been suggested that you can achieve slightly better positional audio by more directly matching your actual speaker layout
  * To that end, if you're 4.0 or 5.1 you may want to specify that in the plugin config for optimal surround sound quality.
* Dolby Atmos / Winsonic
  * The plugin now supports forcing WINSONIC inside of FMOD in the plugin config.
  * You need to have Dolby Atmos / Winsonic enabled in your Windows sound settings.
  * If enabling Dolby Atmos, it may be beneficial to set the SpeakerMode to 7.1.4 in the plugin config.
  * I'm not actually sure if this is just putting a 7.1.4 bed in a dolby atmos stream or if its truely object based.
  * I haven't really tested this well, I only have a 9 channel receiver at the moment, and my overheads aren't connectedanymore, and I cant really tell with my receviers virtual atmos that well with the limited testing ive done.
  * Eventually I'll find my dac and headphones to test this properly. 
* Linux Users
  * In order to load bepinex you need to allow dll overrides with a command line update of something like `WINEDLLOVERRIDES="winhttp=n,b" %command%`
  * If you're a headphones user and utilize pipewire, try out IrateGoose to transform the 7.1 stream into a binaural 2.0 (Thanks RealKodiJack for the suggestion)
    * https://github.com/Barafu/IrateGoose
* Nexusmods - I've uploaded the plugin to nexusmods for users looking for a more managed install process
  * https://www.nexusmods.com/taintedgrailthefallofavalon/mods/149

## Explaination of Fix

* While searching through the decompiled code inside the mono build, I found that none of the FMOD platform profiles shipped with Tainted Grail have a SpeakerMode being set, including the fallback Default profile. With no SpeakerMode being set at all, this causes the game to fall back to the FMOD default, of Stereo.
* This dll injection just overrides the platform lookup for setting the speakermode, and instead just hard codes `SPEAKERMODE._7POINT1`
* NOTE: FMOD will automatically downmix to your systems channel output, so its safe to just specify `SPEAKERMODE._7POINT1`
  * See https://www.fmod.com/docs/2.02/api/mixing-and-routing-in-the-core-api.html#upmixdownmix-behavior for additional information

## Requirements

* Tainted Grail: The Fall of Avalon
  * Last Tested Against: 1.23b
* bepinex (6.x / be)
  * For Main/Mono
  * https://builds.bepinex.dev/projects/bepinex_be
  * As of 20260504 I've tested against 
    * https://builds.bepinex.dev/projects/bepinex_be/755/BepInEx-Unity.IL2CPP-win-x64-6.0.0-be.755%2B3fab71a.zip
    * https://builds.bepinex.dev/projects/bepinex_be/755/BepInEx-Unity.Mono-win-x64-6.0.0-be.755%2B3fab71a.zip
* bepinex (5.x)
  * For Mono users who want to use other plugins from Nexusmods
  * https://github.com/bepinex/bepinex/releases
  * As of 20260504 I've tested against
    * https://github.com/BepInEx/BepInEx/releases/download/v5.4.23.5/BepInEx_linux_x64_5.4.23.5.zip 

* dotnet if you're looking to compile the dll yourself.
  * https://dotnet.microsoft.com/en-us/download

## If you Want to Build the dll yourself

* See README.BUILD.md

## Installation

### Nexusmods

* For Automated Installs via Vortex, please see https://www.nexusmods.com/taintedgrailthefallofavalon/mods/149
* See Config Section Below for additional settings.
* See Verification Section Below for validation

### Manual Installs
#### Chosing Which DLL to install
* FOA_Surround_Fix.IL2CPP.dll 
  * Intended for use with the Main branch of Tainted Grail - Required BepInEx6
* FOA_Surround_Fix.Mono.dll
  * Intended for use with the Mono branch of Tainted Grail - Requires BepInEx6
* FOA_Surround_Fix.bepinex5.Mono.dll
  * Intended for use with the Mono branch of Tainted Grail - Requires BepInEx5
  * Targeting users who already have a BepInEx5 deployment, or who are interested in using other plugins from Nexusmods.

#### Installation Steps

* Download BepInEx from either builds.bepinex.dev (BepInEx6), or https://github.com/bepinex/bepinex/releases (BepInEx5)
  * For Main you want the IL2CPP release (BepInEx-Unity.IL2CPP-win-x64-6.0.0-be.755%2B3fab71a.zip)
  * For Mono/BepInEx6 you want the Mono release (BepInEx-Unity.Mono-win-x64-6.0.0-be.755%2B3fab71a.zip)
  * For Mono/BepInEx5 you want the Mono release (BepInEx_linux_x64_5.4.23.5.zip)
* Uncompress BepInEx archive to `$(path-to-game)`
* Download latest dll from https://github.com/chrastoph/FOA_Surround_Fix/releases 
* Copy your DLL into `$(path-to-game)/BepInEx/plugins/`
* Start the game
  * Expect a longer then normal startup time when starting with BepInEx for the first time.
* BepInEx logs should look something like this to verify if the plugin is working.

### Config

* Config
  * BepInEx Will automatically create a config file in `$(path-to-game)/BepInEx/config/` after its first run
  * Config can be modified with a text editor of your choice, or with the BepInEx configuration manager in game.
    * Main Branch - FOA_Surround_Fix.IL2CPP.cfg
    * Mono Branch - FOA_Surround_Fix.Mono.cfg
    * Mono Branch + BepInEx5 - FOA_Surround_Fix.bepinex5.Mono.cfg
  * The Following Settings can be changed
    * `SpeakerMode=$(x)`
      * `Options: Stereo, 2.0, Quad, 4.0, Surround, 5.1, 7.1 (default), 7.1.4`
      * You may get better positional sound by exactly matching your speaker layout.
    * `OutputType=$(x)`
      * `Options: WASAPI (default), WINSONIC`
      * When using Dolby Atmos it is recommended by Dolby to use 7.1.4, but who knows, testing needed.
        * https://professional.dolby.com/gaming/gaming-getting-started/dolby-atmos-documentation/#atmos
  * Since the plugin taps into FMOD during initialization, After changing the config, a game restart is required.

### Verification

* If everything works as intended $(game-path)/BepInEx/LogOutput.log should contain something like

```
[Info   :   BepInEx] Loading [Tainted Grail: Fall of Avalon - Surround Sound Fixes - Main Branch 1.2.0]
[Info   :Tainted Grail: Fall of Avalon - Surround Sound Fixes - Main Branch] Plugin FOA_Surround_Fix.IL2CPP (IL2CPP) loaded -- version:1.2.0 Chris Andrews @ 20260505094100
[Info   :Tainted Grail: Fall of Avalon - Surround Sound Fixes - Main Branch] Initializing HarmonyX - Starting
[Info   :Tainted Grail: Fall of Avalon - Surround Sound Fixes - Main Branch] Patch - SetSoftwareFormatPatch Loaded
[Info   :Tainted Grail: Fall of Avalon - Surround Sound Fixes - Main Branch] Initializing HarmonyX - Complete
[Info   :Tainted Grail: Fall of Avalon - Surround Sound Fixes - Main Branch] Current FMOD Speaker Mode: STEREO
[Info   :Tainted Grail: Fall of Avalon - Surround Sound Fixes - Main Branch] Config FMOD SpeakerMode: 7.1.4
[Info   :Tainted Grail: Fall of Avalon - Surround Sound Fixes - Main Branch] Current FMOD OutputType: WASAPI
[Info   :Tainted Grail: Fall of Avalon - Surround Sound Fixes - Main Branch] Config FMOD OutputType: WINSONIC
[Info   :Tainted Grail: Fall of Avalon - Surround Sound Fixes - Main Branch] Final FMOD Speaker Mode: _7POINT1POINT4
[Info   :Tainted Grail: Fall of Avalon - Surround Sound Fixes - Main Branch] Final FMOD OutputType: WINSONIC
[Info   :Tainted Grail: Fall of Avalon - Surround Sound Fixes - Main Branch] FMOD Format Parameters: rate:[48000] mode:[_7POINT1POINT4] raw:[12]
```
