using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoreRolesMod.Patches.Roles
{
    [HarmonyPatch(typeof(IntroCutscene.CoBegin__d), nameof(IntroCutscene.CoBegin__d.MoveNext))]
    public class IntroScenePatch
    {
        static bool Prefix(IntroCutscene.CoBegin__d __instance)
        {
            return true;
        }

        static void Postfix(IntroCutscene.CoBegin__d __instance)
        {
            foreach (var player in PlayerControl.AllPlayerControls)
            {
                if (player.Data.IsImpostor)
                {
                    System.Console.WriteLine(player.name + " is an impostor");
                }
            }

            __instance.__this.Title.Text = "Medic";
            __instance.__this.Title.Color = Palette.VisorColor;
            __instance.__this.ImpostorText.Text = "Create a shield to protect a [8DFFFF]Crewmate";
            __instance.__this.BackgroundBar.material.color = Palette.VisorColor;
        }
    }
}


