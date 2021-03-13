using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoreRolesMod.Roles.Morphling
{
    [HarmonyPatch(typeof(KillButtonManager), nameof(KillButtonManager.PerformKill))]
    public class MorphlingKillButtonPatch
    {
        public static bool Prefix(KillButtonManager __instance)
        {
            return __instance != HudUpdatePatch.MorphlingMorphButton?.KillButton;
        }
    }
}
