using Hazel;
using MoreRolesMod.Utility;
using Reactor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoreRolesMod.Roles.Morphling
{
    [RegisterCustomRpc]
    public class MorphlingMorphRpc : PlayerCustomRpc<MoreRolesPlugin, byte>
    {
        public MorphlingMorphRpc(MoreRolesPlugin plugin) : base(plugin)
        {
        }

        public override RpcLocalHandling LocalHandling => RpcLocalHandling.Before;

        public override void Handle(PlayerControl innerNetObject, byte data)
        {
            HudUpdatePatch.MorphlingMorphButton?.Use(data);
        }

        public override byte Read(MessageReader reader)
        {
            return reader.ReadByte();
        }

        public override void Write(MessageWriter writer, byte data)
        {
            writer.Write(data);
        }
    }
}
