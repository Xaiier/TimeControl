/*
 * Version Checker
 * Created by darklight
 * License: ???
 */

using System;
using UnityEngine;

namespace TimeControl
{
    public class ToolbarSupport
    {
        //State
        private bool registered;
        private bool stockDelayRegister;
        private bool blizzyRegistered;
        private bool stockRegistered;
        private Texture2D buttonTexture;
        private ApplicationLauncherButton stockButton;
        private IButton blizzyButton;
        //Singleton
        private static ToolbarSupport singleton;

        public static ToolbarSupport fetch
        {
            get
            {
                return singleton;
            }
        }

        public void DetectSettingsChange()
        {
            if (registered)
            {
                DisableToolbar();
                EnableToolbar();
            }
        }

        public void EnableToolbar()
        {
            buttonTexture = GameDatabase.Instance.GetTexture("TimeControl/icons/active", false);
            if (registered)
            {
                return;
            }
            registered = true;
            if (Settings.fetch.toolbarType == ToolbarType.DISABLED)
            {
                //Nothing!
            }
            if (Settings.fetch.toolbarType == ToolbarType.FORCE_STOCK)
            {
                EnableStockToolbar();
            }
            if (Settings.fetch.toolbarType == ToolbarType.BLIZZY_IF_INSTALLED)
            {
                if (ToolbarManager.ToolbarAvailable)
                {
                    EnableBlizzyToolbar();
                }
                else
                {
                    EnableStockToolbar();
                }
            }
            if (Settings.fetch.toolbarType == ToolbarType.BOTH_IF_INSTALLED)
            {
                if (ToolbarManager.ToolbarAvailable)
                {
                    EnableBlizzyToolbar();
                }
                EnableStockToolbar();
            }
        }

        public void DisableToolbar()
        {
            registered = false;
            if (blizzyRegistered)
            {
                DisableBlizzyToolbar();
            }
            if (stockRegistered)
            {
                DisableStockToolbar();
            }
        }

        private void EnableBlizzyToolbar()
        {
            blizzyRegistered = true;
            blizzyButton = ToolbarManager.Instance.add("TimeControl", "GUIButton");
            blizzyButton.OnClick += OnBlizzyClick;
            blizzyButton.ToolTip = "Time Control";
            blizzyButton.TexturePath = "TimeControl/icons/inactive";
            blizzyButton.Visibility = new GameScenesVisibility(GameScenes.EDITOR, GameScenes.FLIGHT, GameScenes.SPACECENTER, GameScenes.TRACKSTATION);
        }

        private void DisableBlizzyToolbar()
        {
            blizzyRegistered = false;
            if (blizzyButton != null)
            {
                blizzyButton.Destroy();
            }
        }

        private void EnableStockToolbar()
        {
            stockRegistered = true;
            if (ApplicationLauncher.Ready)
            {
                EnableStockForRealsies();
            }
            else
            {
                stockDelayRegister = true;
                GameEvents.onGUIApplicationLauncherReady.Add(EnableStockForRealsies);
            }
        }

        private void EnableStockForRealsies()
        {
            if (stockDelayRegister)
            {
                stockDelayRegister = false;
                GameEvents.onGUIApplicationLauncherReady.Remove(EnableStockForRealsies);
            }
            stockButton = ApplicationLauncher.Instance.AddModApplication(HandleButtonClick, HandleButtonClick, DoNothing, DoNothing, DoNothing, DoNothing, ApplicationLauncher.AppScenes.ALWAYS, buttonTexture);
        }

        private void DisableStockToolbar()
        {
            stockRegistered = false;
            if (stockDelayRegister)
            {
                stockDelayRegister = false;
                GameEvents.onGUIApplicationLauncherReady.Remove(EnableStockForRealsies);
            }
            if (stockButton != null)
            {
                ApplicationLauncher.Instance.RemoveModApplication(stockButton);
            }
        }

        private void OnBlizzyClick(ClickEvent clickArgs)
        {
            HandleButtonClick();
        }

        private void HandleButtonClick()
        {
            Client.fetch.toolbarShowGUI = !Client.fetch.toolbarShowGUI;
        }

        private void DoNothing()
        {
        }

        public static void Reset()
        {
            lock (Client.eventLock)
            {
                if (singleton != null)
                {
                    singleton.DisableToolbar();
                }
                singleton = new ToolbarSupport();
            }
        }
    }

    public enum ToolbarType
    {
        DISABLED,
        FORCE_STOCK,
        BLIZZY_IF_INSTALLED,
        BOTH_IF_INSTALLED
    }
}

