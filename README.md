# Tainted Grail: The Fall of Avalon -- Surround Sound Fixes

## Notes
* This version of the fix supports both the main branch, as well as the mono branch of the game. 
  * If you are using BepInEx 5 with the Mono Branch, there is a dll for you as well.
* I Would always show caution about downloading a dll/binary from the internet, but have included a prebuilt dll on the releases page
* Defaults to 5.1, if you're 7.1 update the config file
* Dolby Atmos / Winsonic
  * The plugin now supports forcing WINSONIC inside of FMOD in the plugin config.
  * You need to have Dolby Atmos / Winsonic enabled in your Windows sound settings.
  * If enabling Dolby Atmos, it may be beneficial to set the SpeakerMode to 7.1.4 in the plugin config.
  * I'm not actually sure if this is just putting a 7.1.4 bed in a dolby atmos stream or if its truely object based.
  * I haven't really tested this well, I only have a 9 channel receiver at the moment, and my overheads aren't connectedanymore, and I cant really tell with my receviers virtual atmos that well with the limited testing ive done.
  * Eventually I'll find my dac and headphones to test this properly. 
* Linux Users
  * In order to load bepinex you need to allow dll overrides with a command line update of something like `WINEDLLOVERRIDES="winhttp=n,b" %command%`

## Explaination of Fix

* While searching through the decompiled code inside the mono build, I found that none of the FMOD platform profiles shipped with Tainted Grail have a SpeakerMode being set, including the fallback Default profile. With no SpeakerMode being set at all, this causes the game to fall back to the FMOD default, of Stereo.
* This dll injection just overrides the platform lookup for setting the speakermode, and instead just hard codes `SPEAKERMODE._5POINT1`
* NOTE: Previously the plugin defaulted to _7POINT1 and we allowed FMOD to downmix to 5.1, but its been reported that the surround sound quality is better at 5.1 by forcing 5.1 directly.
* NOTE: FMOD will automatically downmix to your systems channel output, so its safe to just specify `SPEAKERMODE._7POINT1`
  * See https://www.fmod.com/docs/2.02/api/mixing-and-routing-in-the-core-api.html#upmixdownmix-behavior for additional information

## Requirements
* bepinex (6.x / be)
  * https://builds.bepinex.dev/projects/bepinex_be
  * As of 20260504 I've tested against 
    * https://builds.bepinex.dev/projects/bepinex_be/755/BepInEx-Unity.IL2CPP-win-x64-6.0.0-be.755%2B3fab71a.zip
    * https://builds.bepinex.dev/projects/bepinex_be/755/BepInEx-Unity.Mono-win-x64-6.0.0-be.755%2B3fab71a.zip
* bepinex (5.x)
  * https://github.com/bepinex/bepinex/releases
  * As of 20260504 I've tested against
    * https://github.com/BepInEx/BepInEx/releases/download/v5.4.23.5/BepInEx_linux_x64_5.4.23.5.zip 

* dotnet if you're looking to compile the dll yourself.
  * https://dotnet.microsoft.com/en-us/download

## TLDR

* If you want a binary, please see the releases page.
* If you want to build yourself, please see the steps below

## FOA_Surround_Fix.IL2CPP.dll / FOA_Surround_Fix.Mono.dll 

* Download latest version of BepInEx, from their builds page, and uncompress the package to your `$(path-to-game)` directory.
  * For Main Branch you want the IL2CPP release (BepInEx-Unity.IL2CPP-win-x64-6.0.0-be.755%2B3fab71a.zip)
  * For Mono Branch you want the Mono Release (BepInEx-Unity.Mono-win-x64-6.0.0-be.755%2B3fab71a.zip)
* Download latest release from https://github.com/chrastoph/FOA_Surround_Fix/releases
  * FOA_Surround_Fix.IL2CPP.dll for the main branch of the game
  * FOA_Surround_Fix.Mono.dll for the mono branch of the game

* Jump to common below

## If you Want to Build the dll yourself

* Download latest version of BepInEx (IL2CPP/Mono) from their builds page, and uncompress the package to your `$(path-to-game)` directory.
* Check out the git repository to a `$(git-path)` directory of your chosing
* Make sure you have dotnet runtime, and sdk installed
* Main Branch - IL2CPP
  * Run The game at least once. Expect a longer than normal startup time, this will populate files into $(path-to-game)/BepInEx/interop which are needed to compile the dll
  * copy `$(path-to-game)/BepInEx/interop/FMODUnity.dll` to `$(git-path)/lib/IL2CPP/`
  * run `dotnet build` in the `$(git-path)/IL2CPP/` directory
  * Assuming no compile errors, you should now have a `$(git-path)/.artifacts/mod.IL2CPP/bin/Debug/net6.0/FOA_Surround_Fix.IL2CPP.dll`
* Mono Branch - MONO
  * copy `$(path-to-game)/Fall of Avalon_Data/Managed/FMODUnity.dll` to `$(git-path)/lib/Mono/`
  * run `dotnet build` in the `$(git-path)/Mono/` directory
  * Assuming no compile errors, you should now have a `$(git-path)/.artifacts/mod.Mono/bin/Debug/netstandard2.1/FOA_Surround_Fix.mono.dll`
* Mono Branch for BepInEx 5
  * Note: This requires a lot of extra libs to be pulled in, in BepInEx6 this is done via NuGet
  * copy `$(path-to-game)/Fall of Avalon_Data/Managed/FMODUnity.dll` to `$(git-path)/lib/Mono_BepInEx5/`
  * copy `$(path-to-game)/Fall of Avalon_Data/Managed/UnityEngine.dll` to `$(git-path)/lib/Mono_BepInEx5/`
  * copy `$(path-to-game)/Fall of Avalon_Data/Managed/UnityEngine.CoreModule.dll` to `$(git-path)/lib/Mono_BepInEx5/`
  * copy `$(path-to-game)/BepInEx/core/0Harmony.dll` to `$(git-path)/lib/Mono_BepInEx5/`
  * copy `$(path-to-game)/BepInEx/core/BepInEx.dll` to `$(git-path)/lib/Mono_BepInEx5/`
  * run `dotnet build` in the `$(git-path)/Mono_BepInEx5/` directory
  * Assuming no compile errors, you should now have a `$(git-path)/.artifacts/mod.Mono_bepinex5/bin/Debug/netstandard2.1/FOA_Surround_Fix.bepinex5.Mono.dll`
* Both
  * For what its worth, I added in a solution to compile both dlls for BepInEx6
  * Required both versions of FMODUnity.dll to be populated into `$(git-path)/lib/IL2CPP/` and `$(git-path)/lib/Mono/`
  * `dotnet build FOA_Surround_Fix.slnx`
* Jump to common below

## Common
* Copy Your compiled DLL into `$(path-to-game)/BepInEx/plugins/`
  * For Main Branch: `FOA_Surround_fix.IL2CPP.dll`
  * For Mono Branch: `FOA_Surround_fix.Mono.dll`
  * For Mono Branch + BepInEx5: `FOA_Surround_fix.bepinex5.Mono.dll`
* Start the game
* BepInEx logs should look something like this to verify if the plugin is working.
* Config
  * BepInEx Will automatically create a config file in `$(path-to-game)/BepInEx/config/` after its first run
  * Config can be modified with a text editor of your choice, or with the BepInEx configuration manager in game.
    * Main Branch - FOA_Surround_Fix.IL2CPP.cfg
    * Mono Branch - FOA_Surround_Fix.Mono.cfg
    * Mono Branch + BepInEx5 - FOA_Surround_Fix.bepinex5.Mono.cfg
  * The Following Settings can be changed
    * `SpeakerMode=$(x)`
      * `Options: Stereo, 2.0, Quad, 4.0, Surround, 5.1 (default), 7.1, 7.1.4`
      * You may get better positional sound by exactly matching your speaker layout.
    * `OutputType=$(x)`
      * `Options: WASAPI (default), WINSONIC`
      * When using Dolby Atmos it is recommended by Dolby to use 7.1.4, but who knows, testing needed.
        * https://professional.dolby.com/gaming/gaming-getting-started/dolby-atmos-documentation/#atmos
  * Since the plugin taps into FMOD during initialization, After changing the config, a game restart is required.

```
[Info   :   BepInEx] Loading [Tainted Grail: Fall of Avalon - Surround Sound Fixes - Mono Branch 1.1.0]
[Info   :Tainted Grail: Fall of Avalon - Surround Sound Fixes - Mono Branch] Plugin FOA_Surround_Fix.Mono (Mono) loaded -- version:1.1.0 Chris Andrews @ 20260502042800
[Info   :Tainted Grail: Fall of Avalon - Surround Sound Fixes - Mono Branch] Initializing HarmonyX - Starting
[Info   :Tainted Grail: Fall of Avalon - Surround Sound Fixes - Mono Branch] Patch - SetSoftwareFormatPatch Loaded
[Info   :Tainted Grail: Fall of Avalon - Surround Sound Fixes - Mono Branch] Initializing HarmonyX - Complete
[Info   :Tainted Grail: Fall of Avalon - Surround Sound Fixes - Mono Branch] FMOD Outputs: Found candidates: FMOD.RESULT setOutput(FMOD.OUTPUTTYPE)
[Info   :Tainted Grail: Fall of Avalon - Surround Sound Fixes - Mono Branch] FMOD Software Formats: Found candidates: FMOD.RESULT setSoftwareFormat(Int32, FMOD.SPEAKERMODE, Int32)
[Info   :Tainted Grail: Fall of Avalon - Surround Sound Fixes - Mono Branch] Current FMOD Speaker Mode: STEREO
[Info   :Tainted Grail: Fall of Avalon - Surround Sound Fixes - Mono Branch] Config FMOD SpeakerMode: 7.1
[Info   :Tainted Grail: Fall of Avalon - Surround Sound Fixes - Mono Branch] Current FMOD OutputType: WASAPI
[Info   :Tainted Grail: Fall of Avalon - Surround Sound Fixes - Mono Branch] Config FMOD OutputType: WINSONIC
[Info   :Tainted Grail: Fall of Avalon - Surround Sound Fixes - Mono Branch] Forcing FMOD OutputType to: WINSONIC
[Info   :Tainted Grail: Fall of Avalon - Surround Sound Fixes - Mono Branch] Final FMOD Speaker Mode: _7POINT1
[Info   :Tainted Grail: Fall of Avalon - Surround Sound Fixes - Mono Branch] Final FMOD OutputType: WINSONIC
[Info   :Tainted Grail: Fall of Avalon - Surround Sound Fixes - Mono Branch] FMOD Format Parameters: rate:[48000] mode:[_7POINT1] raw:[8]
```
