using BepInEx;
using BepInEx.IL2CPP;
using HarmonyLib;
using Reactor;
using MoreRolesMod.Config;
using BepInEx.Logging;
using Reactor.Patches;
using Essentials.Options;
using System;
using System.Reflection;

namespace MoreRolesMod
{

    [BepInPlugin(Id)]
    [BepInProcess("Among Us.exe")]
    [BepInDependency(ReactorPlugin.Id)]
    public partial class MoreRolesPlugin : BasePlugin
    {
        public const string Id = "dev.kynet.moreroles";
        private const string KynetServerName = "Kynet (EU)";
        private const string ArtifactBuildNumberUrl = "https://jenkins.kynet.dev/job/MoreRolesMod/job/master/lastSuccessfulBuild/buildNumber";

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
        public static Version Version { get; } = Assembly.GetExecutingAssembly().GetName().Version;

        public override void Load()
        {
            LoadAssets();
            LoadGameConfig();
            AddCustomServerRegion();
            RegisterColors();

            // Disables the twitch advertisement in the game lobby
            CustomOption.ShamelessPlug = false;
            InitializeGameOptions();

            ReactorVersionShower.TextUpdated += UpdatReactVersionShowerText;
            RegisterInIl2CppAttribute.Register();
            RegisterCustomRpcAttribute.Register(this);

            Harmony.PatchAll();
        }

    }
}
