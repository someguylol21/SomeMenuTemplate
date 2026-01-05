using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using MelonLoader;
using System.Linq;

[assembly: MelonInfo(typeof(NewTemplate.Core), NewTemplate.PluginInfo.Name, NewTemplate.PluginInfo.Version, NewTemplate.PluginInfo.Credits, null)]
[assembly: MelonGame("Duttbust", "Capuchin")]

namespace NewTemplate
{
    public class Core : MelonMod
    {
        public static Il2Cpp.FusionPlayer[] players = [];
        public static FusionPlayer localPlayer;
        private readonly HarmonyLib.Harmony harmony = new(PluginInfo.HarmonyName);

        public override void OnInitializeMelon()
        {
            ClassInjector.RegisterTypeInIl2Cpp<NewTemplate.Classes.ColorChanger>();
            ClassInjector.RegisterTypeInIl2Cpp<NewTemplate.Classes.TimedBehaviour>();
            ClassInjector.RegisterTypeInIl2Cpp<NewTemplate.Classes.Button>();
            ClassInjector.RegisterTypeInIl2Cpp<NewTemplate.Classes.ClampColor>();
            ClassInjector.RegisterTypeInIl2Cpp<NewTemplate.Classes.EspObject>();
            LoggerInstance.Msg("Initialized.");
            harmony.PatchAll();
        }
    }
}