/* Author: inmo-jang https://github.com/inmo-jang/unity_assets/tree/master/PointCloudStreaming
 * Modifications: Added queue compatibility by Trung-Hoa Ha
 */
using System;
using System.Collections;
using System.Collections.Generic;
using RosSharp.RosBridgeClient.MessageTypes.Sensor;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

// Attached to RosBridge object.
namespace RosSharp.RosBridgeClient
{
    [RequireComponent(typeof(RosConnector))]
    public class PointCloudSubscriber : UnitySubscriber<MessageTypes.Sensor.PointCloud2>
    {
        public int MaxQueueSize;
        
        private byte[] byteArray;
        private bool isMessageReceived = false;      
        private int messageSize;
        private int receivedMessages = 0;
        private int lastmessageSize;

        private Vector3[] pcl;
        private Color[] pcl_color;
        private Queue<Vector3> pclQueue = new Queue<Vector3>();
        private Queue<Color> pcl_colorQueue = new Queue<Color>();

        int width;
        int height;
        int row_step;
        int point_step;

        protected override void Start()
        {
            Debug.Log("PointCloudSubscriber: Max Queue Size is set to " + MaxQueueSize);
            base.Start();
        }

        // Check if new message received.
        public void Update()
        {
            if (isMessageReceived)
            {
                PointCloudRendering();
                isMessageReceived = false;
            }
        }

        // Called by subscriber to send message.
        protected override void ReceiveMessage(PointCloud2 message)
        {
            messageSize = message.data.GetLength(0);
            byteArray = new byte[messageSize];
            byteArray = message.data;
            width = (int)message.width;
            height = (int)message.height;
            row_step = (int)message.row_step;
            point_step = (int)message.point_step;

            messageSize = messageSize / point_step;
            isMessageReceived = true;
        }

        // Change points of the point cloud.
        void PointCloudRendering()
        {
            int x_posi;
            int y_posi;
            int z_posi;

            float x;
            float y;
            float z;

            int rgb_posi;
            var rgb_max = 255;

            float r;
            float g;
            float b;

            // Create every vector, convert byte type to float in this part.
            for (var n = 0; n < messageSize; n++)
            {
                x_posi = n * point_step + 0;
                y_posi = n * point_step + 4;
                z_posi = n * point_step + 8;

                x = BitConverter.ToSingle(byteArray, x_posi);
                y = BitConverter.ToSingle(byteArray, y_posi);
                z = BitConverter.ToSingle(byteArray, z_posi);

                rgb_posi = n * point_step + 16;

                b = byteArray[rgb_posi + 0];
                g = byteArray[rgb_posi + 1];
                r = byteArray[rgb_posi + 2];

                r /= rgb_max;
                g /= rgb_max;
                b /= rgb_max;
                
                if (receivedMessages > MaxQueueSize){
                  pclQueue.Dequeue();
                  pcl_colorQueue.Dequeue();
                }
                pclQueue.Enqueue(new Vector3(x, z, y));
                pcl_colorQueue.Enqueue(new Color(r, g, b));
            }
            receivedMessages += 1;
        }

        public Vector3[] GetPCL()
        {
            return pclQueue.ToArray();
        }

        public Color[] GetPCLColor()
        {
            return pcl_colorQueue.ToArray();
        }
    }
}
