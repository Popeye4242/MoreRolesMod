using Hazel;
using MoreRolesMod.Roles.Sheriff;
using Reactor;

namespace MoreRolesMod.Roles
{
    [RegisterCustomRpc]
    public class ResetGameRpc : PlayerCustomRpc<MoreRolesPlugin, bool>
    {
        public ResetGameRpc(MoreRolesPlugin plugin) : base(plugin)
        {

        }

        public override RpcLocalHandling LocalHandling => RpcLocalHandling.Before;

        public override void Handle(PlayerControl innerNetObject, bool data)
        {
            HudUpdatePatch.SheriffKillButton = null;
            GameManager.ResetGame();
            System.Console.WriteLine("Resetted Game");
        }

        public override bool Read(MessageReader reader)
        {
            return reader.ReadBoolean();
        }

        public override void Write(MessageWriter writer, bool data)
        {
            writer.Write(data);
        }
    }
}
