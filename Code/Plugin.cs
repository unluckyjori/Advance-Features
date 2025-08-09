using System;
using System.IO;
using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace AdvanceFeatures
{
    [BepInPlugin("com.example.advancefeatures", "Advance Features", "1.0.9")]
    public class Plugin : BaseUnityPlugin
    {

        public static ConfigEntry<bool> EnablePerformanceUI;
        public static ConfigEntry<bool> EnableDeathUI;
        public static ConfigEntry<bool> ShowDeathUsername;
        public static ConfigEntry<float> DeathVoiceSensitivity;
        public static ConfigEntry<float> BounceSmoothness;
        public static ConfigEntry<bool> ShowAvatars;
        public static ConfigEntry<bool> EnableAdvancedLogging;
        internal static ManualLogSource Log;
        private Harmony _harmony;
        private AssetBundle _assetBundle;

        private void Awake()
        {
            Log = Logger;
            Log.LogInfo("Initializing Advance Features plugin");

            EnablePerformanceUI = Config.Bind(
                "General",
                "EnablePerformanceReportUI",
                true,
                "Toggle the custom performance-report UI"
            );

            EnableDeathUI = Config.Bind(
                "General",
                "EnableDeathSpectateUI",
                true,
                "Toggle the custom death-spectate UI"
            );
            ShowDeathUsername = Config.Bind(
                "DeathScreen",
                "ShowUsernameUnderAvatar",
                true,
                "Enable or disable the player?s name under their avatar on the death spectate screen"
            );
            DeathVoiceSensitivity = Config.Bind(
                "DeathScreen",
                "VoiceSensitivity",
                10.0f,
                "How strongly avatars bounce in response to voice"
            );
            BounceSmoothness = Config.Bind(
                "DeathScreen",
                "BounceSmoothness",
                12.0f,
                "How quickly the avatar bounce reacts to voice volume. Higher = snappier bounce."
            );
            ShowAvatars = Config.Bind(
                "Performance Report UI",
                "ShowAvatars",
                false,
                "If true, fetch and display each player's Steam avatar on the performance report."
             );
            EnableAdvancedLogging = Config.Bind(
                "Logging",
                "EnableAdvancedLogging",
                false,
                "If true, logs when the mod does anything"
            );
            if (EnableAdvancedLogging.Value)
                Log.LogInfo("Advanced logging enabled");

            _harmony = new Harmony("com.example.advancefeatures");
            _harmony.PatchAll();
            Log.LogInfo("Harmony patches applied");

            string bundlePath = Path.Combine(Path.GetDirectoryName(Info.Location)!, "advancefeaturesassets");
            try
            {
                if (File.Exists(bundlePath))
                {
                    Log.LogInfo("Loading asset bundle for Advance Features");
                    _assetBundle = AssetBundle.LoadFromFile(bundlePath);
                    Endscreen.LoadAssets(_assetBundle);
                    DeathScreen.LoadAssets(_assetBundle);
                    Log.LogInfo($"Asset bundle has been found at {bundlePath}");
                }
                else
                {
                    Log.LogWarning($"Asset bundle not found at {bundlePath}");
                }
            }
            catch (Exception e)
            {
                Log.LogError("Failed to load asset bundle");
                Log.LogError(e);
            }
        }
    }
}