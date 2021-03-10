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
    public class ExampleComponent : MonoBehaviour
    {

        SpriteRenderer m_SpriteRenderer;

        public ExampleComponent(IntPtr ptr) : base(ptr)
        {
        }

        [HideFromIl2Cpp]
        public MoreRolesPlugin Plugin { get; internal set; }
        public SpriteRenderer renderer => m_SpriteRenderer;
        void Awake()
        {
            SpriteRenderer sprRend = gameObject.AddComponent<SpriteRenderer>();
            sprRend.color = new Color(0.9f, 0.9f, 0.9f, 1.0f);
        }
        private void Start()
        {
            //Fetch the SpriteRenderer from the GameObject
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
            //Set the GameObject's Color quickly to a set Color (blue)
            m_SpriteRenderer.sprite = Assets.Popeye;
            System.Console.WriteLine("started component: {0}", m_SpriteRenderer == null);

        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F3) && AmongUsClient.Instance && PlayerControl.LocalPlayer)
            {
                Plugin.Log.LogWarning("Sending example rpc");
                Rpc<TestRpc>.Instance.Send(new TestRpc.TestData("Cześć :)"));
                Rpc<TestRpc>.Instance.SendTo(AmongUsClient.Instance.HostId, new TestRpc.TestData("host :O"));
            }
        }
    }
}
