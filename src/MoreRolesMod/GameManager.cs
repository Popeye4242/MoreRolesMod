using System;
using System.Collections.Generic;
using System.Linq;

namespace MoreRolesMod
{
    public static class GameManager
    {
        public static Dictionary<byte, Role> PlayerRoles { get; } = new Dictionary<byte, Role>();
        public static (PlayerControl Player, double Distance) ClosestPlayer { get; private set; }

        internal static void ResetGame()
        {
            PlayerRoles.Clear();
            ClosestPlayer = default;
        }

        internal static void UpdateGame()
        {
            UpdateClosestPlayer();
        }

        private static double GetDistanceBetweenPlayers(PlayerControl player, PlayerControl refplayer)
        {
            var refpos = refplayer.GetTruePosition();
            var playerpos = player.GetTruePosition();

            return Math.Sqrt((refpos[0] - playerpos[0]) * (refpos[0] - playerpos[0]) +
                             (refpos[1] - playerpos[1]) * (refpos[1] - playerpos[1]));
        }

        private static void UpdateClosestPlayer()
        {
            var players = PlayerControl.AllPlayerControls
                .ToArray()
                .Where(x => !x.Data.IsDead && !x.AmOwner)
                .Select(x => (Player: x, Distance: GetDistanceBetweenPlayers(x, PlayerControl.LocalPlayer)));
            if (players.Any())
            {
                var min = players.Min(x => x.Distance);
                ClosestPlayer = players.First(x => x.Distance == min);
            }
            else
            {
                // game is not ready yet
            }

        }
    }
}
