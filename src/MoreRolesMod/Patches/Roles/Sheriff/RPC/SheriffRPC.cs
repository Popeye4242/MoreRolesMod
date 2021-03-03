using Hazel;
using MoreRolesMod.Patches.Networking;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoreRolesMod.Patches.Roles.Sheriff.RPC
{
    public static class SheriffRPC
    {
        public static void SendKillTargetRPC(PlayerControl target)
        {
            MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)CustomSheriffRPCActions.SheriffKill, SendOption.Reliable);
            writer.Write(PlayerControl.LocalPlayer.PlayerId);
            writer.Write(PlayerControl.LocalPlayer.PlayerId);
            AmongUsClient.Instance.FinishRpcImmediately(writer);
            // make the local player kill the target player
            PlayerControl.LocalPlayer.MurderPlayer(target);
            AmongUsClient.Instance.FinishRpcImmediately(writer);
        }
    }
}
