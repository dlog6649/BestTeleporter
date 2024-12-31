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
            mls.LogInfo($"플러그인 {PluginInfo.PLUGIN_GUID} 이(가) 로드되었습니다!");

            // 모든 패치 적용
            harmony.PatchAll(typeof(Plugin));
            harmony.PatchAll(typeof(TeleporterPatch));
            
            mls.LogInfo("텔레포터 아이템 드롭 방지 패치가 적용되었습니다.");
        }
    }
} 