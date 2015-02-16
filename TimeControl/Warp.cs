using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace TimeControl
{
    [KSPAddon(KSPAddon.Startup.SpaceCentre, false)]
    class Warp : MonoBehaviour
    {
        private static Warp singleton;

        public static Warp fetch
        {
            get
            {
                return singleton;
            }
        }

        private void Awake()
        {
            singleton = this;

            print("Warp class initialized");
        }
    }

    [KSPAddon(KSPAddon.Startup.TrackingStation, false)]
    [RequireComponent(typeof(Warp))]
    class WarpTrackingStation : MonoBehaviour { }

    [KSPAddon(KSPAddon.Startup.Flight, false)]
    [RequireComponent(typeof(Warp))]
    class WarpFlight : MonoBehaviour { }
}