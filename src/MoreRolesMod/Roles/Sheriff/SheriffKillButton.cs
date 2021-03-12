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
            System.Console.WriteLine("Sheriff KillButton behaviour added");
        }

        public void Start()
        {
        }



        public void Update()
        {
            System.Console.WriteLine("Updated kill button");
            KillButtonManager killButton = HudManager.Instance.KillButton;
            if (PlayerControl.LocalPlayer.Data.IsDead)
            {
                killButton.gameObject.SetActive(false);
                killButton.isActive = false;
                return; 
            }
            if (HudManager.Instance.UseButton != null && HudManager.Instance.UseButton.isActiveAndEnabled)
            {
                GameManager.UpdateClosestPlayer();
                killButton.gameObject.SetActive(true);
                killButton.isActive = true;
                m_cooldown = Math.Max(0, m_cooldown - Time.deltaTime);
                killButton.SetCoolDown(m_cooldown, GameManager.Config.SheriffKillCooldown);
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

        public void OnDestroy()
        {
            System.Console.WriteLine("Sheriff button destroyed");
        }

        internal void ResetCooldown()
        {
            m_cooldown = GameManager.Config.SheriffKillCooldown;
            System.Console.WriteLine("Resetted cooldown");
        }
    }
}
