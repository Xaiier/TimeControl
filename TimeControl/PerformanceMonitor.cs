/*
 * Time Control
 * Created by Xaiier
 * License: MIT
 */

//TODO make this a standalone and include FPS display in it

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TimeControl
{
    [KSPAddon(KSPAddon.Startup.MainMenu, true)]
    public class PerformanceMonitor : MonoBehaviour
    {
        //Public accessible
        private static float fps; //frames per second
        private static float pps; //physics updates per second
        private static double ptr = 0f; //physics time ratio

        public static float getfps
        {
            get
            {
                return fps;
            }
        }

        public static float getpps
        {
            get
            {
                return pps;
            }
        }

        public static double getptr
        {
            get
            {
                return ptr;
            }
        }

        //Settings
        private float updateInterval = 0.5f; //half a second

        //Private
        private int frames = 0;
        private float lastInterval = 0f;
        private float last = 0f;
        private float current = 0f;
        private Queue<double> ptrRollingAvg = new Queue<double>();

        //Functions
        private void Update()
        {
            //FPS calculation
            ++frames;
            if (Time.realtimeSinceStartup > lastInterval + updateInterval)
            {
                fps = frames / (Time.realtimeSinceStartup - lastInterval);
                frames = 0;
                lastInterval = Time.realtimeSinceStartup;
            }

            //PPS calculation
            pps = Time.timeScale / Time.fixedDeltaTime;

            //PTR calculation
            current = Time.realtimeSinceStartup - last;
            last = Time.realtimeSinceStartup;
            ptrRollingAvg.Enqueue(Time.deltaTime / current);
            while (ptrRollingAvg.Count > fps)
            {
                ptrRollingAvg.Dequeue();
            }
            if (ptrRollingAvg.Count > 0)
            {
                ptr = ptrRollingAvg.Average<double>(num => Convert.ToDouble(num));
            }
        }
        private void Awake()
        {
            UnityEngine.Object.DontDestroyOnLoad(this); //Don't go away on scene changes
        }
    }
}
