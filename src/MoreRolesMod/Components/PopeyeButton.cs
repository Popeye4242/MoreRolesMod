using MoreRolesMod.Rpc;
using Reactor;
using System;
using System.Collections.Generic;
using System.Text;
using UnhollowerBaseLib.Attributes;
using UnityEngine;
using UnityEngine.Events;

namespace MoreRolesMod.Components
{
    [RegisterInIl2Cpp]
    public class PopeyeButton : MonoBehaviour
    {

        private SpriteRenderer m_SpriteRenderer = null;
        private BoxCollider2D m_collider = null;

        [HideFromIl2Cpp]
        public MoreRolesPlugin Plugin { get; internal set; }
        public SpriteRenderer renderer => m_SpriteRenderer;

        public PopeyeButton(IntPtr handle) : base(handle)
        {
        }

        public void Awake()
        {
            gameObject.layer = Layers.UI;
            var corner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
            var scale = gameObject.transform.lossyScale;

            gameObject.transform.position = corner + new Vector3(scale.x, scale.y);

            m_SpriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            m_SpriteRenderer.sprite = Assets.Popeye;

            m_collider = gameObject.AddComponent<BoxCollider2D>();
            m_collider.size = new Vector2(100, 100);

        }

        public void OnMouseDown()
        {
            System.Console.WriteLine("Squished that cat"); ;
        }

        public void Start()
        {
            gameObject.SetActive(true);
        }



        public void Update()
        {

        }
    }
}
