using BepInEx;
using BepInEx.Configuration;
using BepInEx.Unity.Mono;
using HarmonyLib;
using System.Reflection;

namespace FOA_Surround_Fix;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
  private ConfigEntry<string> SpeakerModeConfig;
  public static ConfigEntry<string> OutputTypeConfig;

  private void Awake()
  {
    // Plugin startup logic
    PluginLog.Info = Logger.LogInfo;
    PluginLog.Warning = Logger.LogWarning;
    PluginLog.Error = Logger.LogError;

    PluginLog.Info($"Plugin {MyPluginInfo.PLUGIN_GUID} (Mono) loaded -- version:{MyPluginInfo.PLUGIN_VERSION} {BuildInfo.Info.PluginAuthor} @ {BuildInfo.Info.LastModified}");

    SpeakerModeConfig = Config.Bind("Audio","SpeakerMode","7.1","Speaker mode: Stereo, 5.1, 7.1");
    PluginConfig.SpeakerMode = SpeakerModeConfig.Value;

    OutputTypeConfig = Config.Bind("Audio","OutputType","WASAPI","Output Type: WASAPI, WINSONIC");
    PluginConfig.OutputType = OutputTypeConfig.Value;

    // harmony init
    var harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
    PluginLog.Info("Initializing HarmonyX - Starting");
    harmony.PatchAll();
    PluginLog.Info("Initializing HarmonyX - Complete");

    foreach (var m in typeof(FMOD.System).GetMethods(
      System.Reflection.BindingFlags.Public |
      System.Reflection.BindingFlags.NonPublic |
      System.Reflection.BindingFlags.Instance |
      System.Reflection.BindingFlags.Static))
    {
      if (m.Name.Contains("setSoftwareFormat"))
      {
        PluginLog.Info("FMOD Software Formats: Found candidates: " + m.ToString());
      }
      else if (m.Name == "setOutput")
      {
        PluginLog.Info("FMOD Outputs: Found candidates: " + m.ToString());
      }
    }
  }
}

