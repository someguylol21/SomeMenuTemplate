using System;
using UnityEngine;
using static NewTemplate.Menu.Main;

namespace NewTemplate.Classes
{
    public class ExtGradient
    {
        public GradientColorKey[] colors = GetSolidGradientKey(new Color32(0, 0, 0, 255));

        public bool isRainbow = false;
    }
}
