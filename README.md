# FOA_Surround_Fix
Tainted Grail: The Fall of Avalon Surround Sound Fixes

## Notes
* This version of the fix is designed for use with the main branch of the game, compared to my old fix which required the mono branch.
* I Would always show caution about downloading a dll/binary from the internet, but have included a prebuilt dll on the releases page

## Explaination of Fix

* While searching through the decompiled code inside the mono build, I found that none of the FMOD platform profiles shipped with Tainted Grail have a SpeakerMode being set, including the fallback Default profile. With no SpeakerMode being set at all, this causes the game to fall back to the FMOD default, of Stereo.
* NOTE: FMOD will automatically downmix to your systems channel output, so its safe to just specify SPEAKERMODE._7POINT1

## Requirements
* bepinex (6.x / be)
** https://builds.bepinex.dev/projects/bepinex_be
** As of 20260325 I've tested against 
*** https://builds.bepinex.dev/projects/bepinex_be/755/BepInEx-Unity.IL2CPP-win-x64-6.0.0-be.755%2B3fab71a.zip

* dotnet if you're looking to compile the dll yourself.
** https://dotnet.microsoft.com/en-us/download

## TLDR

* If you want a binary, please see the releases page.
* If you want to build yourself, please see the steps below

## FOA_Surround_Fix.dll

* Download latest version of BepInEx (IL2CPP) from their builds page, and uncompress the package to your $(path-to-game) directory.
* Download latest FOA_Surround_fix.dll from https://github.com/chrastoph/FOA_Surround_Fix/releases
* Jump to common below

## If you Want to Build the dll yourself

* Download latest version of BepInEx (IL2CPP) from their builds page, and uncompress the package to your $(path-to-game) directory.
* Check out the git repository to a $(git-path) directory of your chosing
* Run The game at least once expect a longer than normal startup time, this will populate files into $(path-to-game)/BepInEx/interop
* copy $(path-to-game)/BepInEx/interop/FMODUnity.dll to $(git-path)/lib/
* Make sure you have dotnet runtime, and sdk installed
* run `dotnet build` in the $(git-path) directory
* Assuming no compile errors, you should now have a $(git-path)/bin/Debug/net6.0/FOA_Surround_Fix.dll
* Jump to common below

## Common
* Drop FOA_Surround_fix.dll into $(path-to-game)/BepInEx/plugins/
* Start the game
* BepinInEx logs should look something like this to verify if the plugin is working.

```
[Info   :   BepInEx] Loading [Tainted Grail: Fall of Avalon - Surround Sound Fixes 1.0.0]
[Info   :Tainted Grail: Fall of Avalon - Surround Sound Fixes] Plugin FOA_Surround_Fix loaded -- version:1.0.0 Chris Andrews @ 20260325084400
[Info   :Tainted Grail: Fall of Avalon - Surround Sound Fixes] Initializing HarmonyX - Starting
[Info   :Tainted Grail: Fall of Avalon - Surround Sound Fixes] Initializing HarmonyX - Complete
[Info   :Tainted Grail: Fall of Avalon - Surround Sound Fixes] Found candidate: FMOD.RESULT setSoftwareFormat(Int32, FMOD.SPEAKERMODE, Int32)
[Message:   BepInEx] Chainloader startup complete
[Info   :Tainted Grail: Fall of Avalon - Surround Sound Fixes] PreFix Method Trigger for Harmony
[Info   :Tainted Grail: Fall of Avalon - Surround Sound Fixes] Current Speaker Mode: STEREO
[Info   :Tainted Grail: Fall of Avalon - Surround Sound Fixes] PostFix Method Trigger for Harmony
[Info   :Tainted Grail: Fall of Avalon - Surround Sound Fixes] Final Speaker Mode: _7POINT1
```
