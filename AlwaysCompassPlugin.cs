using BepInEx;
using HarmonyLib;
using GlobalSettings;

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

    // Patch 1: 拦截 ToolItem.IsEquipped
    [HarmonyPatch(typeof(ToolItem), "IsEquipped", MethodType.Getter)]
    public static class ToolItemIsEquippedPatch
    {
        public static bool Prefix(ToolItem __instance, ref bool __result)
        {
            // Check if mod is enabled
            if (AlwaysCompassPlugin.Instance != null && !AlwaysCompassPlugin.Instance.IsEnabled.Value)
            {
                return true;
            }

            // 如果是罗盘
            if (__instance == Gameplay.CompassTool)
            {
                __result = true;  // 强制返回"已装备"
                return false;     // 跳过原方法
            }
            
            return true;  // 不是罗盘，执行原方法
        }
    }

    // // Patch 2: 拦截 ToolStatus.IsEquipped（备用）
    // [HarmonyPatch(typeof(ToolItemManager.ToolStatus), "IsEquipped", MethodType.Getter)]
    // public static class ToolStatusIsEquippedPatch
    // {
    //     public static bool Prefix(ToolItemManager.ToolStatus __instance, ref bool __result)
    //     {
    //         // Check if mod is enabled
    //         if (AlwaysCompassPlugin.Instance != null && !AlwaysCompassPlugin.Instance.IsEnabled.Value)
    //         {
    //             return true;
    //         }

    //         // 如果这个状态对象管理的是罗盘
    //         if (__instance.tool == Gameplay.CompassTool)
    //         {
    //             __result = true;
    //             return false;
    //         }
            
    //         return true;
    //     }
    // }
}
