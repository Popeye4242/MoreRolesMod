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

        public void Start()
        {
        }



        public void Update()
        {
            KillButtonManager killButton = HudManager.Instance.KillButton;
            if (HudManager.Instance.UseButton != null && HudManager.Instance.UseButton.isActiveAndEnabled)
            {
                GameManager.UpdateClosestPlayer();
                m_cooldown = Math.Max(0, m_cooldown - Time.deltaTime);
                killButton.SetCoolDown(m_cooldown, PlayerControl.GameOptions.KillCooldown);

                var closestPlayer = GameManager.ClosestPlayer;
                if (closestPlayer == default)
                    return;

                if (closestPlayer.Distance < GameOptionsData.KillDistances[PlayerControl.GameOptions.KillDistance])
                {
                    killButton.SetTarget(closestPlayer.Player);
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
            m_cooldown = PlayerControl.GameOptions.KillCooldown;
        }
    }
}
