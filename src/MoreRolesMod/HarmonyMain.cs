using BepInEx;
using BepInEx.Configuration;
using BepInEx.IL2CPP;
using HarmonyLib;
using System;
using System.Linq;
using System.Net;
using Reactor;
using Essentials.Options;
using MoreRolesMod.Config;

namespace TestMod
{

    [BepInPlugin(Id)]
    [BepInProcess("Among Us.exe")]
    [BepInDependency(ReactorPlugin.Id)]
    public class HarmonyMain : BasePlugin
    {
        public const string Id = "dev.kynet.moreroles";

        public Harmony Harmony { get; } = new Harmony(Id);

        public static readonly GameConfig GameConfig = new GameConfig();

        public override void Load()
        {
            LoadGameConfig();

            CustomOption.ShamelessPlug = false;

            AddCustomServerRegion();

            Harmony.PatchAll();
        }

        private void LoadGameConfig()
        {
            GameConfig.Ip = Config.Bind("Custom", "Ipv4 or Hostname", "127.0.0.1");
            GameConfig.Port = Config.Bind("Custom", "Port", (ushort)22023);
        }

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

            ServerManager.DefaultRegions = defaultRegions.ToArray();
        }
    }
}
