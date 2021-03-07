using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using TestMod;

namespace MoreRolesMod.Patches
{
    [HarmonyPatch(typeof(MainMenuManager), nameof(MainMenuManager.Start))]
    public class MainMenuPatch
    {

        static void Postfix(MainMenuManager __instance)
        {
            MoreRolesPlugin.MainMenuManager = __instance;
        }

    }
}
