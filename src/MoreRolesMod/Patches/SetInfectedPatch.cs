using HarmonyLib;
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

        public static void Postfix(Il2CppReferenceArray<GameData.Nested_1> FMAOEJEHPAO)
        {
            System.Console.WriteLine("Set Infected being executed by {0}", PlayerControl.LocalPlayer.name);
        }
    }
}
