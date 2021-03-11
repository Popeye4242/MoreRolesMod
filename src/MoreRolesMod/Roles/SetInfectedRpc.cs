using Hazel;
using MoreRolesMod.Utility;
using Reactor;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoreRolesMod.Roles
{
    [RegisterCustomRpc]
    public class SetInfectedRpc : PlayerCustomRpc<MoreRolesPlugin, SetInfectedData>
    {
        public SetInfectedRpc(MoreRolesPlugin plugin) : base(plugin)
        {
        }
        public override RpcLocalHandling LocalHandling => RpcLocalHandling.Before;

        public override void Handle(PlayerControl innerNetObject, SetInfectedData data)
        {
            if (AmongUsClient.Instance.GetHost().Id == innerNetObject.OwnerId)
            {
                GameManager.PlayerRoles[data.PlayerId] = data.Role;
            }
            else
            {
                // ignore message, just host can set players infected
            }
        }

        public override SetInfectedData Read(MessageReader reader)
        {
            return (Player: reader.ReadByte(), Role: reader.ReadByte());
        }

        public override void Write(MessageWriter writer, SetInfectedData data)
        {
            writer.Write(data.PlayerId);
            writer.Write((byte)data.Role);
        }
    }

    public struct SetInfectedData
    {
        public Role Role;
        public byte PlayerId;
        public PlayerControl Player => PlayerTools.GetPlayerById(PlayerId);
        public SetInfectedData(byte player, Role role)
        {
            PlayerId = player;
            Role = role;
        }

        public static implicit operator SetInfectedData((PlayerControl Player, Role Role) data) => new SetInfectedData(data.Player.PlayerId, data.Role);
        public static implicit operator SetInfectedData((byte Player, byte Role) data) => new SetInfectedData(data.Player, (Role)data.Role);
    }
}
