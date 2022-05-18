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

// Added allocation free alternatives
// UoK , 2019, Odysseas Doumas (od79@kent.ac.uk / odydoum@gmail.com)

using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class PoseStampedPublisher : UnityPublisher<MessageTypes.Geometry.PoseStamped>
    {
        private bool wait; //lydia
        private TwistSubscriber twist_sub; //Lydia
        public Transform PublishedTransform;
        public string FrameId = "Unity";

        private MessageTypes.Geometry.PoseStamped message;

        private Vector3 modifiedPosition;
    

        protected override void Start()
        {
            base.Start();
            InitializeMessage();

            //Lydia
            wait=false;
            twist_sub = GameObject.Find("RosConnector").GetComponent<TwistSubscriber>();
        }

        private void FixedUpdate()
        {
            UpdateMessage();
        }

        private void InitializeMessage()
        {
            message = new MessageTypes.Geometry.PoseStamped
            {
                header = new MessageTypes.Std.Header()
                {
                    frame_id = FrameId
                }
            };
        }

        private void UpdateMessage()
        {
            //if(twist_sub.MessageReceived())
            //{
            //    Debug.Log("Received");
            //    wait =false;
            //}


            message.header.Update();
            GetGeometryPoint(PublishedTransform.position.Unity2Ros(), message.pose.position);
            GetGeometryQuaternion(PublishedTransform.rotation.Unity2Ros(), message.pose.orientation);

             //(wait==false){
                Publish(message); // is the simulated robot already at position?
                wait = true;
                //Debug.Log("Waiting for 5 seconds");
                //System.Threading.Thread.Sleep(5000);
                //Debug.Log("finished waiting");
            //}else{
            //    Debug.Log("waiting for robot to move");
            //}
        }

        private static void GetGeometryPoint(Vector3 position, MessageTypes.Geometry.Point geometryPoint)
        {
            geometryPoint.x = position.x; //y in unity is z for robot
            geometryPoint.y = position.y; //x in unity is y for robot
            geometryPoint.z = position.z; //z in unity is x for robot
        }

        private static void GetGeometryQuaternion(Quaternion quaternion, MessageTypes.Geometry.Quaternion geometryQuaternion)
        {
            geometryQuaternion.x = quaternion.x;
            geometryQuaternion.y = quaternion.y;
            geometryQuaternion.z = quaternion.z;
            geometryQuaternion.w = quaternion.w;
        }

        public void setWait(bool wait){
            this.wait= wait;
        }
        public bool getWait(){
            return wait;
        }

    }
}
