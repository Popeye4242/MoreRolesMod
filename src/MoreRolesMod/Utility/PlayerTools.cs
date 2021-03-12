using System;
using System.Collections.Generic;
using System.Linq;

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

    }
}
