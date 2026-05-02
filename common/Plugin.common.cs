using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace FOA_Surround_Fix;

public static class PluginLog
{
  public static Action<string> Info = _ => {};
  public static Action<string> Warning = _ => {};
  public static Action<string> Error = _ => {};
}

public static class PluginConfig
{
  // some defaults
  public static string SpeakerMode = "7.1";
  public static string OutputType = "WASAPI";
}

[HarmonyPatch]
public static class SetSoftwareFormatPatch
{
  [HarmonyTargetMethod]
  static MethodBase TargetMethod()
  {
    PluginLog.Info("Patch - SetSoftwareFormatPatch Loaded");
    var method = AccessTools.Method(typeof(FMOD.System), "setSoftwareFormat", new[] { typeof(int), typeof(FMOD.SPEAKERMODE), typeof(int) } );

    return method;
  }

  [HarmonyPrefix]
  // public static void Prefix(ref FMOD.SPEAKERMODE speakermode)
  public static void Prefix(FMOD.System __instance, ref FMOD.SPEAKERMODE speakermode)
  {
    // SpeakerMode
    PluginLog.Info("Current FMOD Speaker Mode: " + speakermode);
    PluginLog.Info("Config FMOD SpeakerMode: " + PluginConfig.SpeakerMode);

    switch (PluginConfig.SpeakerMode.Trim().ToLowerInvariant())
    {
      case "stereo":
        speakermode = FMOD.SPEAKERMODE.STEREO;
        break;
      case "5.1":
        speakermode = FMOD.SPEAKERMODE._5POINT1;
        break;
      case "7.1":
      default:
        speakermode = FMOD.SPEAKERMODE._7POINT1;
        break;
    }

    // Atmos Stuff
    FMOD.OUTPUTTYPE output;
    __instance.getOutput(out output);

    PluginLog.Info("Current FMOD OutputType: " + output.ToString());
    PluginLog.Info("Config FMOD OutputType: " + PluginConfig.OutputType);
    switch (PluginConfig.OutputType.Trim())
    {
      case "WINSONIC":
        FMOD.RESULT result = __instance.setOutput(FMOD.OUTPUTTYPE.WINSONIC);
        break;
      case "WASAPI":
      default:
        // do nothing
        break;
    }

  }
  [HarmonyPostfix]
  public static void Postfix(FMOD.System __instance, ref FMOD.SPEAKERMODE speakermode)
  {
    // Speakermode
    PluginLog.Info("Final FMOD Speaker Mode: " + speakermode);

    // Atmos Stuff
    FMOD.OUTPUTTYPE output;

    __instance.getOutput(out output);
    PluginLog.Info("Final FMOD OutputType: " + output.ToString());

    FMOD.SPEAKERMODE mode;
    int raw;

    __instance.getSoftwareFormat(out int rate, out mode, out raw);
    PluginLog.Info($"FMOD Format Parameters: rate:[{rate}] mode:[{mode}] raw:[{raw}]");
  }
}

