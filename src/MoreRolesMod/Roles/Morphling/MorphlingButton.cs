using MoreRolesMod.Utility;
using Reactor;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace MoreRolesMod.Roles.Morphling
{
    public class MorphlingButton
    {
        private KillButtonManager m_killButton = null;
        private PassiveButton m_sampleButton = null;
        private float m_cooldown = 0f;

        public KillButtonManager KillButton => m_killButton;

        public MorphlingButton()
        {
            CreateButton();
        }

        private void CreateButton()
        {
            m_killButton = UnityEngine.Object.Instantiate(HudManager.Instance.KillButton, HudManager.Instance.transform);
            m_killButton.gameObject.SetActive(true);
            m_killButton.isActive = true;
            m_killButton.renderer.sprite = HudManager.Instance.UseButton.UseButton.sprite;
            m_killButton.renderer.enabled = true;
            m_killButton.renderer.color = Palette.EnabledColor;
            m_killButton.renderer.material.SetFloat("_Desat", 0f);
            m_killButton.transform.position = new Vector3(m_killButton.transform.position.x, HudManager.Instance.ReportButton.transform.position.y);
            m_sampleButton = m_killButton.GetComponent<PassiveButton>();
            m_sampleButton.OnClick.RemoveAllListeners();
            m_sampleButton.OnClick.AddListener((UnityAction)OnSample);

        }

        private void OnSample()
        {
            Morphling.SampledPlayer = m_killButton.CurrentTarget;
            m_sampleButton.OnClick.RemoveAllListeners();
            m_sampleButton.OnClick.AddListener((UnityAction)OnMorph);
        }

        private void OnMorph()
        {
            Rpc<MorphlingMorphRpc>.Instance.Send(data: Morphling.SampledPlayer.PlayerId, immediately: true);
        }

        public void Update()
        {
            GameManager.UpdateClosestPlayer();
            m_cooldown -= Time.deltaTime;
            m_cooldown = Math.Max(0f, m_cooldown);
            m_killButton.SetCoolDown(m_cooldown, GameManager.Config.MorphlingMorphCooldown);

            var closestPlayer = GameManager.ClosestPlayer;
            if (closestPlayer.Distance < GameOptionsData.KillDistances[PlayerControl.GameOptions.KillDistance])
            {
                m_killButton.SetTarget(GameManager.ClosestPlayer.Player);
            }
        }

        public void Use(byte data)
        {
            Morphling.SampledPlayer = PlayerTools.GetPlayerById(data);
            Morphling.IsMorphed = true;
            m_cooldown = GameManager.Config.MorphlingMorphCooldown;
        }
    }
}