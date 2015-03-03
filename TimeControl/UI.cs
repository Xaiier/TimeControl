using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace TimeControl
{
    [KSPAddon(KSPAddon.Startup.MainMenu, true)]
    public class UI : MonoBehaviour
    {
        private bool visible = true;
        private bool settingsOpen = false;
        private int mode = 0;

        private Rect menuWindowPosition;
        private Rect flightWindowPosition;
        private Rect settingsWindowPosition;

        private static Rect closeButton = new Rect(5, 5, 10, 10);
        private static Rect settingsButton = new Rect(182, -1, 20, 20);
        private static Rect mode0Button = new Rect(10, -1, 25, 20);
        private static Rect mode1Button = new Rect(25, -1, 25, 20);
        private static Rect mode2Button = new Rect(40, -1, 25, 20);

        private void Awake()
        {
            UnityEngine.Object.DontDestroyOnLoad(this);

            print("UI class initialized");
        }

        private void Update()
        {
            sizeWindows();
        }

        private void OnGUI()
        {
            GUI.skin = HighLogic.Skin; //use yellow text from KSP skin for the FPS display

            //if (Settings.showFPS && fpsVisible && HighLogic.LoadedSceneIsFlight)
            //{
            //    Settings.fpsPosition.x = parseSTOI(Settings.fpsX);
            //    Settings.fpsPosition.y = parseSTOI(Settings.fpsY);
            //    GUI.Label(Settings.fpsPosition, Mathf.Floor(PerformanceMonitor.fps).ToString());
            //}

            GUI.skin = null; //use Unity default skin

            if (visible)
            {
                if (HighLogic.LoadedScene == GameScenes.TRACKSTATION || HighLogic.LoadedScene == GameScenes.SPACECENTER)
                {
                    menuWindowPosition = constrainToScreen(GUILayout.Window("Time Control".GetHashCode(), menuWindowPosition, onUtilGUI, "Time Control"));
                }

                if (HighLogic.LoadedSceneIsFlight)
                {
                    flightWindowPosition = constrainToScreen(GUILayout.Window("Time Control".GetHashCode() + 1, flightWindowPosition, onUtilGUI, "Time Control"));

                    if (settingsOpen)
                    {
                        settingsWindowPosition = constrainToScreen(GUILayout.Window("Time Control".GetHashCode() + 2, settingsWindowPosition, onSettingsGUI, "Time Control Settings"));
                    }
                }
            }

            GUI.skin = HighLogic.Skin;//use KSP skin - prevents bugs with other mods that might not bother to set skin
        }

        private void onUtilGUI(int windowId)
        {
            //Minimize button
            if (GUI.Button(closeButton, ""))
            {
                visible = !visible; //ALSO TOOLBAR SUPPORT STUFF
            }

            Color bc = GUI.backgroundColor;
            Color cc = GUI.contentColor;
            GUI.backgroundColor = Color.clear;
            //Settings button
            if (!settingsOpen)
            {
                    GUI.contentColor = new Color(0.5f, 0.5f, 0.5f);
            }
            if (GUI.Button(settingsButton, "?"))
            {
                settingsOpen = !settingsOpen;
            }
            GUI.contentColor = cc;
            //Slow-mo mode
            if (mode != 0)
            {
                GUI.contentColor = new Color(0.5f, 0.5f, 0.5f);
            }
            if (GUI.Button(mode0Button, "S"))
            {
                mode = 0;
            }
            GUI.contentColor = cc;
            //Hyper mode
            if (mode != 1)
            {
                GUI.contentColor = new Color(0.5f, 0.5f, 0.5f);
            }
            if (GUI.Button(mode1Button, "H"))
            {
                mode = 1;
            }
            GUI.contentColor = cc;
            //Rails mode
            if (mode != 2)
            {
                GUI.contentColor = new Color(0.5f, 0.5f, 0.5f);
            }
            if (GUI.Button(mode2Button, "R"))
            {
                mode = 2;
            }
            GUI.contentColor = cc;
            GUI.backgroundColor = bc;

            if (true)
            {
                GUILayout.Label("WHOA, THERES STUFF IN HERE!");
            }

            if (Event.current.button > 0 && Event.current.type != EventType.Repaint && Event.current.type != EventType.Layout) //Ignore right & middle clicks
                Event.current.Use();

            GUI.DragWindow();
        }

        private void onSettingsGUI(int windowID)
        {
            //Close button
            if (GUI.Button(closeButton, ""))
            {
                settingsOpen = !settingsOpen;
            }
        }

        private void sizeWindows()
        {
            menuWindowPosition.height = 0;
            menuWindowPosition.width = 375;

            settingsWindowPosition.height = 0;
            settingsWindowPosition.width = 220;

            switch (mode)
            {
                case 0:
                    flightWindowPosition.height = 0;
                    flightWindowPosition.width = 220;
                    break;
                case 1:
                    flightWindowPosition.height = 0;
                    flightWindowPosition.width = 250;
                    break;
                case 2:
                    flightWindowPosition.height = 0;
                    flightWindowPosition.width = 375;
                    break;
            }

            settingsButton.x = flightWindowPosition.xMax - flightWindowPosition.xMin - 20; //Move the ?
        }

        //HELPER FUNCTIONS
        private Rect constrainToScreen(Rect r)
        {
            r.x = Mathf.Clamp(r.x, 0, Screen.width - r.width);
            r.y = Mathf.Clamp(r.y, 0, Screen.height - r.height);
            return r;
        }
    }
}