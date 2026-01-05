using System;
using System.Runtime.CompilerServices;
using HarmonyLib;
using Il2CppCapuchinPlayFab;
using Il2CppPlayFab;
using MelonLoader;
using UnityEngine;

namespace NewTemplate.Patches
{
    [HarmonyPatch(typeof(GameObject))]
    [HarmonyPatch("CreatePrimitive", 0)]
    internal class ShaderFix : MonoBehaviour
    {
        private static void Postfix(GameObject __result)
        {
            __result.GetComponent<Renderer>().material.shader = Shader.Find("Unlit/Color");
            __result.GetComponent<Renderer>().material.color = new Color32(255, 127, 80, 255);
        }
    }

    [HarmonyPatch(typeof(Il2Cpp.AntiCheat))]
    [HarmonyPatch("Update", 0)]
    internal class LarsIsStinky
    {
        private static bool Prefix()
        {
            return false;
        }
    }
}