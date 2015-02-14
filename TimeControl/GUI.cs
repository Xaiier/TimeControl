using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace TimeControl
{
    [KSPAddon(KSPAddon.Startup.MainMenu, true)]
    public class GUI : MonoBehaviour
    {
        private void Awake()
        {
            UnityEngine.Object.DontDestroyOnLoad(this);

            print("GUI class initialized");
        }
    }
}