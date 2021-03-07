using HarmonyLib;
using Hazel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoreRolesMod.Patches
{
    [HarmonyPatch(typeof(AnnouncementPopUp), nameof(AnnouncementPopUp.Show))]
    class PopupPatch1
    {
        static void Postfix(AnnouncementPopUp __instance)
        {
            System.Console.WriteLine(__instance.AnnounceText.Text);
        }
    }
    [HarmonyPatch(typeof(AnnouncementPopUp), nameof(AnnouncementPopUp.Method_7))]
    class PopupPatch8
    {
        static void Postfix(AnnouncementPopUp __instance)
        {
            // TODO: fetch announcement from web server
            __instance.AnnounceText.Text =
                $@"{DateTime.Now.ToShortDateString()}

Work is currently in Progress.";
        }
    }
}
