/*
© Siemens AG, 2017-2019
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

// Modified by Lydia Ebbinghaus and Yannic Seidler.

using System.Collections.Generic;
using UnityEngine;
using System;

namespace RosSharp.RosBridgeClient
{
    public class JointStateSubscriberMod : UnitySubscriber<MessageTypes.Sensor.JointState>
    {
        public List<string> JointNames;
        public List<JointStateWriterMod> JointStateWriters;
        private robotType type;
        bool infoLoaded;

        // Modified. Used for initializing lists.
        public void JointStateSubscriberInit()
        {
            var loader = GameObject.Find("ObjectLoader").GetComponent<Loader>();
            this.Topic = loader.topic;
            JointNames = loader.jointNames;
            JointStateWriters = loader.jointStateWriters;

            if(!infoLoaded)
            {
                try
                {
                    type = this.GetComponent<RobotInformation>().robotType;
                    infoLoaded=true;
                }
                catch(NullReferenceException)
                {
                    Debug.Log("Robot not loaded yet.");
                }
            }
            
        }

        // Added so is subscribing.
        protected override void Start()
        {
            //modified
            JointStateSubscriberInit();
            
			base.Start();
            Debug.Log("Starting to update Joints");
		}

        // Modified du to incorrect representation.
        // Joints are not updated correctly. Still work to do.
        protected override void ReceiveMessage(MessageTypes.Sensor.JointState message)
        {
            int index;
            //Debug.Log("joint Name : " + message.name[0]);
            switch (type)
            {
                case robotType.drz_telemax:
                    for (int i = 0; i < message.name.Length; i++)
                    {
                        index = JointNames.IndexOf(message.name[i]);
                        if (index != -1)
                        {
                            // For some reason backflippers are represented incorrectly rotatet, therefore manually modified until theres a better solution.
                            // Attention!: Not a generic solution.
                            // Undo: delete if-else clause, just leave "JointStateWriters[index].Write((float) message.position[i])";.
                            if ((i==6)||(i==7))
                            {
                                JointStateWriters[index].Write(-(float) message.position[i],type);
                            }
                            else
                            {
                                JointStateWriters[index].Write((float) message.position[i],type);
                            }
                        }
                    }
                return;

                case robotType.asterix_ugv:
                    Debug.Log("joint Name : " + message.name[0]);
                    for (int i = 0; i < message.name.Length; i++)
                    {
                        index = JointNames.IndexOf(message.name[i]);
                        JointStateWriters[index].Write((float) message.position[i],type);
                        
                    }
                return;
            }

            for (int i = 0; i < message.name.Length; i++)
            {
                index = JointNames.IndexOf(message.name[i]);
                JointStateWriters[index].Write((float) message.position[i],type);
                
            }
        }
    }
}

