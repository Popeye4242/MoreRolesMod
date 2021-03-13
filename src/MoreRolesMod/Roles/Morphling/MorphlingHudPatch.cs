using HarmonyLib;
using InnerNet;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MoreRolesMod.Roles.Morphling
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class HudUpdatePatch
    {
        public static MorphlingButton MorphlingMorphButton { get; set; }
        static HudManager HudManager { get; set; }
        static void Postfix(HudManager __instance)
        {

            HudManager = __instance;
            if (AmongUsClient.Instance.GameMode == GameModes.FreePlay || AmongUsClient.Instance.GameState == InnerNetClient.GameStates.Started)
            {
                if (PlayerControl.LocalPlayer.Data.IsDead)
                    return;

                if (!PlayerControl.LocalPlayer.HasRole(Role.Morphling))
                    return;

                if (MorphlingMorphButton == null)
                {
                    AddMorphButton();
                }
                else
                {
                    MorphlingMorphButton.Update();
                }
            }
            else if (AmongUsClient.Instance.GameState == InnerNetClient.GameStates.Ended)
            {
                if (MorphlingMorphButton != null)
                {
                    UnityEngine.Object.DestroyImmediate(MorphlingMorphButton.KillButton);
                    MorphlingMorphButton = null;
                }
            }
        }

        private static void AddMorphButton()
        {
            MorphlingMorphButton = new MorphlingButton();
        }
    }
}
