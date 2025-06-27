using Il2CppInterop.Runtime.Injection;
using MelonLoader;
using SomeTemplate;

[assembly: MelonInfo(typeof(SomeTemplate.Core), PluginInfo.Name, PluginInfo.Version, PluginInfo.Credits, null)]
[assembly: MelonGame("Duttbust", "Capuchin")]

namespace SomeTemplate
{
    public class Core : MelonMod
    {
        private readonly HarmonyLib.Harmony harmony = new("some.template.harmony");

        public override void OnInitializeMelon()
        {
            ClassInjector.RegisterTypeInIl2Cpp<SomeTemplate.Classes.ColorChanger>();
            ClassInjector.RegisterTypeInIl2Cpp<SomeTemplate.Classes.TimedBehaviour>();
            ClassInjector.RegisterTypeInIl2Cpp<SomeTemplate.Classes.Button>();
            LoggerInstance.Msg("Initialized.");
            harmony.PatchAll();
        }
    }
}