using SomeTemplate.Mods;
using UnityEngine;
using UnityEngine.XR.OpenXR.Input;
using static SomeTemplate.Menu.Main;
using static SomeTemplate.Settings;

namespace SomeTemplate.Classes
{
	public class Button : MonoBehaviour
	{
		public string relatedText = "TestButton";

        public static float buttonCooldown = 0f;
		public void OnTriggerEnter(Collider collider)
		{
			if (Time.time > buttonCooldown && collider == buttonCollider && menu != null)
			{
                buttonCooldown = Time.time + 0.2f;
				Toggle(this.relatedText);
            }
		}
	}
}
