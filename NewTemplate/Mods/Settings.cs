using static NewTemplate.Menu.Main;
using static NewTemplate.Settings;

namespace NewTemplate.Mods
{
    internal class SettingsMods
    {
        public static void GoTo(int page)
        {
            currentButtonCategory = page;
        }
        
        public static void RightHandMenu(bool b)
        {
            rightHanded = b;
        }
    }
}
