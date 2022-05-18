/*
 * Handle the Event in Lab Mode
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

public class Lab : MonoBehaviour,IMode
{
    
    public Player_Control Player_Control;
    public GameObject[] ModeMenu;
    public MeshRenderer LockView;
    public GameObject[] CustomHand;
    public Hand[] OriginHand;
    public Vector2 signal;
    public Vector2 Signal => signal;
    public SpeedLinearEditor velocitySpeedEditor;
    public SpeedLinearEditor angleSpeedEditor;

    public float velocitySpeed;
    public float angleSpeed;
    
    // change the speed, so that the robot can run faster after editing
    

    public Joystick angle_joystick;
    public Joystick velocity_joystick;

    public float angle,velocity;
    SteamVR_Action_Vibration vibration = SteamVR_Input.GetVibrationAction("Haptic");
    
    Player player;
    InteractionManagement interactionManagement;
    GameObject robot;
    bool forward = false;
    bool angle_joystickAttached = false;
    bool velocity_joystickAttached = false;

    private void OnEnable() {
        GameObject.Find("Input").GetComponent<VRInput>().SetMode(this.gameObject);

        // The player cannot be raised or lowered (the aerial perspective is disabled)
        Player_Control.upDownEnabled = false; 
        
        interactionManagement = InteractionManagement.Instance;
        interactionManagement.SetPlayerText( "Control Mode : " + gameObject.name, 7, false);
        interactionManagement.HideController();

        robot =  GameObject.FindGameObjectWithTag("robot");
        

        player = Player.instance;

        player.transform.position = gameObject.transform.position + new Vector3(0,0,26);
        player.transform.LookAt(gameObject.transform);

        // reset the menu position
        interactionManagement.SetMenu(false);
        interactionManagement.SetMenu(true);

        // Add SteamVR Actions Listener
        SteamVR_Actions.default_Menu.AddOnStateDownListener(MenuActionHandler,SteamVR_Input_Sources.Any);

        // if not do this, it will be some problems with the display of custom hands. Reseason is unknown
        SetCustomHand(false);
        SetCustomHand(true);
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {   
        velocitySpeed = velocitySpeedEditor.speed;
        angleSpeed = angleSpeedEditor.speed;
        if(InteractionManagement.Instance.Robot_Locked){
            signal =  new Vector2(0,0);
            LockView.material.color = new Color32(183, 0, 14, 255);
        }
        else{
            LockView.material.color = new Color32(74, 126, 52, 255);
            UpdateSignal();
        }

    }
    
    public void StartSettingAngle(){
        angle_joystickAttached = true;
    }

    public void StopSettingAngle(){
        angle_joystickAttached = false;
        this.angle = 0;
    }

     public void StartSettingVelocity(){
        velocity_joystickAttached = true;
    }

    public void StopSettinVelocity(){
        velocity_joystickAttached = false;
        this.velocity = 0;
    }

    public void UpdateSignal()
    {   velocity = 0;
        angle = 0;

        // the robot will stop/start move forward automatically
        if(forward){
            velocity =  velocitySpeed;    // justify the speed so that it will not run too fast
        }

        // change the direction of the robot
        if(angle_joystickAttached){
            angle = System.Math.Abs(angle_joystick.value.y)>0.1 ? angle_joystick.value.y * angleSpeed : 0;
        }

        // the robot will move forwards or backwards
        if(velocity_joystickAttached){
            velocity = System.Math.Abs(velocity_joystick.value.y)>0.1 ? -velocity_joystick.value.y * velocitySpeed : 0;

            // close the auto drive
            if(forward){
                interactionManagement.SetPlayerText("Start Auto Driving",5,false);
                forward = false;
            }
        }
        signal =  new Vector2(angle,velocity);
     
    }

    // Handle Button Click Effect: the robot will stop/start move forward automatically 
    public void goForward(GameObject button){
        forward = !forward;
        if(forward){
            interactionManagement.SetPlayerText("Start Auto Driving",5,false);
        }
        else{
            interactionManagement.SetPlayerText("Stop Auto Driving",5,false);
        }
        vibration.Execute(0f, 0.1f, 50f, 1f, SteamVR_Input_Sources.Any);
    }

    // Handle Button Click Effect: Rescue the target
    public void rescue(){
    
        foreach(GameObject target in TestManagement.Instance.targetList){
            if(target!=null){
                float distance = Vector3.Distance(target.transform.position, robot.transform.position);
                if(distance <= 5){
                   TestManagement.Instance.rescue(target);
                }
            }
        }
        vibration.Execute(0f, 0.1f, 50f, 1f, SteamVR_Input_Sources.Any);
    }


    public void LockButtonDown(){
        interactionManagement.LockRobot();
        vibration.Execute(0f, 0.1f, 50f, 1f, SteamVR_Input_Sources.Any);
    }


    private void MenuActionHandler(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {   
        interactionManagement.SetMenu();
        
        // Mode Selektoon Setting
        // in Lab Mode, the user can not see the Controller
        if(!interactionManagement.Menu_Opened){
            interactionManagement.HideController();
            SetCustomHand(true);
        }
        else{
            SetCustomHand(false);
        }
        vibration.Execute(0f, 0.1f, 50f, 1f, SteamVR_Input_Sources.Any);
    }
    

    public void SetCustomHand(bool open){
        foreach(GameObject hand in CustomHand){
            hand.SetActive(open);
        }
        foreach(Hand hand in OriginHand){
            hand.mainRenderModel.SetVisibility(!open);
        }
    }


    private void OnDisable() {

        // Remove SteamVR Actions Listener
        SteamVR_Actions.default_Menu.RemoveOnStateDownListener(MenuActionHandler,SteamVR_Input_Sources.Any);
        player.transform.position = robot.transform.position + new Vector3(-1,0,0);;

        // close the custom hand
        SetCustomHand(false);
    }
    
}
