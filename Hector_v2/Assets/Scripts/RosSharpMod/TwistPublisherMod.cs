/*
© Siemens AG, 2017-2018
Author: Dr. Martin Bischoff (martin.bischoff@siemens.com)

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at
<http://www.apache.org/licenses/LICENSE-2.0>.
Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

// Modified by Lydia Ebbinghaus

using UnityEngine;
using System;

// Publishes control signals to Ros.
// Attached to Rosbridge object.
namespace RosSharp.RosBridgeClient
{
    public class TwistPublisherMod : UnityPublisher<MessageTypes.Geometry.Twist>
    {
        private MessageTypes.Geometry.Twist message;
        private RoboterControl robotControl;
        public Vector3 linearVelocity;
        public Vector3 angularVelocity;
        bool roboterLoaded;

        // Start is called before first frame update.
        // Modified.
        protected override void Start()
        {
            base.Start();
            InitializeMessage();
        }

        // Changed from fixedUpdate to Update, called once per frame.
        void Update() 
        {
            UpdateMessage();
        }

        // Initialize message.
        private void InitializeMessage()
        {
            message = new MessageTypes.Geometry.Twist();
            message.linear = new MessageTypes.Geometry.Vector3();
            message.angular = new MessageTypes.Geometry.Vector3();
        }

        // Get control signal from RobotControl and send to ROS.
        // Modified.
        private void UpdateMessage()
        {
            try
            {
                if(!robotControl)
                {
                    robotControl = GameObject.FindWithTag("robot").GetComponent<RoboterControl>();
                }
                if(robotControl!=null) roboterLoaded=true;
            }
            catch(NullReferenceException)
            {
                Debug.Log("TwistPublisherMod: No Gameobject with tag robot. Robot not loaded yet.");
            }

            // Use this for old Gamepad input.
            //linearVelocity = new Vector3(-robotControl..getControlSignalXBox().lin,0,0);
            //angularVelocity = new Vector3(0,robotControl.getControlSignalXBox().ang,0);

            if (roboterLoaded) {
                linearVelocity = new Vector3(robotControl.getControlSignal().y, 0, 0);
                angularVelocity = new Vector3(0, robotControl.getControlSignal().x, 0);

                // Other interpretation of telemax cooridinates , therefore modified.
                message.linear = GetGeometryVector3(linearVelocity);
                message.angular = GetGeometryVector3(-angularVelocity.Unity2Ros());

                Publish(message);

            }

            
        }

        // Modified.
        private static MessageTypes.Geometry.Vector3 GetGeometryVector3(Vector3 vector3)
        {
            var geometryVector3 = new MessageTypes.Geometry.Vector3(vector3.x,vector3.y,vector3.z);
            return geometryVector3;
        }
    }
}
