/*
 * Handle the Event in UI Mode
 *
 * 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem.Sample;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;
using UnityEngine.AI;

public class UI_Mode: MonoBehaviour,IMode
{

    // ------------------------ UI Menu

    public UnityEngine.UI.Toggle lock_robot_Toggle;
    public UnityEngine.UI.Toggle follow_me_Toggle;
    public UnityEngine.UI.Slider velocity_Slider;
    public bool userControl = false;

    // ------------------------
    InteractionManagement interactionManagement;
    public Player_Control Player_Control;
    public GameObject[] ModeMenu;
    public Vector2 signal;
    public Vector2 Signal => signal;
    SteamVR_Action_Vibration vibration = SteamVR_Input.GetVibrationAction("Haptic");
    GameObject robot;
    Player player;
    private NavMeshAgent navMeshAgent;
    private float originSpeed;    // origin speed from navMEshAgent;
    private void OnEnable() {

        GameObject.Find("Input").GetComponent<VRInput>().SetMode(this.gameObject);
        
        // The player can be raised or lowered (the aerial perspective is enabled)
        Player_Control.upDownEnabled = true;

        robot =  GameObject.FindGameObjectWithTag("robot");
        navMeshAgent = robot.GetComponent<NavMeshAgent>();
        originSpeed = navMeshAgent.speed;

        navMeshAgent.stoppingDistance = 2;
        player = Player.instance;

        interactionManagement = InteractionManagement.Instance;
        interactionManagement.SetPlayerText( "Control Mode : " + gameObject.name, 7, false);
        interactionManagement.SetMenu(false);

        // open the mode menu
        foreach(GameObject child in ModeMenu){
            child.SetActive(true);
        }

        // Add  SteamVR Actions Listener
        SteamVR_Actions.default_Menu.AddOnStateDownListener(MenuActionHandler,SteamVR_Input_Sources.Any);
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
            lock_robot_Toggle.isOn = true;
            navMeshAgent.ResetPath();
        }
        else{
            UpdateSignal();
            lock_robot_Toggle.isOn = false;
        }
    }

    public void UpdateSignal()
    {   
        if(!userControl){
            signal = Vector2.zero;
        }
        
        // Auto driving
        if(follow_me_Toggle.isOn){
            Vector3 targetPosition = player.transform.position;
            targetPosition.y = robot.transform.position.y;
            navMeshAgent.SetDestination(targetPosition);
            navMeshAgent.speed = originSpeed * velocity_Slider.value;
        }
        else{
            // stop auto driving
            navMeshAgent.ResetPath();
        }     
        
    }

    // Handle Button Select Effect: Left or Right
    // angele = -1/1
    public void setAngleStart(float angle){
        if(interactionManagement.Robot_Locked){
            interactionManagement.SetPlayerText("Please unlock the robot");
        }
        else{
            userControl = true;
            this.signal = new Vector2(angle,0);
        }
        
    }

    // Handle Button Select Effect: Forward or Backward
    // velocity = -1/1
    public void setVelocityStart(float velocity){
        if(interactionManagement.Robot_Locked){
            interactionManagement.SetPlayerText("Please unlock the robot");
        }
        else{
           userControl = true;
           this.signal = new Vector2(0, velocity * velocity_Slider.value);
        }
        
    }

    public void setControlEnde(){
        this.signal = new Vector2(0,0);
        userControl = false;
    }

    public void lockRobot(){
        interactionManagement.LockRobot();
    }

    private void MenuActionHandler(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        interactionManagement.SetMenu();
   
        vibration.Execute(0f, 0.1f, 50f, 1f, SteamVR_Input_Sources.Any);
    }

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

        navMeshAgent.speed = originSpeed;

        // Remove SteamVR Actions Listener
        SteamVR_Actions.default_Menu.RemoveOnStateDownListener(MenuActionHandler,SteamVR_Input_Sources.Any);
        SteamVR_Actions.default_ModeMenu.RemoveOnStateDownListener(ModeMenuHandler,SteamVR_Input_Sources.Any);
    }

}

