using HarmonyLib;
using InnerNet;
using MoreRolesMod.Roles.Sheriff;
using MoreRolesMod.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoreRolesMod.Patches
{

    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class UpdatePlayerNameColors
    {
        static void Postfix(HudManager __instance)
        {
            if (AmongUsClient.Instance.GameMode == GameModes.FreePlay || AmongUsClient.Instance.GameState == InnerNetClient.GameStates.Started)
            {
                if (PlayerControl.LocalPlayer.Data.IsDead)
                {
                    foreach (var player in PlayerControl.AllPlayerControls)
                    {
                        if (player.Data.IsImpostor)
                        {
                            player.nameText.Color = Palette.ImpostorRed;
                        }
                        else if (player.HasRole(Role.Sheriff))
                        {
                            player.nameText.Color = Sheriff.SheriffColor;
                        }
                        else
                        {
                            player.nameText.Color = Palette.White;
                        }
                    }

                    if (MeetingHud.Instance != null)
                    {
                        foreach (var playerVoteArea in MeetingHud.Instance.playerStates)
                        {
                            if (playerVoteArea.NameText != null)
                            {
                                playerVoteArea.NameText.Color = PlayerTools.GetPlayerById(playerVoteArea.TargetPlayerId).nameText.Color;
                            }
                        }
                    }
            }
                else
                {
                    foreach (var player in PlayerControl.AllPlayerControls)
                    {
                        player.nameText.Color = Palette.White;
                    }
                    if (PlayerControl.LocalPlayer.Data.IsImpostor)
                    {
                        PlayerControl.LocalPlayer.nameText.Color = Palette.ImpostorRed;
                    }
                    else if (PlayerControl.LocalPlayer.HasRole(Role.Sheriff))
                    {
                        PlayerControl.LocalPlayer.nameText.Color = Sheriff.SheriffColor;
                    }
                    else
                    {
                        PlayerControl.LocalPlayer.nameText.Color = Palette.White;
                    }

                    foreach (var player in PlayerControl.AllPlayerControls)
                    {
                        if (MeetingHud.Instance != null)
                        {
                            foreach (var playerVoteArea in MeetingHud.Instance.playerStates)
                            {
                                if (playerVoteArea.NameText != null && player.PlayerId == playerVoteArea.TargetPlayerId)
                                {
                                    playerVoteArea.NameText.Color = player.nameText.Color;
                                }
                            }
                        }
                    }
                }

            }
        }
    }
}
