/*
 * Handle the Event in Defualt Mode
 *
 * Author: Jingyi Jia
 * 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem.Sample;
using UnityEngine.UI;


public class Handle : MonoBehaviour,IMode
{
    InteractionManagement interactionManagement;
    public Player_Control Player_Control;
    public GameObject[] ModeMenu;
    public Vector2 signal;
    public Vector2 Signal => signal;
    SteamVR_Action_Vibration vibration = SteamVR_Input.GetVibrationAction("Haptic");

    private void OnEnable() {
        
        // The player can be raised or lowered (the aerial perspective is enabled)
        Player_Control.upDownEnabled = true;
        
        interactionManagement = InteractionManagement.Instance;
        interactionManagement.SetPlayerText( "Control Mode : " + gameObject.name, 7, false);
        interactionManagement.SetMenu(false);

        // Add  SteamVR Actions Listener
        SteamVR_Actions.default_Menu.AddOnStateDownListener(MenuActionHandler,SteamVR_Input_Sources.Any);
        SteamVR_Actions.default_Lock.AddOnStateDownListener(Locked_onStateDown,SteamVR_Input_Sources.Any);
        SteamVR_Actions.default_ModeMenu.AddOnStateDownListener(ModeMenuHandler,SteamVR_Input_Sources.Any);
    }

    // Start is called before the first frame update
    void Start()
    {
        
        
        
    }


    // Update is called once per frame
    void Update()
    {   
        if(interactionManagement.Robot_Locked){
            signal =  new Vector2(0,0);
        }
        else{
            UpdateSignal();
        }
       
    }

    public void UpdateSignal()
    {   
        SteamVR_Action_Vector2 touchpad = SteamVR_Input.GetVector2Action("read_touchpad"); 
        Vector2 touchValue = touchpad.GetAxis(SteamVR_Input_Sources.RightHand);
        SteamVR_Action_Single squezz = SteamVR_Input.GetSingleAction("Squeeze");
        float angle = touchValue.x;
        float velocity = squezz.GetAxis(SteamVR_Input_Sources.RightHand)*0.3f;
        //vibration.Execute(0f, 0.1f, velocity*50f, velocity, SteamVR_Input_Sources.RightHand);
        signal =  new Vector2(angle,velocity);    
    }


    // open or close the menu
    private void MenuActionHandler(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        interactionManagement.SetMenu();
        vibration.Execute(0f, 0.1f, 50f, 1f, SteamVR_Input_Sources.Any);
    }


    // Handle Robot Locking
    private void Locked_onStateDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (!interactionManagement.Menu_Opened)
        {
            interactionManagement.LockRobot();
            vibration.Execute(0f, 0.1f, 50f, 1f, SteamVR_Input_Sources.Any);
        }
        else {
            interactionManagement.SetPlayerText("Please close the menu first",5,false);
        }
    }

    // open or close the modemenu: map, monitor....
    private void ModeMenuHandler(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {   
        bool active = false;
        foreach(GameObject child in ModeMenu){
            active = !child.activeSelf;
            child.SetActive(!child.activeSelf);
        }
        vibration.Execute(0f, 0.1f, 50f, 1f, SteamVR_Input_Sources.Any);
    }
    private void OnDisable() {
        foreach(GameObject child in ModeMenu){
            if(child != null)
                child.SetActive(false);
        }
        // Remove SteamVR Actions Listener
        SteamVR_Actions.default_Menu.RemoveOnStateDownListener(MenuActionHandler,SteamVR_Input_Sources.Any);
        SteamVR_Actions.default_Lock.RemoveOnStateDownListener(Locked_onStateDown,SteamVR_Input_Sources.Any);
        SteamVR_Actions.default_ModeMenu.RemoveOnStateDownListener(ModeMenuHandler,SteamVR_Input_Sources.Any);
    }
    
}
