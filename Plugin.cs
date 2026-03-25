using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace FOA_Surround_Fix;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BasePlugin
{
    internal static new ManualLogSource Log;

    public override void Load()
    {
      // Plugin startup logic
      Log = base.Log;
      Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} loaded -- version:{MyPluginInfo.PLUGIN_VERSION} {BuildInfo.Info.PluginAuthor} @ {BuildInfo.Info.LastModified}");

      // harmony init
      var harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
      Log.LogInfo("Initializing HarmonyX - Starting");
      harmony.PatchAll();
      Log.LogInfo("Initializing HarmonyX - Complete");

      foreach (var m in typeof(FMOD.System).GetMethods(
        System.Reflection.BindingFlags.Public |
        System.Reflection.BindingFlags.NonPublic |
        System.Reflection.BindingFlags.Instance |
        System.Reflection.BindingFlags.Static))
      {
        if (m.Name.Contains("setSoftwareFormat"))
        {
          Log.LogInfo("Found candidate: " + m.ToString());
        }
      }
    }


    [HarmonyPatch]
    public static class SetSoftwareFormatPatch
    {
      [HarmonyTargetMethod]
      static MethodBase TargetMethod()
      {
        return AccessTools.Method(typeof(FMOD.System), "setSoftwareFormat", new[] { typeof(int), typeof(FMOD.SPEAKERMODE), typeof(int) } );
        // return AccessTools.Method(typeof(FMOD.System), "setSoftwareFormat", new[] { typeof(FMOD.SPEAKERMODE), typeof(int) } );
      }

      static FMOD.SPEAKERMODE ForcedSpeakerMode(FMODUnity.Platform platform)
      {
        Plugin.Log.LogInfo("Forcing FMOD speaker mode to 7.1");
        return FMOD.SPEAKERMODE._7POINT1;
      }

      [HarmonyPrefix]
      public static void Prefix(ref FMOD.SPEAKERMODE speakermode)
      {
        Plugin.Log.LogInfo("PreFix Method Trigger for Harmony");
        Plugin.Log.LogInfo("Current Speaker Mode: " + speakermode);
        speakermode = FMOD.SPEAKERMODE._7POINT1;
      }
      [HarmonyPostfix]
      public static void Postfix(ref FMOD.SPEAKERMODE speakermode)
      {
        Plugin.Log.LogInfo("PostFix Method Trigger for Harmony");
        Plugin.Log.LogInfo($"Final Speaker Mode: " + speakermode);
      }
    }

}

