using NewTemplate.Classes;
using UnityEngine;
using static NewTemplate.Menu.Main;

namespace NewTemplate
{
    internal class Settings
    {
        public static float colMult = 1f;
        public static ExtGradient backgroundColorStatic = GetSolidExtGradient(new Color32(70, 223, 0, 255));

        public static ExtGradient background = new ExtGradient
        {
            colors = new GradientColorKey[]
            {
                new GradientColorKey(new Color32(30, 30, 30, 255), 0f),
                new GradientColorKey(new Color32(100, 100, 100, 255), 0.5f),
                new GradientColorKey(new Color32(30, 30, 30, 255), 1f)
            }
        };

        public static ExtGradient backgroundColor = background;
        public static ExtGradient[] buttonColors = new[]
        {
            GetSolidExtGradient(new Color32(40, 40, 40, 255)), // Disabled
            GetSolidExtGradient(new Color32(70, 70, 70, 255)) // Enabled
        };
        public static Color[] textColors = new Color[]
        {
            Color.white, // Disabled
            Color.white // Enabled
        };

        public static Font currentFont = Resources.GetBuiltinResource<Font>("Arial.ttf");

        public static bool fpsCounter = true;
        public static bool disconnectButton = true;
        public static bool rightHanded = false;
        public static KeyCode keyboardButton = KeyCode.X;
        public static Vector3 menuSize = new Vector3(0.1f, 1f, 1f); // Depth, Width, Height
        public static int buttonsPerPage = 8;
    }
}
