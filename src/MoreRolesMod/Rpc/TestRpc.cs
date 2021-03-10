using Reactor;
using System;
using System.Collections.Generic;
using System.Text;
using MoreRolesMod;
using Hazel;

namespace MoreRolesMod.Rpc
{
    [RegisterCustomRpc]
    public class TestRpc : PlayerCustomRpc<MoreRolesPlugin, TestRpc.TestData>
    {

        public TestRpc(MoreRolesPlugin plugin) : base(plugin)
        {
        }
        public override RpcLocalHandling LocalHandling => RpcLocalHandling.None;

        public override void Handle(PlayerControl innerNetObject, TestData data)
        {
            MoreRolesPlugin.Logger.LogWarning($"{innerNetObject.Data.PlayerId} sent \"{data.Message}\"");
        }

        public override TestData Read(MessageReader reader)
        {
            return new TestData(reader.ReadString());
        }

        public override void Write(MessageWriter writer, TestData data)
        {
            writer.Write(data.Message);
        }

        public readonly struct TestData
        {
            public readonly string Message;
            public TestData(string message)
            {
                Message = message;
            }
        }
    }
}
