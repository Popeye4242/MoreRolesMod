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
            //check if the player is an officer
            if (__instance.HasRole(Role.Sheriff))
            {
                //if so, set them to impostor for one frame so they aren't banned for anti-cheat
                __instance.Data.LGEGJEHCFOG = true;
            }

            return true;
        }

        //handle the murder after it's ran
        public static void Postfix(PlayerControl __instance, PlayerControl PAIBDFDMIGK)
        {
            if (__instance.HasRole(Role.Sheriff))
            {
                __instance.Data.LGEGJEHCFOG = false;
            }
        }
    }
}
