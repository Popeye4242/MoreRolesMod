using Reactor.Extensions;
using Reactor.Unstrip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace MoreRolesMod
{
    public partial class MoreRolesPlugin
    {
        private void LoadAssets()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "Assets", "morerolesmod");
            Assets.Bundle = AssetBundle.LoadFromFile(path);
            Assets.Popeye = Assets.Bundle.LoadAsset<Sprite>("Popeye").DontUnload();

            Log.LogMessage("Loaded Assets");
        }
    }
}
