/* 
 * author: Jana-Sophie Schönfeld
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attached to GameObject RosConnector in the hierachy.

namespace RosSharp.RosBridgeClient
{
    [RequireComponent(typeof(RosConnector))]
    public class CameraServiceSubscriber: UnityServiceSubscriber<MessageTypes.Rosapi.TopicsForTypeRequest, MessageTypes.Rosapi.TopicsForTypeResponse> 
    {
        public string[] topics;


        // Start is called before the first frame update.
        // Calls rosapi service.
        void Start() 
        {
            base.Start("/rosapi/topics_for_type", new MessageTypes.Rosapi.TopicsForTypeRequest("sensor_msgs/CompressedImage"));
        }

        // Gets answer from service and saves all topics for compressed images.
        protected override void ServiceResponseHandler(MessageTypes.Rosapi.TopicsForTypeResponse topicsMessage)
        {
            topics = topicsMessage.topics;
            Debug.Log("Got the topics");
        }
    }
}