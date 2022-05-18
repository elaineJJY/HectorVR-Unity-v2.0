using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Valve.VR;

public class TestRobot : MonoBehaviour
{


    [Tooltip("The speed of the robot")]
    public float linearSpeed = 0.1f;

    [Tooltip("The speed of the robot's angle change")]
    public float angularSpeed = 1f;

    // ------- Test Information-------------
    [Tooltip("Number of collisions detected")]
    public int triggerCount = 0;

    [Tooltip("Total driving distance. (meter)")]
    public float totalDriveDistance = 0;

     [Tooltip("Total driving time. (second)")]
    public float totalDriveTime = 0;

     [Tooltip("Adverage speed. (m/s)")]
    public float averageSpeed = 0;
   
    // --------------

    private Vector3 PrePosition;
    public AudioSource CollisionAlarm;
    
 
    SteamVR_Action_Vibration vibration = SteamVR_Input.GetVibrationAction("Haptic");
    
    // Start is called before the first frame update
    void Start()
    {
        PrePosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        //move();
        // caculate the distance
        totalDriveDistance += Vector3.Distance(PrePosition,transform.position);
        PrePosition = transform.position;

        // caculate the time
        Vector2 controlSignal = GameObject.Find("Input").GetComponent<VRInput>().getSignal();
        if(controlSignal!=Vector2.zero || this.GetComponent<NavMeshAgent>().velocity!=Vector3.zero){
            totalDriveTime += Time.deltaTime;
            averageSpeed = totalDriveDistance/totalDriveTime;
        }
      

    }

    private void FixedUpdate()
    {
        move();
    }

    // move the robot in unity
    private void move(){  
        Vector3  linearVelocity = Vector2.zero;
        Vector3  angularVelocity = Vector2.zero;
        RoboterControl robotControl =this.GetComponent<RoboterControl>();
        if(robotControl != null && robotControl.enabled){
            Vector2  controlSignal = this.GetComponent<RoboterControl>().getControlSignal();
            linearVelocity = new Vector3(0,0,controlSignal.y * linearSpeed);
            angularVelocity = new Vector3(0,controlSignal.x * angularSpeed,0);
            
        }
        else{
            Vector2 controlSignal = GameObject.Find("Input").GetComponent<VRInput>().getSignal();
            linearVelocity = new Vector3(0,0,controlSignal.y * linearSpeed);
            angularVelocity = new Vector3(0,controlSignal.x * angularSpeed,0);
        }
        this.transform.Translate(linearVelocity);
        this.transform.Rotate(angularVelocity);  
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Building"){
            triggerCount++;
            CollisionAlarm.Play();
            vibration.Execute(0f, 0.1f, 50f, 0.5f, SteamVR_Input_Sources.Any);
        }    
    }

  

   

}


