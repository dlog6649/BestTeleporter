using HarmonyLib;            // Harmony 패치 기능을 사용하기 위한 import
using GameNetcodeStuff;      // PlayerControllerB 클래스를 사용하기 위한 import

namespace BestTeleporter
{
  [HarmonyPatch]            // 이 클래스가 Harmony 패치들을 포함하고 있다고 표시
  internal class TeleporterPatch
  {
    // 일반 텔레포터 아이템 드롭 방지 (텔레포트 전)
    [HarmonyPatch(typeof(TeleporterScript), "TeleportPlayer")]  // TeleporterScript 클래스의 TeleportPlayer 메서드를 패치
    [HarmonyPrefix]         // 원본 메서드 실행 전에 이 코드를 실행
    static bool PreventItemDropTeleporter(PlayerControllerB playerScript)
    {
      // 일반 텔레포터 아이템 드롭 방지
      playerScript.isHoldingObject = false;
      return true;
    }

    // 일반 텔레포터 아이템 상태 복구 (텔레포트 후)
    [HarmonyPatch(typeof(TeleporterScript), "TeleportPlayer")]
    [HarmonyPostfix]        // 원본 메서드 실행 후에 이 코드를 실행
    static void RestoreItemHoldingTeleporter(PlayerControllerB playerScript)
    {
      // 일반 텔레포터 아이템 상태 복구
      playerScript.isHoldingObject = true;
    }

    // 역방향 텔레포터 아이템 드롭 방지 (텔레포트 전)
    [HarmonyPatch(typeof(InverseTeleporterItem), "TeleportPlayerOutside")]
    [HarmonyPrefix]
    static bool PreventItemDropInverse(PlayerControllerB playerScript)
    {
      // 역방향 텔레포터 아이템 드롭 방지
      playerScript.isHoldingObject = false;
      return true;
    }

    // 역방향 텔레포터 아이템 상태 복구 (텔레포트 후)
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
