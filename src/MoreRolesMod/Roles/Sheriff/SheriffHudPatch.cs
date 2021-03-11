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
                GameManager.UpdateGame();
                if (PlayerControl.LocalPlayer.HasRole(Role.Sheriff))
                {
                    if (SheriffKillButton == null)
                    {
                        AddPopeyeButton();
                    }
                }
            }
        }


        private static void AddPopeyeButton()
        {
            var killButton = SheriffKillButton = HudManager.KillButton;
            HudManager.KillButton.gameObject.AddComponent<SheriffKillButton>();;

            killButton.gameObject.SetActive(true);
            killButton.isActive = true;

            // change player name color 
            PlayerControl.LocalPlayer.nameText.Color = Sheriff.SheriffColor;
        }
    }
}
