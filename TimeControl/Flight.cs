using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace TimeControl
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class Flight : MonoBehaviour
    {
        private void Awake()
        {
            print("Flight class initialized");
        }
    }
}
