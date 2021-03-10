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
        [HideFromIl2Cpp]
        public MoreRolesPlugin Plugin { get; internal set; }

        public ExampleComponent(IntPtr ptr) : base(ptr)
        {
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
