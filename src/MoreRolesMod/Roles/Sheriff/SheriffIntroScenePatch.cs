using HarmonyLib;
using MoreRolesMod.Roles;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MoreRolesMod.Patches
{
    [HarmonyPatch(typeof(IntroCutscene.CoBegin__d), nameof(IntroCutscene.CoBegin__d.MoveNext))]
    public class SheriffIntroScenePatch
    {
        static bool Prefix(IntroCutscene.CoBegin__d __instance)
        {
            return true;
        }

        static void Postfix(IntroCutscene.CoBegin__d __instance)
        {
            if (PlayerControl.LocalPlayer.HasRole(Role.Sheriff))
            {
                __instance.__this.Title.Text = "Sheriff";
                __instance.__this.Title.Color = new Color(0, 40f / 255f, 198f / 255f, 1);
                __instance.__this.ImpostorText.Text = "Kill the impostor [FF0000FF]Impostors";
                __instance.__this.BackgroundBar.material.color = new Color(0, 40f / 255f, 198f / 255f, 1);
            }
        }
    }
}


