using MelonLoader;
using NewTemplate.Classes;
using NewTemplate.Mods;
using static NewTemplate.Settings;

namespace NewTemplate.Menu
{
    internal class Buttons
    {
        public static List<List<ButtonInfo>> buttons = new List<List<ButtonInfo>>
        {
            new List<ButtonInfo> // main page, put stuff here
            {
                new ButtonInfo { buttonText = "Settings", method = delegate { SettingsMods.GoTo(1); }, isTogglable = false},
                new ButtonInfo { buttonText = "Regular Button", method = delegate { Mods.Global.TestButton(); }, isTogglable = false},
                new ButtonInfo { buttonText = "Toggleable Button", enableMethod = delegate { Mods.Global.ToggableTestButtonOn(); }, disableMethod = delegate { Mods.Global.ToggableTestButtonOff(); }},
                new ButtonInfo { buttonText = "Regular Button 2", method = delegate { Mods.Global.TestButton(); }, isTogglable = false},
                new ButtonInfo { buttonText = "Toggleable Button 2", enableMethod = delegate { Mods.Global.ToggableTestButtonOn(); }, disableMethod = delegate { Mods.Global.ToggableTestButtonOff(); }},
                new ButtonInfo { buttonText = "Regular Button 3", method = delegate { Mods.Global.TestButton(); }, isTogglable = false},
                new ButtonInfo { buttonText = "Toggleable Button 3", enableMethod = delegate { Mods.Global.ToggableTestButtonOn(); }, disableMethod = delegate { Mods.Global.ToggableTestButtonOff(); }},
                new ButtonInfo { buttonText = "Regular Button 4", method = delegate { Mods.Global.TestButton(); }, isTogglable = false},
                new ButtonInfo { buttonText = "Toggleable Button 4", enableMethod = delegate { Mods.Global.ToggableTestButtonOn(); }, disableMethod = delegate { Mods.Global.ToggableTestButtonOff(); }},
                new ButtonInfo { buttonText = "Regular Button 5", method = delegate { Mods.Global.TestButton(); }, isTogglable = false},
                new ButtonInfo { buttonText = "Toggleable Button 5", enableMethod = delegate { Mods.Global.ToggableTestButtonOn(); }, disableMethod = delegate { Mods.Global.ToggableTestButtonOff(); }},
                new ButtonInfo { buttonText = "Regular Button 6", method = delegate { Mods.Global.TestButton(); }, isTogglable = false},
                new ButtonInfo { buttonText = "Toggleable Button 6", enableMethod = delegate { Mods.Global.ToggableTestButtonOn(); }, disableMethod = delegate { Mods.Global.ToggableTestButtonOff(); }},
                new ButtonInfo { buttonText = "Print Players", method = delegate { Mods.Global.TestPlayers(); }, isTogglable = false}
            },
            new List<ButtonInfo> // settings
            {
                new ButtonInfo { buttonText = "Go Back", method = delegate { SettingsMods.GoTo(0); }, isTogglable = false},
                new ButtonInfo { buttonText = "Right Hand Menu", enableMethod = delegate { SettingsMods.RightHandMenu(true); }, disableMethod = delegate { SettingsMods.RightHandMenu(false); }}
            },

        };
    }
}
