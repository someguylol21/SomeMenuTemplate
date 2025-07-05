using UnityEngine;
using UnityEngine.XR;

namespace SomeTemplate
{
    internal class Controls
    {
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

        public static bool XButton()
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
    }
}