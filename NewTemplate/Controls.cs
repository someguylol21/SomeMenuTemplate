using UnityEngine;
using UnityEngine.XR;

namespace NewTemplate
{
    internal class Controls
    {
        public static bool toggle = false;
        public static bool holding = false;

        public static bool BButton()
        {
            bool Value;
            InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.secondaryButton, out Value);
            return Value;
        }

        public static bool AButton()
        {
            bool Value;
            InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.primaryButton, out Value);
            return Value;
        }

        public static bool XButton() // dont use, used in emote mod
        {
            bool Value;
            InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(CommonUsages.primaryButton, out Value);
            return Value;
        }

        public static bool YButton()
        {
            bool Value;
            InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(CommonUsages.secondaryButton, out Value);
            return Value;
        }

        public static bool LeftJoystick()
        {
            bool Value;

            InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(CommonUsages.primary2DAxisClick, out Value);
            return Value;
        }

        public static bool RGrip()
        {
            bool Value;
            InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.gripButton, out Value);
            return Value;
        }

        public static bool LGrip()
        {
            bool Value;
            InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(CommonUsages.gripButton, out Value);
            return Value;
        }
        public static bool RightJoystick()
        {
            bool Value;
            InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.primary2DAxisClick, out Value);
            if (Value)
            {
                if (!holding)
                {
                    toggle = !toggle;
                    holding = true;
                }
            }
            else
            {
                holding = false;
            }
            return toggle;
        }

        public static Vector2 LeftJoystickAxis()
        {
            Vector2 Value;


            InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(CommonUsages.primary2DAxis, out Value);

            return Value;
        }

        public static Vector2 RightJoystickAxis()
        {
            Vector2 Value;


            InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.primary2DAxis, out Value);

            return Value;
        }

        public static bool LeftTrigger()
        {
            bool Value;


            InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(CommonUsages.triggerButton, out Value);

            return Value;
        }

        public static bool RightTrigger()
        {
            bool Value;

            InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.triggerButton, out Value);

            return Value;
        }

        public static bool RightGrip()
        {
            bool Value;

            InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.gripButton, out Value);

            return Value;
        }
    }
}