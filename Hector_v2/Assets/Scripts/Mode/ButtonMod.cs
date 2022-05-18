using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;

public class ButtonMod : CustomInteractible
{
    public float distanseToPress; //button press reach distance
    [Range(.1f,1f)]
    public float DistanceMultiply=.1f; //button sensetivity slowdown
    public Transform MoveObject; //movable button object
    public UnityEvent ButtonDown, ButtonUp, ButtonUpdate; // events

    private Color32 oldColor;
    float StartButtonPosition; //tech variable, assigned at start of pressed button
    bool press; //button check, to ButtonDown call 1 time
    void Awake()
    {
        StartButtonPosition = MoveObject.localPosition.z;
        oldColor = GetComponentInChildren<MeshRenderer>().material.color;
    }
    

    void GrabStart(CustomHand hand)
    {
        SetInteractibleVariable(hand);
        hand.SkeletonUpdate();
        hand.grabType = CustomHand.GrabType.Select;
		Grab.Invoke ();
    }

    void GrabUpdate(CustomHand hand)
    {
        if ((rightHand || leftHand) && GetMyGrabPoserTransform(hand))
        {
            hand.SkeletonUpdate();
            GetComponentInChildren<MeshRenderer>().material.color = Color.grey;
            float tempDistance = Mathf.Clamp(StartButtonPosition-(StartButtonPosition-transform.InverseTransformPoint(hand.PivotPoser.position).z)*DistanceMultiply, StartButtonPosition, distanseToPress);
            if (tempDistance >= distanseToPress)
            {   
                GetComponentInChildren<MeshRenderer>().material.color = Color.red;
                if (!press)
                {
                    ButtonDown.Invoke();
                    SteamVR_Action_Vibration vibration = SteamVR_Input.GetVibrationAction("Haptic");
                    if(rightHand){
                        vibration.Execute(0f, 0.1f, 50f, 1f, SteamVR_Input_Sources.RightHand);
                    }
                    else{
                        vibration.Execute(0f, 0.1f, 50f, 1f, SteamVR_Input_Sources.LeftHand);
                    }
                }
                press = true;
                ButtonUpdate.Invoke();
            }
            else
            {
                if (press)
                {
                    ButtonUp.Invoke();
                }
                press = false;
            }
            MoveObject.localPosition = new Vector3(0, 0, tempDistance);
            MoveObject.rotation = Quaternion.LookRotation(GetMyGrabPoserTransform(hand).forward, hand.PivotPoser.up);
            hand.GrabUpdateCustom();
        }
    }

    void GrabEnd(CustomHand hand)
    {
        //if ((rightHand || leftHand) && GetMyGrabPoserTransform(hand))
        //{
            MoveObject.localPosition = new Vector3(0, 0, StartButtonPosition);
            DettachHand(hand);

            GetComponentInChildren<MeshRenderer>().material.color = oldColor;
        //}
		ReleaseHand.Invoke ();
    }
}