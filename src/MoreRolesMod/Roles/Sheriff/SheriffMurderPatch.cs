using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoreRolesMod.Roles.Sheriff
{
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.MurderPlayer))]
    public static class SheriffMurderPatch
    {
        public static bool Prefix(PlayerControl __instance, PlayerControl PAIBDFDMIGK)
        {
            if (__instance.HasRole(Role.Sheriff))
            {
                __instance.Data.IsImpostor = true;
            }

            return true;
        }

        public static void Postfix(PlayerControl __instance, PlayerControl PAIBDFDMIGK)
        {
            if (__instance.HasRole(Role.Sheriff))
            {
                __instance.Data.IsImpostor = false;
            }
        }
    }
}
