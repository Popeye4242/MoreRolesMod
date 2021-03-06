﻿using MoreRolesMod.Config;
using MoreRolesMod.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MoreRolesMod
{
    public static class GameManager
    {
        public static Dictionary<byte, Role> PlayerRoles { get; } = new Dictionary<byte, Role>();
        public static (PlayerControl Player, double Distance) ClosestPlayer { get; private set; }
        public static LobbyConfig Config { get; private set; }

        internal static void ResetGame()
        {
            PlayerRoles.Clear();
            ClosestPlayer = default;
            Config = MoreRolesPlugin.Instance.GetLobbyConfig();
        }

        internal static void UpdateClosestPlayer()
        {
            var players = PlayerControl.AllPlayerControls
                .ToArray()
                .Where(x => !x.Data.IsDead && !x.AmOwner && x.Visible)
                .Where(x => PlayerTools.IsAnythingBetween(PlayerControl.LocalPlayer.GetTruePosition(), x.GetTruePosition()))
                .Select(x => (Player: x, Distance: Vector3.Distance(PlayerControl.LocalPlayer.transform.position, x.transform.position)));
            if (players.Any())
            {
                var min = players.Min(x => x.Distance);
                ClosestPlayer = players.First(x => x.Distance == min);
                System.Console.WriteLine("Closest player is "+ ClosestPlayer.Player.name);
            }
            else
            {
                ClosestPlayer = default;
            }

        }


    }
}
