using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

public class UIDrag: MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{   

    public GameObject RightHand;
    public bool autoLockRobot = true;
    Transform parent;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startDrag(){
        parent = this.gameObject.transform.parent;
        this.gameObject.transform.parent = RightHand.transform;
    }
    public void stopDrag(){
        this.gameObject.transform.parent = parent;
    }

    public void OnPointerDown(PointerEventData eventData)
    {   
        if(!InteractionManagement.Instance.Robot_Locked && autoLockRobot){
            InteractionManagement.Instance.LockRobot(true);     // automatically lock the robot
        }
        startDrag();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        stopDrag();
    }
}
