using System;
using System.Collections.Generic;
using System.Text;

namespace MoreRolesMod
{
    public partial class MoreRolesPlugin
    {
        private void LoadGameConfig()
        {
            GameConfig.Ip = Config.Bind("Custom", "Ipv4 or Hostname", "127.0.0.1");
            GameConfig.Port = Config.Bind("Custom", "Port", (ushort)22023);

            Log.LogMessage("Loaded Game Config");
        }
    }
}
