using HarmonyLib;
using Il2Cpp;
using Il2CppFusion;
using Il2CppPhoton.Realtime;
using Il2CppPhoton.Voice;
using MelonLoader;
using MelonLoader.ICSharpCode.SharpZipLib.Zip;
using MelonLoader.Utils;
using NewTemplate.Classes;
using NewTemplate.Mods;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using static NewTemplate.Menu.Buttons;
using static NewTemplate.Settings;

namespace NewTemplate.Menu
{
    public enum MenuAudioType
    {
        ButtonClick, MenuClose, MenuOpen
    }

    [HarmonyPatch(typeof(Il2CppLocomotion.Player))]
    [HarmonyPatch("LateUpdate", MethodType.Normal)]
    public class Main : MonoBehaviour
    {
        public static ColorChanger colorChangerBackground;
        public static GameObject menu;
        public static GameObject menuBackground;
        public static GameObject canvasObject;
        public static Text menuTextComponent;
        public static Text tooltipTextComponent;
        public static Text fpsObject;
        public static int pageNumber = 0;
        public static int currentButtonCategory = 0;
        public static int _cachePlayerCount = 0;

        public static void Prefix()
        {
            // Initialize Menu
            try
            {
                if (FusionHub.InRoom && FusionHub.Instance != null && FusionHub.Instance.SpawnedPlayers != null && FusionHub.Instance.SpawnedPlayers.Count != 0 && (_cachePlayerCount != FusionHub.Instance.SpawnedPlayers.Count || Core.localPlayer == null))
                {
                    Core.players = FusionHub.Instance.SpawnedPlayers.ToArray().Select(p => p.Item1).ToArray();
                    Core.localPlayer = Core.players.ToArray().FirstOrDefault(player => player != null && player.IsLocalPlayer);
                }
                else
                {
                    Core.players = [];
                    Core.localPlayer = null;
                }
                bool toOpen = rightHanded ? Controls.BButton() : Controls.YButton();
                bool keyboardOpen = Input.GetKey(keyboardButton);
                if (menu == null)
                {
                    if (toOpen || keyboardOpen)
                    {
                        CreateMenuButGood();
                    }
                }
                else
                {
                    if (toOpen || keyboardOpen)
                    {
                        RecenterMenu(rightHanded, keyboardOpen);
                        if (!menu.active)
                        {
                            menu.SetActive(true);
                        }
                    }
                    else
                    {
                        if (menu.active)
                        {
                            menu.SetActive(false);
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                MelonLogger.Error(string.Format("{0} // Error initializing at {1}: {2}", PluginInfo.Name, exc.StackTrace, exc.Message));
            }

            // Constant
            try
            {
                // Pre-Execution
                if (fpsObject != null)
                {
                    fpsObject.text = "FPS: " + Mathf.Ceil(1f / Time.unscaledDeltaTime).ToString();
                }

                // Execute Enabled mods
                foreach (var buttonlist in buttons)
                {
                    foreach (ButtonInfo v in buttonlist)
                    {
                        if (v.enabled)
                        {
                            if (v.method != null)
                            {
                                try
                                {
                                    v.method.Invoke();
                                }
                                catch (Exception exc)
                                {
                                    MelonLogger.Error(string.Format("{0} // Error with mod {1} at {2}: {3}", PluginInfo.Name, v.buttonText, exc.StackTrace, exc.Message));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                MelonLogger.Error(string.Format("{0} // Error with executing mods at {1}: {2}", PluginInfo.Name, exc.StackTrace, exc.Message));
            }
        }
        public static List<Classes.Button> currentButtons = new();

        public static void CreateMenuButGood()
        {
            // Menu Holder
            menu = GameObject.CreatePrimitive(PrimitiveType.Cube);
            GameObject.Destroy(menu.GetComponent<Rigidbody>());
            GameObject.Destroy(menu.GetComponent<BoxCollider>());
            GameObject.Destroy(menu.GetComponent<Renderer>());
            menu.transform.localScale = new Vector3(0.1f, 0.3f, 0.3825f);

            // Menu Background
            menuBackground = GameObject.CreatePrimitive(PrimitiveType.Cube);
            GameObject.Destroy(menuBackground.GetComponent<Rigidbody>());
            GameObject.Destroy(menuBackground.GetComponent<BoxCollider>());
            menuBackground.transform.parent = menu.transform;
            menuBackground.transform.rotation = Quaternion.identity;
            menuBackground.transform.localScale = menuSize;
            menuBackground.GetComponent<Renderer>().material.color = backgroundColor.colors[0].color;
            menuBackground.transform.position = new Vector3(0.05f, 0f, 0f);
            //RoundObj(menuBackground);
            colorChangerBackground = menuBackground.AddComponent<ColorChanger>();
            colorChangerBackground.colorInfo = backgroundColor;
            colorChangerBackground.Start();
            // Canvas
            canvasObject = new GameObject();
            canvasObject.transform.parent = menu.transform;
            Canvas canvas = canvasObject.AddComponent<Canvas>();
            CanvasScaler canvasScaler = canvasObject.AddComponent<CanvasScaler>();
            canvasObject.AddComponent<GraphicRaycaster>();
            canvas.renderMode = RenderMode.WorldSpace;
            canvasScaler.dynamicPixelsPerUnit = 1000f;

            // Title and FPS
            menuTextComponent = new GameObject
            {
                transform =
                    {
                        parent = canvasObject.transform
                    }
            }.AddComponent<Text>();
            menuTextComponent.font = currentFont;
            menuTextComponent.text = PluginInfo.Name + " <color=grey>[</color><color=white>" + (pageNumber + 1).ToString() + "</color><color=grey>]</color>";
            menuTextComponent.fontSize = 1;
            menuTextComponent.color = textColors[0];
            menuTextComponent.supportRichText = true;
            menuTextComponent.fontStyle = FontStyle.Italic;
            menuTextComponent.alignment = TextAnchor.MiddleCenter;
            menuTextComponent.resizeTextForBestFit = true;
            menuTextComponent.resizeTextMinSize = 0;
            RectTransform component = menuTextComponent.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(0.28f, 0.05f);
            component.position = new Vector3(0.06f, 0f, 0.165f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

            if (fpsCounter)
            {
                fpsObject = new GameObject
                {
                    transform =
                    {
                        parent = canvasObject.transform
                    }
                }.AddComponent<Text>();
                fpsObject.font = currentFont;
                fpsObject.text = "FPS: " + Mathf.Ceil(1f / Time.unscaledDeltaTime).ToString();
                fpsObject.color = textColors[0];
                fpsObject.fontSize = 1;
                fpsObject.supportRichText = true;
                fpsObject.fontStyle = FontStyle.Italic;
                fpsObject.alignment = TextAnchor.MiddleCenter;
                fpsObject.horizontalOverflow = UnityEngine.HorizontalWrapMode.Overflow;
                fpsObject.resizeTextForBestFit = true;
                fpsObject.resizeTextMinSize = 0;
                RectTransform component2 = fpsObject.GetComponent<RectTransform>();
                component2.localPosition = Vector3.zero;
                component2.sizeDelta = new Vector2(0.28f, 0.02f);
                component2.position = new Vector3(0.06f, 0f, 0.135f);
                component2.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
            }

            // Buttons
            // Disconnect
            if (disconnectButton)
            {
                GameObject disconnectbutton = GameObject.CreatePrimitive(PrimitiveType.Cube);
                if (!Input.GetKey(KeyCode.Z))
                {
                    disconnectbutton.layer = 2;
                }
                UnityEngine.Object.Destroy(disconnectbutton.GetComponent<Rigidbody>());
                disconnectbutton.GetComponent<BoxCollider>().isTrigger = true;
                disconnectbutton.transform.parent = menu.transform;
                disconnectbutton.transform.rotation = Quaternion.identity;
                disconnectbutton.transform.localScale = new Vector3(0.09f, 0.9f, 0.08f);
                disconnectbutton.transform.localPosition = new Vector3(0.56f, 0f, 0.6f);
                disconnectbutton.GetComponent<Renderer>().material.color = buttonColors[0].colors[0].color;
                disconnectbutton.AddComponent<Classes.Button>().buttonType = ButtonType.DISCONNECT;
                ColorChanger colorChangerDisconnect = disconnectbutton.AddComponent<ColorChanger>();
                colorChangerDisconnect.colorInfo = buttonColors[0];
                colorChangerDisconnect.Start();

                Text discontext = new GameObject
                {
                    transform =
                            {
                                parent = canvasObject.transform
                            }
                }.AddComponent<Text>();
                discontext.text = "Disconnect";
                discontext.font = currentFont;
                discontext.fontSize = 1;
                discontext.color = textColors[0];
                discontext.alignment = TextAnchor.MiddleCenter;
                discontext.resizeTextForBestFit = true;
                discontext.resizeTextMinSize = 0;

                RectTransform rectt = discontext.GetComponent<RectTransform>();
                rectt.localPosition = Vector3.zero;
                rectt.sizeDelta = new Vector2(0.2f, 0.03f);
                rectt.localPosition = new Vector3(0.064f, 0f, 0.23f);
                rectt.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
                //RoundObj(disconnectbutton);
            }
            GameObject tooltip = GameObject.CreatePrimitive(PrimitiveType.Cube);
            tooltip.layer = 2;
            UnityEngine.Object.Destroy(tooltip.GetComponent<Rigidbody>());
            tooltip.GetComponent<BoxCollider>().isTrigger = true;
            tooltip.transform.parent = menu.transform;
            tooltip.transform.rotation = Quaternion.identity;
            tooltip.transform.localScale = new Vector3(0.09f, 0.9f, 0.13f);
            tooltip.transform.localPosition = new Vector3(0.56f, 0f, -0.58f);
            tooltip.GetComponent<Renderer>().material.color = buttonColors[0].colors[0].color;
            ColorChanger colorChangerTooltip = tooltip.AddComponent<ColorChanger>();
            colorChangerTooltip.colorInfo = buttonColors[0];
            colorChangerTooltip.Start();

            tooltipTextComponent = new GameObject
            {
                transform =
                            {
                                parent = canvasObject.transform
                            }
            }.AddComponent<Text>();
            tooltipTextComponent.text = "Menu Loaded";
            tooltipTextComponent.font = currentFont;
            tooltipTextComponent.fontSize = 1;
            tooltipTextComponent.color = textColors[0];
            tooltipTextComponent.alignment = TextAnchor.MiddleCenter;
            tooltipTextComponent.resizeTextForBestFit = true;
            tooltipTextComponent.resizeTextMinSize = 0;

            RectTransform rectt2 = tooltipTextComponent.GetComponent<RectTransform>();
            rectt2.localPosition = Vector3.zero;
            rectt2.sizeDelta = new Vector2(0.2f, 0.03f);
            rectt2.localPosition = new Vector3(0.064f, 0f, -0.22f);
            rectt2.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
            ////RoundObj(tooltip);
            // Page Buttons
            GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            if (!Input.GetKey(KeyCode.Z))
            {
                gameObject.layer = 2;
            }
            UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            gameObject.transform.parent = menu.transform;
            gameObject.transform.rotation = Quaternion.identity;
            gameObject.transform.localScale = new Vector3(0.09f, 0.2f, 0.9f);
            gameObject.transform.localPosition = new Vector3(0.56f, 0.65f, 0);
            gameObject.GetComponent<Renderer>().material.color = buttonColors[0].colors[0].color;
            gameObject.AddComponent<Classes.Button>().buttonType = ButtonType.PAGE_DOWN;

            ColorChanger colorChangerPrevPageBtn = gameObject.AddComponent<ColorChanger>();
            colorChangerPrevPageBtn.colorInfo = buttonColors[0];
            colorChangerPrevPageBtn.Start();

            Text prevPageText = new GameObject
            {
                transform =
                        {
                            parent = canvasObject.transform
                        }
            }.AddComponent<Text>();
            prevPageText.font = currentFont;
            prevPageText.text = "<";
            prevPageText.fontSize = 1;
            prevPageText.color = textColors[0];
            prevPageText.alignment = TextAnchor.MiddleCenter;
            prevPageText.resizeTextForBestFit = true;
            prevPageText.resizeTextMinSize = 0;
            component = prevPageText.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(0.2f, 0.03f);
            component.localPosition = new Vector3(0.064f, 0.195f, 0f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
            ////RoundObj(gameObject);
            gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            if (!Input.GetKey(KeyCode.Z))
            {
                gameObject.layer = 2;
            }
            UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            gameObject.transform.parent = menu.transform;
            gameObject.transform.rotation = Quaternion.identity;
            gameObject.transform.localScale = new Vector3(0.09f, 0.2f, 0.9f);
            gameObject.transform.localPosition = new Vector3(0.56f, -0.65f, 0);
            gameObject.GetComponent<Renderer>().material.color = buttonColors[0].colors[0].color;
            gameObject.AddComponent<Classes.Button>().buttonType = ButtonType.PAGE_UP;

            ColorChanger colorChangerNextPageBtn = gameObject.AddComponent<ColorChanger>();
            colorChangerNextPageBtn.colorInfo = buttonColors[0];
            colorChangerNextPageBtn.Start();

            Text nextPageText = new GameObject
            {
                transform =
                        {
                            parent = canvasObject.transform
                        }
            }.AddComponent<Text>();
            nextPageText.font = currentFont;
            nextPageText.text = ">";
            nextPageText.fontSize = 1;
            nextPageText.color = textColors[0];
            nextPageText.alignment = TextAnchor.MiddleCenter;
            nextPageText.resizeTextForBestFit = true;
            nextPageText.resizeTextMinSize = 0;
            component = nextPageText.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(0.2f, 0.03f);
            component.localPosition = new Vector3(0.064f, -0.195f, 0f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
            ////RoundObj(gameObject);
            // Mod Buttons
            for (int i = 0; i < buttonsPerPage; i++)
            {
                CreateButton(i * 0.1f);
            }
            RefreshText();
        }

        public static void CreateButton(float offset)
        {
            GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);

            if (!Input.GetKey(KeyCode.Z))
            {
                gameObject.layer = 2;
            }

            UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            gameObject.transform.parent = menu.transform;
            gameObject.transform.rotation = Quaternion.identity;
            gameObject.transform.localScale = new Vector3(0.09f, 0.9f, 0.08f);
            gameObject.transform.localPosition = new Vector3(0.56f, 0f, 0.28f - offset);
            Classes.Button buttonThing = gameObject.AddComponent<Classes.Button>();
            gameObject.name = "SomeButton";
            ColorChanger colorChanger = gameObject.AddComponent<ColorChanger>();
            colorChanger.Start();

            Text text = new GameObject
            {
                transform =
                {
                    parent = canvasObject.transform
                }
            }.AddComponent<Text>();
            text.font = currentFont;
            text.supportRichText = true;
            text.fontSize = 1;
            text.alignment = TextAnchor.MiddleCenter;
            text.fontStyle = FontStyle.Italic;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 0;
            RectTransform component = text.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(.2f, .03f);
            component.localPosition = new Vector3(.064f, 0, .111f - offset / 2.6f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
            buttonThing.buttonColorChanger = colorChanger;
            buttonThing.buttonTextComponent = text;
            currentButtons.Add(buttonThing);
            //RoundObj(gameObject);
        }

        public static int actualMenuPage = 0;
        public static void RefreshText()
        {
            if (menu != null)
            {
                menuTextComponent.text = PluginInfo.Name + " <color=grey>[</color><color=white>" + (pageNumber + 1).ToString() + "</color><color=grey>]</color>";
                ButtonInfo[] activeButtons = buttons[currentButtonCategory].Skip(pageNumber * buttonsPerPage).Take(buttonsPerPage).ToArray();

                for (int i = 0; i < buttonsPerPage; i++)
                {
                    if (activeButtons.Length > i)
                    {
                        //new Tuple<Classes.Button, Text, ColorChanger, GameObject>
                        currentButtons[i].gameObject.SetActive(true);
                        currentButtons[i].buttonTextComponent.enabled = true;

                        foreach (GameObject relatedObject in currentButtons[i].relatedObjects)
                        {
                            relatedObject.SetActive(true);
                        }

                        currentButtons[i].buttonTextComponent.text = activeButtons[i].buttonText;

                        if (activeButtons[i].enabled)
                        {
                            currentButtons[i].buttonColorChanger.colorInfo = buttonColors[1];
                        }
                        else
                        {
                            currentButtons[i].buttonColorChanger.colorInfo = buttonColors[0];
                        }
                        currentButtons[i].currentButtonInfo = activeButtons[i];
                    }
                    else
                    {
                        currentButtons[i].gameObject.SetActive(false);
                        currentButtons[i].buttonTextComponent.enabled = false;

                        foreach (GameObject relatedObject in currentButtons[i].relatedObjects)
                        {
                            relatedObject.SetActive(false);
                        }
                        currentButtons[i].currentButtonInfo = null;
                    }
                }
            }
        }

        public static void RecenterMenu(bool isRightHanded, bool isKeyboardCondition)
        {
            if (!isKeyboardCondition)
            {
                if (!isRightHanded)
                {
                    menu.transform.position = Il2CppLocomotion.Player.Instance.LeftTarget.position;
                    menu.transform.rotation = Il2CppLocomotion.Player.Instance.LeftTarget.rotation;
                }
                else
                {
                    menu.transform.position = Il2CppLocomotion.Player.Instance.RightTarget.position;
                    Vector3 rotation = Il2CppLocomotion.Player.Instance.RightTarget.rotation.eulerAngles;
                    rotation += new Vector3(0f, 0f, 180f);
                    menu.transform.rotation = Quaternion.Euler(rotation);
                }
            }
            else
            {
                menu.transform.rotation = Il2CppLocomotion.Player.Instance.playerCam.transform.rotation * Quaternion.Euler(-90f, 90f, 0f);
                Transform playerThing = Il2CppLocomotion.Player.Instance.playerCam.transform;
                menu.transform.position = playerThing.position + playerThing.forward * 0.5f;
                
                if (Mouse.current.leftButton.isPressed)
                {
                    Ray ray = Il2CppLocomotion.Player.Instance.playerCam.ScreenPointToRay(Mouse.current.position.ReadValue());
                    RaycastHit hit;
                    bool worked = Physics.Raycast(ray, out hit, 100, -1, queryTriggerInteraction: QueryTriggerInteraction.Collide);
                    if (worked)
                    {
                        Classes.Button collide = hit.transform.gameObject.GetComponent<Classes.Button>();
                        if (collide != null)
                        {
                            collide.ClickButton();
                        }
                    }
                }
            }
        }

        public static void Toggle(NewTemplate.Classes.Button button)
        {
            int lastPage = ((buttons[currentButtonCategory].Count + buttonsPerPage - 1) / buttonsPerPage) - 1;
            if (button.buttonType == ButtonType.PAGE_DOWN)
            {
                pageNumber--;
                if (pageNumber < 0)
                {
                    pageNumber = lastPage;
                }
                tooltipTextComponent.text = "Shows the previous page";
            }
            else if (button.buttonType == ButtonType.PAGE_UP)
            {
                pageNumber++;
                if (pageNumber > lastPage)
                {
                    pageNumber = 0;
                }
                tooltipTextComponent.text = "Shows the next page";
            }
            else if (button.buttonType == ButtonType.DISCONNECT)
            {
                tooltipTextComponent.text = "Disconnects you from the room (TO BE IMPLEMENTED, OLD VERSION IS BANNABLE EASILY)";
            }
            else
            {
                ButtonInfo target = button.currentButtonInfo;
                if (target != null)
                {
                    tooltipTextComponent.text = target.toolTip;
                    if (target.isTogglable)
                    {
                        target.enabled = !target.enabled;
                        if (target.enabled)
                        {
                            if (target.enableMethod != null)
                            {
                                try { target.enableMethod.Invoke(); } catch { }
                            }
                        }
                        else
                        {
                            if (target.disableMethod != null)
                            {
                                try { target.disableMethod.Invoke(); } catch { }
                            }
                        }
                    }
                    else
                    {
                        if (target.method != null)
                        {
                            try { target.method.Invoke(); } catch { }
                        }
                    }
                }
                else
                {
                    MelonLogger.Error("Button component does not exist");
                }
            }
            RefreshText();
        }

        public static GradientColorKey[] GetSolidGradientKey(Color color)
        {
            return new GradientColorKey[] { new GradientColorKey(color, 0f), new GradientColorKey(color, 1f) };
        }

        public static ExtGradient GetSolidExtGradient(Color color)
        {
            return new ExtGradient { colors = GetSolidGradientKey(color) };
        }
    }
}

