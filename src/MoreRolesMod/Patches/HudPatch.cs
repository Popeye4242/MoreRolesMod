using HarmonyLib;
using MoreRolesMod.Utility;
using Reactor.Extensions;
using Reactor.Unstrip;
using System;
using System.Collections.Generic;
using System.Text;
using TestMod;
using UnityEngine;

namespace MoreRolesMod.Patches
{

    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Start))]
    public class HudStartPatch
    {
        static void Postfix(HudManager __instance)
        {
            System.Console.WriteLine("Hud started");
        }
    }

    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class HudUpdatePatch
    {
        public static KillButtonManager PopeyeButton { get; set; }
        public static Vector2 PositionOffset = new Vector2(0.125f, 0.125f);
        public static float MaxTimer = 5f;
        public static float Timer = 0f;

        public static void OnClick()
        {
            MoreRolesPlugin.Logger.LogDebug("Clicked Popeye Button!");
        }


        static HudManager HudManager { get; set; }
        static KillButtonManager KillButton => HudManager.KillButton;
        static void Postfix(HudManager __instance)
        {

            HudManager = __instance;
            if (AmongUsClient.Instance.GameMode == GameModes.FreePlay || AmongUsClient.Instance.GameState == InnerNetClient.GameStates.Started)
            {
                if (PopeyeButton != null)
                {
                    UpdatePopeyeButton();
                }
                else
                {
                    if (PlayerControl.LocalPlayer.name.Equals("Popeye", StringComparison.InvariantCultureIgnoreCase))
                    {
                        AddPopeyeButton();
                    }
                }
            }
            else if (AmongUsClient.Instance.GameState == InnerNetClient.GameStates.Joined)
            {
            }
            else if (AmongUsClient.Instance.GameState == InnerNetClient.GameStates.NotJoined)
            {
            }
            else if (AmongUsClient.Instance.GameState == InnerNetClient.GameStates.Ended)
            {
            }
        }


        private static void AddPopeyeButton()
        {
            PopeyeButton = new KillButtonManager();
            PopeyeButton = UnityEngine.Object.Instantiate(HudManager.KillButton, HudManager.transform);
            PopeyeButton.gameObject.SetActive(true);
            PopeyeButton.renderer.enabled = true;
            PopeyeButton.renderer.sprite = Assets.Popeye;
            PassiveButton button = PopeyeButton.GetComponent<PassiveButton>();
            button.OnClick.RemoveAllListeners();
            button.OnClick.AddListener((UnityEngine.Events.UnityAction)OnClick);
            MoreRolesPlugin.Logger.LogDebug("Added Popeye Button");
        }
        private static void UpdatePopeyeButton()
        {
            if (PopeyeButton.transform.localPosition.x > 0f)
            {
                var v = new Vector3((PopeyeButton.transform.localPosition.x + 1.3f) * -1, PopeyeButton.transform.localPosition.y, PopeyeButton.transform.localPosition.z);
                PopeyeButton.transform.localPosition = v + new Vector3(PositionOffset.x, PositionOffset.y);
            }

            if (PlayerControl.LocalPlayer.CanMove)
                Timer -= Time.deltaTime;
            PopeyeButton.renderer.color = Palette.EnabledColor;


            PopeyeButton.gameObject.SetActive(true);
            PopeyeButton.renderer.enabled = true;
            PopeyeButton.renderer.material.SetFloat("_Desat", 0f);
            PopeyeButton.SetCoolDown(Timer, MaxTimer);
        }
    }
}
