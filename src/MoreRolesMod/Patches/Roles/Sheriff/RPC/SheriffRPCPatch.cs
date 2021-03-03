using HarmonyLib;
using Hazel;
using MoreRolesMod.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoreRolesMod.Patches.Roles.Sheriff.RPC
{
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.HandleRpc))]
    public static class SheriffRPCPatch
    {
        public static void Postfix(byte HKHMBLJFLMC, MessageReader ALMCIJKELCP)
        {
            var packetId = HKHMBLJFLMC;
            var reader = ALMCIJKELCP;

            if (!packetId.IsRpcOfType(typeof(CustomSheriffRPCActions)))
            {
                return;
            }

            switch (packetId)
            {
                case (byte) CustomSheriffRPCActions.SheriffKill:
                    HandleSheriffKillRpc(reader);
                    break;
            }
        }

        private static void HandleSheriffKillRpc(MessageReader reader)
        {

            var killerId = reader.ReadByte();
            var killer = PlayerTools.GetPlayerById(killerId);

            var targetId = reader.ReadByte();
            var target = PlayerTools.GetPlayerById(targetId);

            killer.MurderPlayer(target);
        }
    }
}
