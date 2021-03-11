using HarmonyLib;
using MoreRolesMod.Roles;
using Reactor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnhollowerBaseLib;

namespace MoreRolesMod.Patches
{

    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.RpcSetInfected))]
    public class SetInfectedPatch
    {

        public static void Postfix(Il2CppReferenceArray<GameData.PlayerInfo> FMAOEJEHPAO)
        {
            Rpc<ResetGameRpc>.Instance.Send(data: true, immediately: true);
            var players = PlayerControl.AllPlayerControls.ToArray().Where(x => !x.Data.IsImpostor);
            foreach (var localPlayer in players)
            {
                Rpc<SetInfectedRpc>.Instance.Send(data: (localPlayer, Role.Sheriff), immediately: true);
            }
        }
    }
}
