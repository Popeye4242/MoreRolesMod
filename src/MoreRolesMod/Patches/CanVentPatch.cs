﻿using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MoreRolesMod.Patches
{
    [HarmonyPatch(typeof(Vent), nameof(Vent.CanUse))]
    class CanVentPatch
    {
        public static bool Prefix(Vent __instance, ref float __result, [HarmonyArgument(0)] GameData.PlayerInfo pc,
        [HarmonyArgument(1)] out bool canUse, [HarmonyArgument(2)] out bool couldUse)
        {
            var num = float.MaxValue;
            var localPlayer = pc.Object;
            couldUse = true;
            canUse = couldUse;
            if ((DateTime.UtcNow - PlayerVentTimeExtension.GetLastVent(pc.Object.PlayerId)).TotalMilliseconds > 1000)
            {
                num = Vector2.Distance(localPlayer.GetTruePosition(), __instance.transform.position);
                canUse &= num <= __instance.UsableDistance;
            }
            __result = num;
            return false;
        }
    }

    [HarmonyPatch(typeof(Vent), nameof(Vent.Method_38))]
    public static class VentEnterPatch
    {
        public static void Postfix(PlayerControl NMEAPOJFNKA)
        {
            PlayerVentTimeExtension.SetLastVent(NMEAPOJFNKA.PlayerId);
        }
    }

    [HarmonyPatch(typeof(Vent), nameof(Vent.Method_1))]
    public static class VentExitPatch
    {
        public static void Postfix(PlayerControl NMEAPOJFNKA)
        {
            PlayerVentTimeExtension.SetLastVent(NMEAPOJFNKA.PlayerId);
        }
    }

    public static class PlayerVentTimeExtension
    {
        public static IDictionary<byte, DateTime> allVentTimes = new Dictionary<byte, DateTime>() { };

        public static void SetLastVent(byte player)
        {
            if (allVentTimes.ContainsKey(player))
                allVentTimes[player] = DateTime.UtcNow;
            else
                allVentTimes.Add(player, DateTime.UtcNow);
        }

        public static DateTime GetLastVent(byte player)
        {
            return allVentTimes.ContainsKey(player) ? allVentTimes[player] : new DateTime(0);
        }
    }
}
