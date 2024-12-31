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
            // Prevent item dropping for regular teleporter
            playerScript.isHoldingObject = false;
            return true;
        }

        [HarmonyPatch(typeof(TeleporterScript), "TeleportPlayer")]
        [HarmonyPostfix]
        static void RestoreItemHoldingTeleporter(PlayerControllerB playerScript)
        {
            // Restore item holding state for regular teleporter
            playerScript.isHoldingObject = true;
        }

        [HarmonyPatch(typeof(InverseTeleporterItem), "TeleportPlayerOutside")]
        [HarmonyPrefix]
        static bool PreventItemDropInverse(PlayerControllerB playerScript)
        {
            // Prevent item dropping for inverse teleporter
            playerScript.isHoldingObject = false;
            return true;
        }

        [HarmonyPatch(typeof(InverseTeleporterItem), "TeleportPlayerOutside")]
        [HarmonyPostfix]
        static void RestoreItemHoldingInverse(PlayerControllerB playerScript)
        {
            // Restore item holding state for inverse teleporter
            playerScript.isHoldingObject = true;
        }

        // Remove regular teleporter cooldown
        [HarmonyPatch(typeof(TeleporterScript), "Start")]
        [HarmonyPostfix]
        static void RemoveTeleporterCooldown(TeleporterScript __instance)
        {
            __instance.cooldownAmount = 0f;
        }

        // Remove inverse teleporter cooldown
        [HarmonyPatch(typeof(InverseTeleporterItem), "Start")]
        [HarmonyPostfix]
        static void RemoveInverseTeleporterCooldown(InverseTeleporterItem __instance)
        {
            __instance.cooldownTime = 0f;
        }
    }
} 