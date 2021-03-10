using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MoreRolesMod.Patches
{
    [HarmonyPatch(typeof(Vent), nameof(Vent.CanUse))]
    class CanVentPatch
    {
        public static bool Prefix(Vent __instance, ref float __result, [HarmonyArgument(0)] GameData.Nested_1 pc,
        [HarmonyArgument(1)] out bool canUse, [HarmonyArgument(2)] out bool couldUse)
        {
            PlayerControl localPlayer = pc.CBEJMNMADDB;
            couldUse  = true;
            canUse = couldUse;
            float num = Vector2.Distance(localPlayer.GetTruePosition(), __instance.transform.position);
            canUse &= num <= __instance.UsableDistance;
            __result = num;
            return false;
        }
    }
}
