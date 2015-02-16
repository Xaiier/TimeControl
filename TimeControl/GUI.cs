using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace TimeControl
{
    [KSPAddon(KSPAddon.Startup.MainMenu, true)]
    public class UI : MonoBehaviour
    {
        private bool visible;
        private bool settingsOpen;

        private Rect menuWindowPosition;
        private Rect flightWindowPosition;
        private Rect settingsWindowPosition;

        private void Awake()
        {
            UnityEngine.Object.DontDestroyOnLoad(this);

            print("GUI class initialized");
        }

        private void OnGUI()
        {
            GUI.skin = HighLogic.Skin; //use yellow text for the FPS display

            //if (Settings.showFPS && fpsVisible && HighLogic.LoadedSceneIsFlight)
            //{
            //    Settings.fpsPosition.x = parseSTOI(Settings.fpsX);
            //    Settings.fpsPosition.y = parseSTOI(Settings.fpsY);
            //    GUI.Label(Settings.fpsPosition, Mathf.Floor(PerformanceMonitor.fps).ToString());
            //}

            GUI.skin = null; //use Unity default GUI

            if (visible)
            {
                if (HighLogic.LoadedScene == GameScenes.TRACKSTATION || HighLogic.LoadedScene == GameScenes.SPACECENTER)
                {
                    menuWindowPosition = constrainToScreen(GUILayout.Window("Time Control".GetHashCode(), menuWindowPosition, onMenuGUI, "Time Control"));
                }

                if (HighLogic.LoadedSceneIsFlight)
                {
                    flightWindowPosition = constrainToScreen(GUILayout.Window("Time Control".GetHashCode() + 1, flightWindowPosition, onFlightGUI, "Time Control"));

                    if (settingsOpen)
                    {
                        settingsWindowPosition = constrainToScreen(GUILayout.Window("Time Control".GetHashCode() + 2, settingsWindowPosition, onSettingsGUI, "Time Control Settings"));
                    }
                }
            }

            GUI.skin = HighLogic.Skin;//use KSP GUI - prevents bugs with other mods that might not bother to set skin
        }

        private void onMenuGUI(int windowID)
        {

        }

        private void onFlightGUI(int windowID)
        {

        }

        private void onSettingsGUI(int windowID)
        {

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