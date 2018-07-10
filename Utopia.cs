using System;
using ICities;
using UnityEngine;
using ColossalFramework;
using ColossalFramework.Plugins;
using ColossalFramework.UI;
using System.Collections;
using ColossalFramework.Globalization;
using ColossalFramework.Math;
using ColossalFramework.Threading;

namespace Utopia
{
    public class Utopia : IUserMod
    {
        public string Name {
            get { return "Utopia"; }
        }

        public string Description {
            get { return "Build a city in the face of violent opposition..."; }
        }
        
    }

    public class Loader : LoadingExtensionBase
    {
        public static GameObject gc;
        public static UIButton b_build_def = null;

        public override void OnLevelLoaded(LoadMode mode)
        {
            var uiView = UIView.GetAView();
            //(40.19925f, 526.7176f, -389.8089f)
            //Add button
            b_build_def = makeButton(uiView, "Build Defenses");
            b_build_def.transformPosition = new Vector3(-1.2f, -0.87f);
            b_build_def.eventClick += toggleBuildDef;
            DebugOutputPanel.AddMessage(PluginManager.MessageType.Message, "Mode name:" + mode.Name());
            
            gc = new GameObject("Utopia Object");
            gc.AddComponent<UtopiaController>();
           
        }
        public override void OnCreated(ILoading loading)
        {
            base.OnCreated(loading);

        }
        private void toggleBuildDef(UIComponent component, UIMouseEventParameter eventParam)
        {
            DebugOutputPanel.AddMessage(PluginManager.MessageType.Message, "Pressed");
            gc.GetComponent<UtopiaController>().LaunchMeteor();

        }
        private UIButton makeButton(UIView uiView, string t)
        {
            UIButton b = (UIButton)uiView.AddUIComponent(typeof(UIButton));

            b.text = t;
            b.width = 140;
            b.height = 30;
            b.normalBgSprite = "ButtonMenu";
            b.disabledBgSprite = "ButtonMenuDisabled";
            b.hoveredBgSprite = "ButtonMenuHovered";
            b.focusedBgSprite = "ButtonMenuFocused";
            b.pressedBgSprite = "ButtonMenuPressed";
            b.textColor = new Color32(255, 255, 255, 255);
            b.disabledTextColor = new Color32(7, 7, 7, 255);
            b.hoveredTextColor = new Color32(7, 132, 255, 255);
            b.focusedTextColor = new Color32(255, 255, 255, 255);
            b.pressedTextColor = new Color32(30, 30, 44, 255);
            b.playAudioEvents = true;
            return b;
        }
    }    
}