using HarmonyLib;
using MoreRolesMod.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace MoreRolesMod.Patches.Roles
{

    [HarmonyPatch(typeof(EndGameManager), nameof(EndGameManager.SetEverythingUp))]
    public static class EndGamePatch
    {
        public static bool Prefix()
        {
            if (TempData.winners.Count <= 1 || !TempData.DidHumansWin(TempData.EndReason))
                return true;

            TempData.winners.Clear();
            var orderLocalPlayers = GameManager.LocalPlayers.Where(player => player.PlayerId == PlayerControl.LocalPlayer.PlayerId).ToList();
            orderLocalPlayers.AddRange(GameManager.LocalPlayers.Where(player => player.PlayerId != PlayerControl.LocalPlayer.PlayerId));

            foreach (var winner in orderLocalPlayers)
                TempData.winners.Add(new WinningPlayerData(winner.Data));

            return true;
        }

        public static void Postfix(EndGameManager __instance)
        {
            if (!TempData.DidHumansWin(TempData.EndReason))
                return;

            var flag = GameManager.LocalPlayers.Count(player => player.PlayerId == PlayerControl.LocalPlayer.PlayerId) == 0;

            if (!flag)
                return;

            __instance.WinText.Text = "Defeat";
            __instance.WinText.Color = Palette.ImpostorRed;
            __instance.BackgroundBar.material.color = new Color(1, 0, 0);
        }
    }
}
