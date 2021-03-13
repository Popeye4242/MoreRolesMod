using HarmonyLib;
using UnityEngine;
using InnerNet;

namespace MoreRolesMod.Roles.Sheriff
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class HudUpdatePatch
    {
        public static KillButtonManager SheriffKillButton { get; set; }
        static HudManager HudManager { get; set; }
        static void Postfix(HudManager __instance)
        {

            HudManager = __instance;
            if (AmongUsClient.Instance.GameMode == GameModes.FreePlay || AmongUsClient.Instance.GameState == InnerNetClient.GameStates.Started)
            {
                if (PlayerControl.LocalPlayer.Data.IsDead)
                    return;
                
                if (PlayerControl.LocalPlayer.HasRole(Role.Sheriff))
                {
                    if (SheriffKillButton == null)
                    {
                        AddSheriffButton();
                    }
                    else
                    {
                        SheriffKillButton.gameObject.SetActive(true);
                        SheriffKillButton.isActive = true;
                    }
                }
            }
        }


        private static void AddSheriffButton()
        {
            SheriffKillButton = HudManager.KillButton;
            SheriffKillButton.gameObject.AddComponent<SheriffKillButton>();
        }
    }
}
