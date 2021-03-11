using Hazel;
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
            GameManager.ResetGame();
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
