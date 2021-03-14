using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MoreRolesMod.Utility
{
    public static class PlayerTools
    {
        public static PlayerControl[] AllPlayerControls => PlayerControl.AllPlayerControls.ToArray();
        internal static PlayerControl GetPlayerById(byte playerId)
        {
            return AllPlayerControls.FirstOrDefault(player => player.PlayerId == playerId);
        }
        internal static PlayerControl GetPlayerById(sbyte playerId)
        {
            return GetPlayerById((byte)playerId);
        }
        public static bool IsAnythingBetween(Vector2 pos1, Vector2 pos2)
        {
            var layer = LayerMask.GetMask(new string[]
            {
                "Ship",
                "Objects"
            });
            return !PhysicsHelpers.AnythingBetween(pos1, pos2, layer, false);
        }
    }
}
