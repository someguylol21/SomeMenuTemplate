using Il2Cpp;
using Il2CppCapuchinPlayFab;
using Il2CppDissonance.Integrations.Offline;
using Il2CppFusion;
using Il2CppMono.Security.Protocol.Ntlm;
using Il2CppPhoton.Voice;
using Il2CppPhoton.Voice.Fusion;
using Il2CppPhoton.Voice.Unity;
using Il2CppPhoton.Voice.Unity.UtilityScripts;
using Il2CppPlayFab;
using Il2CppPlayFab.ClientModels;
using Il2CppPlayFab.ProgressionModels;
using Il2CppSteamworks;
using Il2CppTMPro;
using Il2CppVLB;
using Il2CppVLB_Samples;
using MelonLoader;
using MelonLoader.Utils;
using NewTemplate.Classes;
using NewTemplate.Menu;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.Networking;
using UnityEngine.Playables;
using UnityEngine.XR;
using static NewTemplate.Menu.Main;


namespace NewTemplate.Mods
{
    internal class Global
    {
        public static void ReturnHome() // return home
        {
            currentButtonCategory = 0;
        }

        public static void TestPlayers()
        {
            MelonLogger.Msg("i hate this why wont this work");
            if (FusionHub.InRoom)
            {
                MelonLogger.Msg("printing players");
                MelonLogger.Msg($"localplayer: {Core.localPlayer.Username}");
                foreach (var plr in Core.players)
                {
                    MelonLogger.Msg($"Player: {plr.Username}");
                }
            }
            else
            {
                MelonLogger.Msg("Not in room!");
            }
        }
        public static void TestButton()
        {
            MelonLogger.Msg("Clicked regular button");
        }
        public static void ToggableTestButtonOn()
        {
            MelonLogger.Msg("Clicked toggable button (ON)");
        }
        public static void ToggableTestButtonOff()
        {
            MelonLogger.Msg("Clicked toggable button (OFF)");
        }
    }
}