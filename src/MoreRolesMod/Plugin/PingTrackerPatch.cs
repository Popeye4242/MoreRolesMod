using HarmonyLib;
using System;
using System.Reflection;

namespace MoreRolesMod.Plugin
{
    [HarmonyPatch(typeof(PingTracker), nameof(PingTracker.Update))]
    public class PingTrackerPatch
    {

        public static void Postfix(PingTracker __instance)
        {
            var version = MoreRolesPlugin.Version.ToString(3);
            __instance.text.Text += $"{Environment.NewLine}More Roles v{version}";
        }
    }
}
