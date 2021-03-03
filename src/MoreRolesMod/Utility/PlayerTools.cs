using System.Collections.Generic;
using System.Linq;

namespace MoreRolesMod.Utility
{
    public static class PlayerTools
    {
        internal static PlayerControl GetPlayerById(byte playerId)
        {
            return (PlayerControl.AllPlayerControls as object as List<PlayerControl>).FirstOrDefault(player => player.PlayerId == playerId);
        }
    }
}
