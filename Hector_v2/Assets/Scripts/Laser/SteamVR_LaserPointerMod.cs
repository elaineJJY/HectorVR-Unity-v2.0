//======= Copyright (c) Valve Corporation, All rights reserved. ===============
// Modified by Trung-Hoa Ha, Lydia Ebbinghaus and Jana-Sophie Schönfeld.
// Enable code that is commented out to use the laser for scrolling.
// Experimental Idea: If Laser is on the bottom of the menu: Scroll Down. If on top of the menu Scroll Up.
// Improvement needed.

using UnityEngine;
using Valve.VR;
public class SteamVR_LaserPointerMod : MonoBehaviour
{
    public SteamVR_Behaviour_Pose pose;

    //public SteamVR_Action_Boolean interactWithUI = SteamVR_Input.__actions_default_in_InteractUI;
    public SteamVR_Action_Boolean interactWithUI = SteamVR_Input.GetBooleanAction("InteractUI");
    //public SteamVR_Action_Boolean touchup = SteamVR_Input.GetBooleanAction("TouchUp");
    //public SteamVR_Action_Boolean touchdown = SteamVR_Input.GetBooleanAction("TouchDown");

    public bool active = true;
    public Color color;
    public float thickness = 0.002f;
    public Color clickColor = Color.green;
    public GameObject holder;
    public GameObject pointer;
    bool isActive = false;
    public bool addRigidBody = false;
    public Transform reference;
    public event PointerEventHandler PointerIn;
    public event PointerEventHandler PointerOut;
    public event PointerEventHandler PointerClick;

    // Modified.
    /*public event PointerEventHandler PointerUp;
    public event PointerEventHandler PointerDown;*/

    Transform previousContact = null;


    private void Start()
    {
        if (pose == null)
            pose = this.GetComponent<SteamVR_Behaviour_Pose>();
        if (pose == null)
            Debug.LogError("No SteamVR_Behaviour_Pose component found on this object", this);

        if (interactWithUI == null)
            Debug.LogError("No ui interaction action has been set on this component.", this);


        holder = new GameObject();
        holder.transform.parent = this.transform;
        holder.transform.localPosition = Vector3.zero;
        holder.transform.localRotation = Quaternion.identity;

        pointer = GameObject.CreatePrimitive(PrimitiveType.Cube);
        pointer.transform.parent = holder.transform;
        pointer.transform.localScale = new Vector3(thickness, thickness, 100f);
        pointer.transform.localPosition = new Vector3(0f, 0f, 50f);
        pointer.transform.localRotation = Quaternion.identity;
        BoxCollider collider = pointer.GetComponent<BoxCollider>();

        if (addRigidBody)
        {
            if (collider)
            {
                collider.isTrigger = true;
            }
            Rigidbody rigidBody = pointer.AddComponent<Rigidbody>();
            rigidBody.isKinematic = true;
        }
        else
        {
            if (collider)
            {
                Object.Destroy(collider);
            }
        }
        Material newMaterial = new Material(Shader.Find("Unlit/Color"));
        newMaterial.SetColor("_Color", color);
        pointer.GetComponent<MeshRenderer>().material = newMaterial;
    }

    public virtual void OnPointerIn(PointerEventArgs e)
    {
        if (PointerIn != null)
            PointerIn(this, e);
    }

    public virtual void OnPointerClick(PointerEventArgs e)
    {
        if (PointerClick != null)
            PointerClick(this, e);
    }

    // Modified.
    /*public virtual void OnPointerDownClick(PointerEventArgs e)
    {
        if (PointerDown != null)
            PointerDown(this, e);
    }
    public virtual void OnPointerUpClick(PointerEventArgs e)
    {
        if (PointerUp != null)
            PointerUp(this, e);
    }*/

    public virtual void OnPointerOut(PointerEventArgs e)
    {
        if (PointerOut != null)
            PointerOut(this, e);
    }


    private void Update()
    {
        // Modified.
        if(active != isActive)
        {
            isActive = active;
            pointer?.SetActive(isActive);
        }
        if(!isActive) return;

        float dist = 100f;

        Ray raycast = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        bool bHit = Physics.Raycast(raycast, out hit);

        // Modified.
        /*if(bHit)
        {
            bool scrollUp = hit.point.y > hit.transform.position.y +hit.transform.localScale.y-0.5f
                        && (hit.point.y <= hit.transform.position.y +hit.transform.localScale.y);
            bool scrollDown = (hit.point.y < (hit.transform.position.y + 0.5f)) 
                        && (hit.point.y >= hit.transform.position.y);

            if(bHit)
            {

                if(scrollUp)
                {
                    Debug.Log("Scrolling up." + hit.transform.name);
                    PointerEventArgs argsUp = new PointerEventArgs();
                    argsUp.fromInputSource = pose.inputSource;
                    argsUp.distance = hit.distance;
                    argsUp.flags = 0;
                    argsUp.target = hit.transform;
                    OnPointerUpClick(argsUp);
                }
                else if(scrollDown)
                {
                    Debug.Log("Scrolling down." + hit.transform.name);
                    PointerEventArgs argsDown = new PointerEventArgs();
                    argsDown.fromInputSource = pose.inputSource;
                    argsDown.distance = hit.distance;
                    argsDown.flags = 0;
                    argsDown.target = hit.transform;
                    OnPointerDownClick(argsDown);
                }
            }
        } 
        */

        if (previousContact && previousContact != hit.transform)
        {
            PointerEventArgs args = new PointerEventArgs();
            args.fromInputSource = pose.inputSource;
            args.distance = 0f;
            args.flags = 0;
            args.target = previousContact;
            OnPointerOut(args);
            previousContact = null;
        }
        if (bHit && previousContact != hit.transform)
        {
            PointerEventArgs argsIn = new PointerEventArgs();
            argsIn.fromInputSource = pose.inputSource;
            argsIn.distance = hit.distance;
            argsIn.flags = 0;
            argsIn.target = hit.transform;
            OnPointerIn(argsIn);
            previousContact = hit.transform;
        }
    
        if (!bHit)
        {
            previousContact = null;
        }
        if (bHit && hit.distance < 100f)
        {
            dist = hit.distance;
        }

        if (bHit && interactWithUI.GetStateUp(pose.inputSource))
        {
            PointerEventArgs argsClick = new PointerEventArgs();
            argsClick.fromInputSource = pose.inputSource;
            argsClick.distance = hit.distance;
            argsClick.flags = 0;
            argsClick.target = hit.transform;
            OnPointerClick(argsClick);
        }

        // Modified.
        /* 
        if (bHit && touchdown.GetState(pose.inputSource))
        {
            PointerEventArgs argsClick = new PointerEventArgs();
            argsClick.fromInputSource = pose.inputSource;
            argsClick.distance = hit.distance;
            argsClick.flags = 0;
            argsClick.target = hit.transform;
            OnPointerDownClick(argsClick);
        }
        if (bHit && touchup.GetState(pose.inputSource))
        {
            PointerEventArgs argsClick = new PointerEventArgs();
            argsClick.fromInputSource = pose.inputSource;
            argsClick.distance = hit.distance;
            argsClick.flags = 0;
            argsClick.target = hit.transform;
            OnPointerUpClick(argsClick);
        }*/

        if (interactWithUI != null && interactWithUI.GetState(pose.inputSource))
        {
            pointer.transform.localScale = new Vector3(thickness * 5f, thickness * 5f, dist);
            pointer.GetComponent<MeshRenderer>().material.color = clickColor;
        }
        else
        {
            pointer.transform.localScale = new Vector3(thickness, thickness, dist);
            pointer.GetComponent<MeshRenderer>().material.color = color;
        }
        pointer.transform.localPosition = new Vector3(0f, 0f, dist / 2f);
    }
}

public struct PointerEventArgs
{
    public SteamVR_Input_Sources fromInputSource;
    public uint flags;
    public float distance;
    public Transform target;
}

public delegate void PointerEventHandler(object sender, PointerEventArgs e);
