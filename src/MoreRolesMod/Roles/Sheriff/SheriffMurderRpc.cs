using Hazel;
using MoreRolesMod.Utility;
using Reactor;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoreRolesMod.Roles.Sheriff
{
    [RegisterCustomRpc]
    public class SheriffMurderRpc : PlayerCustomRpc<MoreRolesPlugin, SheriffMurderData>
    {
        public SheriffMurderRpc(MoreRolesPlugin plugin) : base(plugin)
        {

        }

        public override RpcLocalHandling LocalHandling => RpcLocalHandling.Before;

        public override void Handle(PlayerControl innerNetObject, SheriffMurderData data)
        {
            var killer = PlayerTools.GetPlayerById(data.KillerId);
            var target = PlayerTools.GetPlayerById(data.TargetId);
            System.Console.WriteLine("{0} murdered {1}", killer.name, target.name);
            killer.MurderPlayer(target);
        }

        public override SheriffMurderData Read(MessageReader reader)
        {
            return (Killer: reader.ReadByte(), Target: reader.ReadByte());
        }

        public override void Write(MessageWriter writer, SheriffMurderData data)
        {
            writer.Write(data.KillerId);
            writer.Write(data.TargetId);
        }
    }

    public struct SheriffMurderData
    {
        public byte KillerId;
        public byte TargetId;
        public SheriffMurderData(byte killer, byte target)
        {
            KillerId = killer;
            TargetId = target;
        }

        public static implicit operator SheriffMurderData((byte Killer, byte Target) data) => new SheriffMurderData(data.Killer, data.Target);
    }
}
