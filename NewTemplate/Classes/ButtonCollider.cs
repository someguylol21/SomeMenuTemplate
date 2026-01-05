using NewTemplate.Mods;
using UnityEngine;
using NewTemplate.Menu;
using static NewTemplate.Menu.Main;
using static NewTemplate.Settings;
using Il2Cpp;
using MelonLoader;
using UnityEngine.XR;
using UnityEngine.UI;

public enum ButtonType
{
	PAGE_UP,
	PAGE_DOWN,
	NORMAL,
	DISCONNECT
}

namespace NewTemplate.Classes
{
	public class Button : MonoBehaviour
	{
		public ButtonType buttonType = ButtonType.NORMAL;
		public ColorChanger buttonColorChanger;
		public Text buttonTextComponent;
		public ButtonInfo currentButtonInfo = null;
		internal List<GameObject> relatedObjects = new List<GameObject>();
		public static float buttonCooldown = 0f;

		internal void ClickButton()
		{
			if (Time.time > buttonCooldown && menu != null) // collider == buttonCollider
			{
				buttonCooldown = Time.time + 0.2f;
				if (rightHanded)
					InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).SendHapticImpulse(0, 0.25f, 0.1f);
				else
					InputDevices.GetDeviceAtXRNode(XRNode.RightHand).SendHapticImpulse(0, 0.25f, 0.1f);
				Toggle(this);
			}
		}
		public void OnTriggerEnter(Collider collider)
		{
			if ((rightHanded && collider.gameObject.name.Contains("LFingerTip")) || (!rightHanded && collider.gameObject.name.Contains("RFingerTip")))
			{
				ClickButton();
			}
		}
	}
}
