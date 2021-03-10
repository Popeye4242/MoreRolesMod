using HarmonyLib;
using MoreRolesMod.Utility;
using Reactor.Extensions;
using Reactor.Unstrip;
using System;
using System.Collections.Generic;
using System.Text;
using MoreRolesMod;
using UnityEngine;
using InnerNet;
using MoreRolesMod.Components;

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
        public static PopeyeButton PopeyeButton { get; set; }
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
            if (AmongUsClient.Instance.GameMode == GameModes.FreePlay || AmongUsClient.Instance.GameState == InnerNetClient.Nested_0.Started)
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
            else if (AmongUsClient.Instance.GameState == InnerNetClient.Nested_0.Joined)
            {
            }
            else if (AmongUsClient.Instance.GameState == InnerNetClient.Nested_0.NotJoined)
            {
            }
            else if (AmongUsClient.Instance.GameState == InnerNetClient.Nested_0.Ended)
            {
            }
        }


        private static void AddPopeyeButton()
        {
            PopeyeButton = HudManager.gameObject.AddComponent<PopeyeButton>();
            
        }

        private static void UpdatePopeyeButton()
        {
        }
    }
}
