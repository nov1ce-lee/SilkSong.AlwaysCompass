using BepInEx;
using HarmonyLib;

namespace AlwaysCompass;

[BepInAutoPlugin(id: "io.github.nov1ce-lee.alwayscompass")]
    public partial class AlwaysCompassPlugin : BaseUnityPlugin
    {
        public static AlwaysCompassPlugin Instance { get; private set; }
        public BepInEx.Configuration.ConfigEntry<bool> IsEnabled { get; private set; }

        private void Awake()
        {
            Instance = this;
            // Configuration
            IsEnabled = Config.Bind("General", "Enabled", true, "Enable or disable the Always Compass mod");

            // Put your initialization logic here
            Logger.LogInfo($"Plugin {Name} ({Id}) has loaded!");

            new Harmony(Id).PatchAll();
        }
    }
