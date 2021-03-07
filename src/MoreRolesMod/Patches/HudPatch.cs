using HarmonyLib;
using MoreRolesMod.Utility;
using Reactor.Extensions;
using Reactor.Unstrip;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MoreRolesMod.Patches
{

    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Start))]
    public class HudStartPatch
    {
        static HudManager HudManager { get; set; }
        static void Postfix(HudManager __instance)
        {
            HudManager = __instance;
            if (AmongUsClient.Instance.GameState == InnerNetClient.GameStates.Started)
            {
                System.Console.WriteLine("Game Started");
                AddPopeye();
            }
            else if (AmongUsClient.Instance.GameState == InnerNetClient.GameStates.Joined)
            {
                System.Console.WriteLine("Lobby Joined");
            }
            else if (AmongUsClient.Instance.GameState == InnerNetClient.GameStates.NotJoined)
            {
                System.Console.WriteLine("Client Startet");
            }
            else if (AmongUsClient.Instance.GameState == InnerNetClient.GameStates.Ended)
            {
                System.Console.WriteLine("Game Ended");
            }
        }

        private static void AddPopeye()
        {
        }
    }


    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class HudUüdatePatch
    {
        static void Postfix(HudManager __instance)
        {
        }

    }
}
