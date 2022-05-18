/* author: Jana-Sophie Schönfeld
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    [RequireComponent(typeof(RosConnector))]
    public abstract class UnityServiceSubscriber<Tout, Tin> : MonoBehaviour where Tout : Message where Tin : Message
    {

        protected void Start(string ServiceName, Tout serviceArguments)
        {
            GetComponent<RosConnector>().RosSocket.CallService<Tout, Tin>(ServiceName, ServiceResponseHandler, serviceArguments);
        }

        protected abstract void ServiceResponseHandler(Tin message);
    }
}