using HarmonyLib;
using GameNetcodeStuff;

namespace BestTeleporter
{
    [HarmonyPatch]
    internal class TeleporterPatch
    {
        [HarmonyPatch(typeof(TeleporterScript), "TeleportPlayer")]
        [HarmonyPrefix]
        static bool PreventItemDropTeleporter(PlayerControllerB playerScript)
        {
            // 일반 텔레포터 아이템 드롭 방지
            playerScript.isHoldingObject = false;
            return true;
        }

        [HarmonyPatch(typeof(TeleporterScript), "TeleportPlayer")]
        [HarmonyPostfix]
        static void RestoreItemHoldingTeleporter(PlayerControllerB playerScript)
        {
            // 일반 텔레포터 아이템 상태 복구
            playerScript.isHoldingObject = true;
        }

        [HarmonyPatch(typeof(InverseTeleporterItem), "TeleportPlayerOutside")]
        [HarmonyPrefix]
        static bool PreventItemDropInverse(PlayerControllerB playerScript)
        {
            // 역방향 텔레포터 아이템 드롭 방지
            playerScript.isHoldingObject = false;
            return true;
        }

        [HarmonyPatch(typeof(InverseTeleporterItem), "TeleportPlayerOutside")]
        [HarmonyPostfix]
        static void RestoreItemHoldingInverse(PlayerControllerB playerScript)
        {
            // 역방향 텔레포터 아이템 상태 복구
            playerScript.isHoldingObject = true;
        }

        // 일반 텔레포터 쿨다운 제거
        [HarmonyPatch(typeof(TeleporterScript), "Start")]
        [HarmonyPostfix]
        static void RemoveTeleporterCooldown(TeleporterScript __instance)
        {
            __instance.cooldownAmount = 0f;
        }

        // 역방향 텔레포터 쿨다운 제거
        [HarmonyPatch(typeof(InverseTeleporterItem), "Start")]
        [HarmonyPostfix]
        static void RemoveInverseTeleporterCooldown(InverseTeleporterItem __instance)
        {
            __instance.cooldownTime = 0f;
        }
    }
} 