using MoreRolesMod.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoreRolesMod
{
    public static class PlayerExtensions
    {
        public static bool HasRole(this PlayerControl player, Role role)
        {
            if (GameManager.PlayerRoles.TryGetValue(player.PlayerId, out Role playerRole))
            {
                return playerRole == role;
            }
            return false;
        }

        public static bool HasAnyRole(this PlayerControl player)
        {
            return GameManager.PlayerRoles.ContainsKey(player.PlayerId);
        }
    }
}
