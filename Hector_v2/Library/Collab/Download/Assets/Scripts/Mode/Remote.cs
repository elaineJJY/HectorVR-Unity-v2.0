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
using UnityEngine.AI;

public class Remote: MonoBehaviour,IMode
{
    InteractionManagement interactionManagement;
    public Player_Control Player_Control;

    public RemoteController remoteController;

    public Valve.VR.InteractionSystem.TargetLaser targetLaser;
    public GameObject[] ModeMenu;
    public Vector2 signal;
    public Vector2 Signal => signal;
    SteamVR_Action_Vibration vibration = SteamVR_Input.GetVibrationAction("Haptic");

    GameObject robot;
    private NavMeshAgent navMeshAgent;
    private void OnEnable() {
        
        // The player can be raised or lowered (the aerial perspective is enabled)
        Player_Control.upDownEnabled = true;
        
        robot =  GameObject.FindGameObjectWithTag("robot");
        navMeshAgent = robot.GetComponent<NavMeshAgent>();
        navMeshAgent.stoppingDistance = 1;

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
        signal = Vector2.zero;

        // Auto driving
        if(targetLaser.autoDrive){
            navMeshAgent.SetDestination(targetLaser.targetPosition);
            
            float distance = Vector3.Distance(targetLaser.targetPosition, robot.transform.position);

            // Already arrive at the target position
            if(distance <= 1){
                stopAutoDrive();
            }
           
        }

        // get control signal from the joystick
        if(remoteController != null){
            if(remoteController.angle != 0 || remoteController.velocity !=0 ){
                signal =  new Vector2(remoteController.angle,remoteController.velocity * 0.3f);   
                if(targetLaser.autoDrive){
                    stopAutoDrive();
                }
            } 
        }
    }

    void stopAutoDrive(){
        signal =  new Vector2(0,0);
        targetLaser.autoDrive = false;
        navMeshAgent.SetDestination(robot.transform.position);
        navMeshAgent.ResetPath();
        GameObject.Destroy(targetLaser.TargetFlag);
        //interactionManagement.SetPlayerText("Stop Auto Driving",5,false);
    }

    private void MenuActionHandler(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        interactionManagement.SetMenu();
        vibration.Execute(0f, 0.1f, 50f, 1f, SteamVR_Input_Sources.Any);
        if(interactionManagement.Menu_Opened){
            foreach(GameObject item in GameObject.FindGameObjectsWithTag("ItemPickup")){
                GameObject.Destroy(item);
            }
            stopAutoDrive();
        }
        
    }

    // Handle Robot Locking
    private void Locked_onStateDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (!interactionManagement.Menu_Opened)
        {
            interactionManagement.LockRobot();
        }
        else {
            interactionManagement.SetPlayerText("Please close the menu first",5,false);
        }
        if(interactionManagement.Robot_Locked){
            stopAutoDrive();
        }
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

        // Remove SteamVR Actions Listener
        SteamVR_Actions.default_Menu.RemoveOnStateDownListener(MenuActionHandler,SteamVR_Input_Sources.Any);
        SteamVR_Actions.default_Lock.RemoveOnStateDownListener(Locked_onStateDown,SteamVR_Input_Sources.Any);
        SteamVR_Actions.default_ModeMenu.RemoveOnStateDownListener(ModeMenuHandler,SteamVR_Input_Sources.Any);

        foreach(GameObject item in GameObject.FindGameObjectsWithTag("ItemPickup")){
            GameObject.Destroy(item);
        }

        stopAutoDrive();
        // targetLaser.autoDrive = false;
        // GameObject.Destroy(targetLaser.TargetFlag);

    }

}
