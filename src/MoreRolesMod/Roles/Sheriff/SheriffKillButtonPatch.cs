using HarmonyLib;
using Reactor;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoreRolesMod.Roles.Sheriff
{
    [HarmonyPatch(typeof(KillButtonManager), nameof(KillButtonManager.PerformKill))]
    public class SheriffKillButtonPatch
    {
        public static bool Prefix()
        {
            if (!PlayerControl.LocalPlayer.HasRole(Role.Sheriff))
                return false;

            if (PlayerControl.LocalPlayer.Data.AKOHOAJIHBE)
                return false;

            var currentTarget = HudManager.Instance.KillButton.CurrentTarget;

            Rpc<SheriffMurderRpc>.Instance.Send(data: (Killer: PlayerControl.LocalPlayer.PlayerId, Target: currentTarget.PlayerId), immediately: true);
            if (!currentTarget.Data.LGEGJEHCFOG)
            {
                Rpc<SheriffMurderRpc>.Instance.Send(data: (Killer: PlayerControl.LocalPlayer.PlayerId, Target: PlayerControl.LocalPlayer.PlayerId), immediately: true);
            }

            var killButton = HudManager.Instance.KillButton.gameObject.GetComponent<SheriffKillButton>();
            killButton?.ResetCooldown();
            return false;
        }
    }
}
