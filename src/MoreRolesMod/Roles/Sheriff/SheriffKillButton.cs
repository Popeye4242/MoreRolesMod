using MoreRolesMod.Roles.Sheriff;
using Reactor;
using System;
using System.Collections.Generic;
using System.Text;
using UnhollowerBaseLib.Attributes;
using UnityEngine;
using UnityEngine.Events;

namespace MoreRolesMod.Roles.Sheriff
{
    [RegisterInIl2Cpp]
    public class SheriffKillButton : MonoBehaviour
    {
        [HideFromIl2Cpp]
        public MoreRolesPlugin Plugin { get; internal set; }
        private float m_cooldown = 10f;

        public SheriffKillButton(IntPtr handle) : base(handle)
        {
        }

        public void Awake()
        {
        }

        public void OnMouseUpAsButton()
        {
            if (PlayerControl.LocalPlayer.HasRole(Role.Sheriff))
            {
                var data = (Killer: PlayerControl.LocalPlayer.PlayerId, Target: GameManager.ClosestPlayer.Player.PlayerId);
                Rpc<SheriffMurderRpc>.Instance.Send(data, immediately: true);
            }
        }

        public void Start()
        {
        }



        public void Update()
        {
            KillButtonManager killButton = HudManager.Instance.KillButton;
            if (PlayerControl.LocalPlayer.Data.AKOHOAJIHBE)
            {
                killButton.gameObject.SetActive(false);
                killButton.isActive = false;
                return; 
            }
            if (HudManager.Instance.UseButton != null && HudManager.Instance.UseButton.isActiveAndEnabled)
            {
                m_cooldown = Math.Max(0, m_cooldown - Time.deltaTime);
                killButton.SetCoolDown(m_cooldown, 10f);
                if (GameManager.ClosestPlayer.Distance < GameOptionsData.KillDistances[PlayerControl.GameOptions.KillDistance])
                {
                    killButton.SetTarget(GameManager.ClosestPlayer.Player);
                }
                else
                {
                    killButton.SetTarget(null);
                }

                if (Input.GetKeyInt(KeyCode.Q))
                {
                    killButton.PerformKill();
                }
            }
        }

        internal void ResetCooldown()
        {
            m_cooldown = 10f;
            System.Console.WriteLine("Resetted cooldown");
        }
    }
}
