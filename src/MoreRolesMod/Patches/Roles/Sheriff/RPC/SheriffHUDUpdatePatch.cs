using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoreRolesMod.Patches.Roles.Sheriff.RPC
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public static class SheriffHUDUpdatePatch
    {
        public static void Postfix(HudManager __instance)
        {
            if (AmongUsClient.Instance.GameState == InnerNetClient.GameStates.Started)
            {
                UpdateIngameSheriffHud(__instance);
            }
        }

        private static void UpdateIngameSheriffHud(HudManager hudManager)
        {
            var KillButton = hudManager.KillButton;
            KillButton.gameObject.SetActive(true);
            KillButton.isActive = true;
            KillButton.SetCoolDown(0f, 1f);
            KillButton.renderer.color = Palette.EnabledColor;
            KillButton.renderer.material.SetFloat("_Desat", 0f);
        }
    }
}
