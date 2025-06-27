using MelonLoader;
using static SomeTemplate.Menu.Main;

namespace SomeTemplate.Mods
{
    internal class Global
    {
        public static void ReturnHome() // return home
        {
            buttonsType = 0;
        }

        public static void ModTemplate1()
        {
            MelonLogger.Msg("Hello, world! 1");
        }

        public static void ModTemplate2Enable()
        {
            MelonLogger.Msg("Hello, world! 2");
        }

        public static void ModTemplate2Disable()
        {
            MelonLogger.Msg("Goodbye, world! 2");
        }
    }
}
