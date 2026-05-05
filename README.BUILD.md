# Build Instructions

## Requirements

* bepinex (6.x / be)
  * For Main/Mono
  * https://builds.bepinex.dev/projects/bepinex_be
  * As of 20260505 I've tested against
    * https://builds.bepinex.dev/projects/bepinex_be/755/BepInEx-Unity.IL2CPP-win-x64-6.0.0-be.755%2B3fab71a.zip
    * https://builds.bepinex.dev/projects/bepinex_be/755/BepInEx-Unity.Mono-win-x64-6.0.0-be.755%2B3fab71a.zip
* bepinex (5.x)
  * For Mono users who want to use other plugins from Nexusmods
  * https://github.com/bepinex/bepinex/releases
  * As of 20260505 I've tested against
    * https://github.com/BepInEx/BepInEx/releases/download/v5.4.23.5/BepInEx_linux_x64_5.4.23.5.zip

* dotnet if you're looking to compile the dll yourself.
  * https://dotnet.microsoft.com/en-us/download

## Build Steps

* Download latest version of BepInEx (IL2CPP/Mono) from their build/github page, and uncompress the package to your `$(path-to-game)` directory.
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
  * Note: This requires a lot of extra libs to be pulled in
    * In BepInEx6 this is done via NuGet
    * In BepInEx5 this is done via references to the libraries provided by BepInEx / Tainted Grail
  * copy `$(path-to-game)/Fall of Avalon_Data/Managed/FMODUnity.dll` to `$(git-path)/lib/Mono_BepInEx5/`
  * copy `$(path-to-game)/Fall of Avalon_Data/Managed/UnityEngine.dll` to `$(git-path)/lib/Mono_BepInEx5/`
  * copy `$(path-to-game)/Fall of Avalon_Data/Managed/UnityEngine.CoreModule.dll` to `$(git-path)/lib/Mono_BepInEx5/`
  * copy `$(path-to-game)/BepInEx/core/0Harmony.dll` to `$(git-path)/lib/Mono_BepInEx5/`
  * copy `$(path-to-game)/BepInEx/core/BepInEx.dll` to `$(git-path)/lib/Mono_BepInEx5/`
  * run `dotnet build` in the `$(git-path)/Mono_BepInEx5/` directory
  * Assuming no compile errors, you should now have a `$(git-path)/.artifacts/mod.Mono_bepinex5/bin/Debug/netstandard2.1/FOA_Surround_Fix.bepinex5.Mono.dll`
* Both
  * For what its worth, I added in a solution to compile both dlls for BepInEx6
  * Requires both versions of FMODUnity.dll to be populated into `$(git-path)/lib/IL2CPP/` and `$(git-path)/lib/Mono/`
  * `dotnet build FOA_Surround_Fix.slnx`
* Follow Deployment Steps in README.md

