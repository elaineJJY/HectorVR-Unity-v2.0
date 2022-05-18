using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;


public class RemoteController : MonoBehaviour
{
    public Transform modelJoystick;
    public float joystickRot = 20;

    public Transform modelTrigger;
    public float triggerRot = 20;

    public Transform MenuButton;

    //ui stuff
    public Canvas ui_Canvas;
    public Image ui_rpm;
    public Image ui_speed;
    public RectTransform ui_steer;

    public float ui_steerangle;

    public Vector2 ui_fillAngles;


    SteamVR_Action_Vector2 touchpad = SteamVR_Input.GetVector2Action("read_touchpad"); 
    SteamVR_Action_Single squezz = SteamVR_Input.GetSingleAction("Squeeze");
    SteamVR_Action_Boolean modeMenu = SteamVR_Input.GetBooleanAction("modemenu");
    public float angle;
    public float velocity;
    
    private float usteer;
    private Interactable interactable;

    private Quaternion trigSRot;

    private Quaternion joySRot;

    private Coroutine resettingRoutine;

    private Vector3 initialScale;

    private Remote remote;
    private void Start()
    {
        joySRot = modelJoystick.localRotation;
        trigSRot = modelTrigger.localRotation;
        
        remote = GameObject.Find("Modes/Remote").GetComponent<Remote>();
        remote.remoteController = this;

        interactable = GetComponent<Interactable>();
    }

    private void Update()
    {
        Vector2 steer = Vector2.zero;
        bool b_modemenu = false;

        if (interactable.attachedToHand)
        {
            SteamVR_Input_Sources hand = interactable.attachedToHand.handType;

            // caculate the angle and the velocity
            velocity = squezz.GetAxis(hand);
            angle = touchpad.GetAxis(hand).x;
            steer = touchpad.GetAxis(hand);
            b_modemenu = modeMenu.GetStateDown(hand);
            //interactable.attachedToHand.TriggerHapticPulse(0.1f, velocity*50f, velocity);
        }
        else{
            velocity = 0;
            angle = 0;
        }

        if (ui_Canvas != null)
        {
            ui_Canvas.gameObject.SetActive(interactable.attachedToHand);

            usteer = Mathf.Lerp(usteer, steer.x, Time.deltaTime * 9);
            ui_steer.localEulerAngles = Vector3.forward * usteer * -ui_steerangle;
            ui_rpm.fillAmount = Mathf.Lerp(ui_rpm.fillAmount, Mathf.Lerp(ui_fillAngles.x, ui_fillAngles.y, velocity), Time.deltaTime * 4);
            float speedLim = 1;
            ui_speed.fillAmount = Mathf.Lerp(ui_fillAngles.x, ui_fillAngles.y, 1 - (Mathf.Exp(- velocity/ speedLim)));

        }

        modelJoystick.localRotation = joySRot;
        modelJoystick.Rotate(steer.y * -joystickRot, steer.x * -joystickRot, 0, Space.Self);
        

        modelTrigger.localRotation = trigSRot;
        modelTrigger.Rotate(velocity * -triggerRot, 0, 0, Space.Self);
        MenuButton.localScale = new Vector3(1, 1, b_modemenu ? 0.4f : 1.0f);
        
    
    }

    private void OnDestroy() {
        remote.remoteController = null;
    }
}
