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

        private SpriteRenderer m_SpriteRenderer = null;
        private BoxCollider2D m_collider = null;

        [HideFromIl2Cpp]
        public MoreRolesPlugin Plugin { get; internal set; }
        public SpriteRenderer renderer => m_SpriteRenderer;

        public SheriffKillButton(IntPtr handle) : base(handle)
        {
        }

        public void Awake()
        {
            gameObject.layer = Layers.UI;
            var corner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
            var scale = gameObject.transform.lossyScale;

            gameObject.transform.position = corner + new Vector3(scale.x, scale.y);

            m_SpriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            m_SpriteRenderer.sprite = HudManager.Instance.KillButton.renderer.sprite;

            m_collider = gameObject.AddComponent<BoxCollider2D>();
            m_collider.size = new Vector2(100, 100);

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

        }
    }
}
