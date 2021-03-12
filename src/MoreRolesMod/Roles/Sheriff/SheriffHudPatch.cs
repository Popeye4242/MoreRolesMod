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
            System.Console.WriteLine("Adding Killbutton");
            SheriffKillButton = HudManager.KillButton;
            SheriffKillButton.gameObject.SetActive(true);
            SheriffKillButton.isActive = true;
            HudManager.KillButton.gameObject.AddComponent<SheriffKillButton>();
            // change player name color 
            PlayerControl.LocalPlayer.nameText.Color = Sheriff.SheriffColor;
        }
    }
}
