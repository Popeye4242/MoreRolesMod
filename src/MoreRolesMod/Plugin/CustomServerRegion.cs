using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace MoreRolesMod
{
    public partial class MoreRolesPlugin
    {
        private void AddCustomServerRegion()
        {
            var defaultRegions = ServerManager.DefaultRegions.ToList();
            var ip = GameConfig.Ip.Value;
            if (Uri.CheckHostName(ip).ToString() == "Dns")
            {
                foreach (var address in Dns.GetHostAddresses(GameConfig.Ip.Value))
                {
                    if (address.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
                        continue;
                    ip = address.ToString();
                    break;
                }
            }

            defaultRegions.Insert(0, new RegionInfo(
                "Custom", ip, new[]
                {
                    new ServerInfo($"Custom-Server", ip, GameConfig.Port.Value)
                })
            );
            defaultRegions.Insert(1, new RegionInfo(
                KynetServerName, ip, new[]
                {
                    new ServerInfo($"Kynet (EU)", Dns.GetHostEntry("amongus.kynet.dev").AddressList.First().ToString(), 22023)
                })
            );

            ServerManager.DefaultRegions = defaultRegions.ToArray();

            Log.LogMessage("Added Custom Server Regions");
        }
    }
}
