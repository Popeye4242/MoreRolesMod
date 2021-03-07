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
using MoreRolesMod;
using Reactor.Unstrip;
using System.IO;
using UnityEngine;
using Reactor.Extensions;
using System.Reflection;

namespace TestMod
{

    [BepInPlugin(Id)]
    [BepInProcess("Among Us.exe")]
    [BepInDependency(ReactorPlugin.Id)]
    public class HarmonyMain : BasePlugin
    {
        public const string Id = "dev.kynet.moreroles";
        private const string KynetServerName = "Kynet (EU)";

        public Harmony Harmony { get; } = new Harmony(Id);

        public static readonly GameConfig GameConfig = new GameConfig();

        public override void Load()
        {
            System.Console.WriteLine("Launching More Roles Mod");
            LoadAssets();
            System.Console.WriteLine("Loaded Assets");
            
            LoadGameConfig();
            System.Console.WriteLine("Loaded Game Config");

            CustomOption.ShamelessPlug = false;

            AddCustomServerRegion();
            System.Console.WriteLine("Added Custom Server Regions");

            Harmony.PatchAll();
        }

        private void LoadAssets()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "Assets", "morerolesmod");
            Assets.Bundle = AssetBundle.LoadFromFile(path);
            Assets.Popeye = Assets.Bundle.LoadAsset<Sprite>("Popeye").DontUnload();
            System.Console.WriteLine("{1}: {0}", Assets.Popeye == null, Assets.Popeye.name);
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
            defaultRegions.Insert(1, new RegionInfo(
                KynetServerName, ip, new[]
                {
                    new ServerInfo($"Kynet (EU)", "amongus.kynet.dev", 22023)
                })
            );

            ServerManager.DefaultRegions = defaultRegions.ToArray();
        }
    }
}
