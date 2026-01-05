using MelonLoader;
using UnityEngine;

namespace NewTemplate.Classes
{
    public class ColorChanger : TimedBehaviour
    {
        public Color currentColor = new(1f, 1f, 1f, 1f);
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
                Color color;
                if (colorInfo.isRainbow)
                {
                    float h = (Time.frameCount / 180f) % 1f;
                    color = UnityEngine.Color.HSVToRGB(h, 1f, 1f);
                }
                else
                {
                    color = new Gradient { colorKeys = colorInfo.colors }.Evaluate((Time.time / 2f) % 1);
                }
                renderer.material.color = color;
                currentColor = color;
            }
        }

        public Renderer renderer;
        public ExtGradient colorInfo;
    }
}
