using HarmonyLib;
using MoreRolesMod.Roles;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MoreRolesMod.Roles.Morphling
{
    [HarmonyPatch(typeof(IntroCutscene.CoBegin__d), nameof(IntroCutscene.CoBegin__d.MoveNext))]
    public class MorphlingIntroScenePatch
    {
        static bool Prefix(IntroCutscene.CoBegin__d __instance)
        {
            return true;
        }

        static void Postfix(IntroCutscene.CoBegin__d __instance)
        {
            if (PlayerControl.LocalPlayer.HasRole(Role.Morphling))
            {
                __instance.__this.Title.Text = "Morphling";
            }
        }
    }
}


