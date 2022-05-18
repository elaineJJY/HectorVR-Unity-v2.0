using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class SpecialTwistPublisher : UnityPublisher<MessageTypes.Geometry.Twist>
    {
        public Transform PublishedTransform;

        private MessageTypes.Geometry.Twist message;
        private float previousRealTime;        
        private Vector3 previousPosition = Vector3.zero;
        private Quaternion previousRotation = Quaternion.identity;

        //Testing
        public Vector3 LINEAR;
        public Vector3 ANGULAR;


        protected override void Start()
        {
            base.Start();
            InitializeMessage();
        }

        private void FixedUpdate()
        {
            UpdateMessage();
        }

        private void InitializeMessage()
        {
            message = new MessageTypes.Geometry.Twist();
            message.linear = new MessageTypes.Geometry.Vector3();
            message.angular = new MessageTypes.Geometry.Vector3();
        }
        private void UpdateMessage()
        {
            Vector3 linearVelocity = (PublishedTransform.position - previousPosition) / Time.fixedDeltaTime;
            Vector3 angularVelocity = (PublishedTransform.rotation.eulerAngles - previousRotation.eulerAngles) / Time.fixedDeltaTime;

            message.linear = GetGeometryVector3(linearVelocity.Unity2Ros());
            message.angular = GetGeometryVector3(-angularVelocity.Unity2Ros());

            LINEAR=linearVelocity.Unity2Ros();
            ANGULAR= (PublishedTransform.position - previousPosition) / Time.fixedDeltaTime;
            
            previousPosition = PublishedTransform.position;
            previousRotation = PublishedTransform.rotation;

            //message.linear = GetGeometryVector3(new Vector3(0,0,1).Unity2Ros());
            //message.angular = GetGeometryVector3(new Vector3(0,0,0).Unity2Ros());

            
            

            Publish(message);
        }

        private static MessageTypes.Geometry.Vector3 GetGeometryVector3(Vector3 vector3)
        {
            MessageTypes.Geometry.Vector3 geometryVector3 = new MessageTypes.Geometry.Vector3();
            geometryVector3.x = vector3.x;
            geometryVector3.y = vector3.y;
            geometryVector3.z = vector3.z;
            return geometryVector3;
        }
    }
}