using Il2CppInterop.Runtime.Injection;
using MelonLoader;
using SomeTemplate;
using System.Text.RegularExpressions;

[assembly: MelonInfo(typeof(SomeTemplate.Core), PluginInfo.Name, PluginInfo.Version, PluginInfo.Owner, null)]
[assembly: MelonGame("Duttbust", "Capuchin")]

namespace SomeTemplate
{
    public class Core : MelonMod
    {
        private readonly HarmonyLib.Harmony harmony = new(PluginInfo.HarmonyName);

        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("Initialized.");
            LoggerInstance.Msg($"Created harmony (lars reference) with name: {PluginInfo.HarmonyName}");
            ClassInjector.RegisterTypeInIl2Cpp<SomeTemplate.Classes.ColorChanger>();
            ClassInjector.RegisterTypeInIl2Cpp<SomeTemplate.Classes.TimedBehaviour>();
            ClassInjector.RegisterTypeInIl2Cpp<SomeTemplate.Classes.Button>();
            harmony.PatchAll();
        }
    }
}