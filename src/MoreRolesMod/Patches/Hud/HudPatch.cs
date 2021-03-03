using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoreRolesMod.Patches.Hud
{

    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class HudPatch
    {
        static void Postfix(HudManager __instance)
        {

        }

    }
}
