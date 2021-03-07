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
using BepInEx.Logging;
using Reactor.Patches;
using UnityEngine.SceneManagement;

namespace TestMod
{

    [BepInPlugin(Id)]
    [BepInProcess("Among Us.exe")]
    [BepInDependency(ReactorPlugin.Id)]
    public class MoreRolesPlugin : BasePlugin
    {
        public const string Id = "dev.kynet.moreroles";
        private const string KynetServerName = "Kynet (EU)";
        private const string ArtifactBuildNumberUrl = "https://jenkins.kynet.dev/job/MoreRolesMod/job/master/lastSuccessfulBuild//buildNumber";

        public static readonly GameConfig GameConfig = new GameConfig();

        public MoreRolesPlugin()
        {
            Instance = this;
            Harmony = new Harmony(Id);
        }
        public static MoreRolesPlugin Instance { get; private set; }

        public Harmony Harmony { get; }

        public static ManualLogSource Logger => Instance.Log;

        public static MainMenuManager MainMenuManager { get; internal set; }

        public override void Load()
        {
            Log.LogMessage("Launching More Roles Mod");
            LoadAssets();
            Log.LogMessage("Loaded Assets");
            
            LoadGameConfig();
            Log.LogMessage("Loaded Game Config");

            CustomOption.ShamelessPlug = false;

            AddCustomServerRegion();
            Log.LogMessage("Added Custom Server Regions");

            ReactorVersionShower.TextUpdated += UpdatReactVersionShowerText;
            Harmony.PatchAll();
        }

        private void UpdatReactVersionShowerText(TextRenderer text)
        {
            System.Console.WriteLine("Updating version shower");
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            string strText = "[FFFFFFFF]More Roles Mod v" + version.ToString(3);
            using (WebClient client = new WebClient())
            {
                string build = client.DownloadString(ArtifactBuildNumberUrl);
                if (!string.Equals(version.Build, build))
                {
                    strText += " ([FF1111FF]Update available[])";
                }
            }
            text.Text = strText + Environment.NewLine + text.Text;;

        }

        private void LoadAssets()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "Assets", "morerolesmod");
            Assets.Bundle = AssetBundle.LoadFromFile(path);
            Assets.Popeye = Assets.Bundle.LoadAsset<Sprite>("Popeye").DontUnload();
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
                    new ServerInfo($"Kynet (EU)", Dns.GetHostEntry("amongus.kynet.dev").AddressList.First().ToString(), 22023)
                })
            );

            ServerManager.DefaultRegions = defaultRegions.ToArray();
        }
    }
}
