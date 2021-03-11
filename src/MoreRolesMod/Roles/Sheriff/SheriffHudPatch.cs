using HarmonyLib;
using UnityEngine;
using InnerNet;

namespace MoreRolesMod.Roles.Sheriff
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class HudUpdatePatch
    {
        public static GameObject SheriffButton { get; set; }
        static HudManager HudManager { get; set; }
        static KillButtonManager KillButton => HudManager.KillButton;
        static void Postfix(HudManager __instance)
        {

            HudManager = __instance;
            if (AmongUsClient.Instance.GameMode == GameModes.FreePlay || AmongUsClient.Instance.GameState == InnerNetClient.Nested_0.Started)
            {
                GameManager.UpdateGame();
                if (PlayerControl.LocalPlayer.HasRole(Role.Sheriff))
                {
                    if (SheriffButton == null)
                    {
                        AddPopeyeButton();
                        PlayerControl.LocalPlayer.nameText.Color = new Color(40f / 255f, 198f / 255f, 0, 1);
                    }
                }
            }
        }


        private static void AddPopeyeButton()
        {
            SheriffButton = UnityEngine.Object.Instantiate(new GameObject(), HudManager.gameObject.transform);
            SheriffButton.AddComponent<SheriffKillButton>();

        }
    }
}
