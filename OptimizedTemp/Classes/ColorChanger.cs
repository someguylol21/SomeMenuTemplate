﻿using MelonLoader;
using UnityEngine;

namespace SomeTemplate.Classes
{
    public class ColorChanger : TimedBehaviour
    {
        public override void Start()
        {
            base.Start();
            renderer = base.GetComponent<Renderer>();
            Update();
        }

        public override void Update()
        {
            base.Update();
            if (colorInfo != null)
            {
                Color color = new Gradient { colorKeys = colorInfo.colors }.Evaluate((Time.time / 2f) % 1);
                if (colorInfo.isRainbow)
                {
                    float h = (Time.frameCount / 180f) % 1f;
                    color = UnityEngine.Color.HSVToRGB(h, 1f, 1f);
                }
                renderer.material.color = color;
            }
        }

        public Renderer renderer;
        public ExtGradient colorInfo;
    }
}
