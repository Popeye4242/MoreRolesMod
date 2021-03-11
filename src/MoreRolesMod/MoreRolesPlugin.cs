using BepInEx;
using BepInEx.IL2CPP;
using HarmonyLib;
using Reactor;
using MoreRolesMod.Config;
using BepInEx.Logging;
using Reactor.Patches;

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

        public override void Load()
        {
            Log.LogMessage("Launching More Roles Mod");
            LoadAssets();
            LoadGameConfig();
            AddCustomServerRegion();
            RegisterColors();

            ReactorVersionShower.TextUpdated += UpdatReactVersionShowerText;
            RegisterInIl2CppAttribute.Register();
            RegisterCustomRpcAttribute.Register(this);

            Harmony.PatchAll();
        }

    }
}
