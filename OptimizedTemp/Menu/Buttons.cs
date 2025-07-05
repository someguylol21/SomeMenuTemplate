using MelonLoader;
using SomeTemplate.Classes;
using SomeTemplate.Mods;
using static SomeTemplate.Settings;

namespace SomeTemplate.Menu
{
    internal class Buttons
    {
        public static ButtonInfo[][] buttons = new ButtonInfo[][]
        {
            new ButtonInfo[] { // Main Mods
                new ButtonInfo { buttonText = "Settings", method =() => SettingsMods.EnterSettings(), isTogglable = false, toolTip = "Opens the main settings page for the menu."},
                new ButtonInfo { buttonText = "mod template not toggable", method =() => Global.ModTemplate1(), isTogglable = false},
                new ButtonInfo { buttonText = "mod template toggable", enableMethod =() => Global.ModTemplate2Enable(), disableMethod =() => Global.ModTemplate2Disable()},
                new ButtonInfo { buttonText = "regular placeholder 2", isTogglable = false},
                new ButtonInfo { buttonText = "togglable placeholder 2"},
                new ButtonInfo { buttonText = "regular placeholder 3", isTogglable = false},
                new ButtonInfo { buttonText = "togglable placeholder 3"},
                new ButtonInfo { buttonText = "regular placeholder 4", isTogglable = false},
                new ButtonInfo { buttonText = "togglable placeholder 4"},
                new ButtonInfo { buttonText = "regular placeholder 5", isTogglable = false},
                new ButtonInfo { buttonText = "togglable placeholder 5"},
                new ButtonInfo { buttonText = "regular placeholder 6", isTogglable = false},
                new ButtonInfo { buttonText = "togglable placeholder 6"},
            },

            new ButtonInfo[] { // Settings
                new ButtonInfo { buttonText = "Return to Main", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "Returns to the main page of the menu."},
                new ButtonInfo { buttonText = "Menu", method =() => SettingsMods.MenuSettings(), isTogglable = false, toolTip = "Opens the settings for the menu."},
            },

            new ButtonInfo[] { // Menu Settings
                new ButtonInfo { buttonText = "Return to Settings", method =() => SettingsMods.EnterSettings(), isTogglable = false, toolTip = "Returns to the main settings page for the menu."},
                new ButtonInfo { buttonText = "Right Hand", enableMethod =() => SettingsMods.RightHand(), disableMethod =() => SettingsMods.LeftHand(), toolTip = "Puts the menu on your right hand."},
                new ButtonInfo { buttonText = "FPS Counter", enableMethod =() => SettingsMods.EnableFPSCounter(), disableMethod =() => SettingsMods.DisableFPSCounter(), enabled = fpsCounter, toolTip = "Toggles the FPS counter."},
                new ButtonInfo { buttonText = "Disconnect Button", enableMethod =() => SettingsMods.EnableDisconnectButton(), disableMethod =() => SettingsMods.DisableDisconnectButton(), enabled = disconnectButton, toolTip = "Toggles the disconnect button."},
            },
        };
    }
}
