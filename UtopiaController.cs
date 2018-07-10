using ColossalFramework;
using ColossalFramework.Plugins;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Utopia
{
    class UtopiaController : MonoBehaviour
    {

        private CameraController m_cameraController;

        public void Awake()
        {
            GameObject gameObject = GameObject.FindGameObjectWithTag("MainCamera");
            if (gameObject != null)
            {
                m_cameraController = gameObject.GetComponent<CameraController>();
                DebugOutputPanel.AddMessage(PluginManager.MessageType.Message, "Main camera found! " + gameObject.name);
            }
            else
            {
                DebugOutputPanel.AddMessage(PluginManager.MessageType.Message, "No main camera!");
            }

            DebugOutputPanel.AddMessage(PluginManager.MessageType.Message, "Utopia Controller Loaded");
        }
        public void LaunchMeteor()
        {

            DebugOutputPanel.AddMessage(PluginManager.MessageType.Message, "Launch Meteor 1");
            if ((UnityEngine.Object)m_cameraController != (UnityEngine.Object)null)
            {
                DebugOutputPanel.AddMessage(PluginManager.MessageType.Message, "Launch Meteor 2");
                int num12 = PrefabCollection<DisasterInfo>.LoadedCount();
                if (num12 != 0)
                {
                    DebugOutputPanel.AddMessage(PluginManager.MessageType.Message, "Launch Meteor 3");
                    //  for (int i = 0; i < num12; i++)
                    // {
                    DisasterInfo loaded = PrefabCollection<DisasterInfo>.GetLoaded((uint)3);
                    if ((UnityEngine.Object)loaded != (UnityEngine.Object)null && loaded.m_disasterAI.CanSelfTrigger())
                    {
                        DebugOutputPanel.AddMessage(PluginManager.MessageType.Message, "Name "+loaded.gameObject.name);
                        Singleton<SimulationManager>.instance.AddAction(StartDisaster(loaded, m_cameraController.m_currentPosition, (0f - m_cameraController.m_currentAngle.x) * 0.0174532924f));
                    }
                   // }
                }
            }
        }
        private IEnumerator StartDisaster(DisasterInfo info, Vector3 target, float angle)
        {
            DisasterManager disasterManager = Singleton<DisasterManager>.instance;
            if (disasterManager.CreateDisaster(out ushort disaster, info))
            {
                disasterManager.m_disasters.m_buffer[disaster].m_targetPosition = target;
                disasterManager.m_disasters.m_buffer[disaster].m_angle = angle;
                disasterManager.m_disasters.m_buffer[disaster].m_intensity = (byte)Singleton<SimulationManager>.instance.m_randomizer.Int32(10, 100);
                disasterManager.m_disasters.m_buffer[disaster].m_flags |= DisasterData.Flags.SelfTrigger;
                info.m_disasterAI.StartNow(disaster, ref disasterManager.m_disasters.m_buffer[disaster]);
            }
            yield return (object)0;
            /*Error: Unable to find new state assignment for yield return*/
            ;
        }


    }
}
