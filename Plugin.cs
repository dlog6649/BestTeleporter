using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace BestTeleporter
{
  [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
  public class Plugin : BaseUnityPlugin
  {
    private readonly Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);
    private static ManualLogSource mls;

    private void Awake()
    {
      mls = BepInEx.Logging.Logger.CreateLogSource(PluginInfo.PLUGIN_GUID);
      mls.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} has been loaded!");

      // Apply all patches
      harmony.PatchAll(typeof(Plugin));
      harmony.PatchAll(typeof(TeleporterPatch));
      
      mls.LogInfo("Teleporter item drop prevention and cooldown removal patches have been applied.");
    }
  }
}