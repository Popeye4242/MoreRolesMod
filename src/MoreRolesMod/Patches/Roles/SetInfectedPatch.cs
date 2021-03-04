using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnhollowerBaseLib;

namespace MoreRolesMod.Patches.Roles
{

    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.RpcSetInfected))]
    public class SetInfectedPatch
    {

        public static void Postfix(Il2CppReferenceArray<GameData.PlayerInfo> JPGEIBIBJPJ)
        {
            System.Console.WriteLine("Set Infected being executed by {0}", PlayerControl.LocalPlayer.name);
            if (!AmongUsClient.Instance.AmHost)
            {
                System.Console.WriteLine("Current player is not host (shouldn't happen under normal conditions)");
                return;
            }

            var crewmates = PlayerControl.AllPlayerControls.ToArray().ToList();
            crewmates.RemoveAll(x => x.Data.IsImpostor);


        }
    }
}
