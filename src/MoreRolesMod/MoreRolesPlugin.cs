using BepInEx;
using BepInEx.Configuration;
using BepInEx.IL2CPP;
using HarmonyLib;
using System;
using System.Linq;
using System.Net;
using Reactor;
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
using MoreRolesMod.Components;

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
