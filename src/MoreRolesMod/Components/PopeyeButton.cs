using MoreRolesMod.Rpc;
using Reactor;
using System;
using System.Collections.Generic;
using System.Text;
using UnhollowerBaseLib.Attributes;
using UnityEngine;

namespace MoreRolesMod.Components
{
    [RegisterInIl2Cpp]
    public class PopeyeButton : MonoBehaviour
    {

        private GameObject button = null;
        private SpriteRenderer m_SpriteRenderer = null;

        [HideFromIl2Cpp]
        public MoreRolesPlugin Plugin { get; internal set; }
        public SpriteRenderer renderer => m_SpriteRenderer;

        public PopeyeButton(IntPtr handle) : base(handle)
        {
        }

        public void Start()
        {
            button = Instantiate(new GameObject(), gameObject.transform);
            button.name = "asdfg";
            var local = button.transform.localPosition;
            // make button half size
            button.transform.localScale /= 2;
            button.transform.localPosition = new Vector3((local.x + 1.3f) * -1, local.y, local.z) + new Vector3(0.125f, 0.125f);

            m_SpriteRenderer = button.AddComponent<SpriteRenderer>();
            m_SpriteRenderer.sprite = Assets.Popeye;
        }
        public void Update()
        {

        }
    }
}
