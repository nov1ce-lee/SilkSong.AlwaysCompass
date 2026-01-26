using BepInEx;
using HarmonyLib;

namespace AlwaysCompass;

// TODO - adjust the plugin guid as needed
[BepInAutoPlugin(id: "io.github.nov1ce-lee.alwayscompass")]
public partial class AlwaysCompassPlugin : BaseUnityPlugin
{
    private void Awake()
    {
        // Put your initialization logic here
        Logger.LogInfo($"Plugin {Name} ({Id}) has loaded!");

        new Harmony(Id).PatchAll();
    }
}
