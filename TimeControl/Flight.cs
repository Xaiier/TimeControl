using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace TimeControl
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class Flight : MonoBehaviour
    {
        private static Flight singleton;

        public static Flight fetch
        {
            get
            {
                return singleton;
            }
        }

        private void Awake()
        {
            singleton = this;

            print("Flight class initialized");
        }
    }
}