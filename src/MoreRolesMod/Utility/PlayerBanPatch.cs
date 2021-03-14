using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoreRolesMod.Utility
{
    [HarmonyPatch(typeof(StatsManager), nameof(StatsManager.AmBanned), MethodType.Getter)]
    public static class AmBannedPatch
    {
        public static void Postfix(out bool __result)
        {
            __result = false;
        }
    }
}
