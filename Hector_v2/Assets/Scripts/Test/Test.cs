using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update

    public bool test_Robot_Movement;
    public bool test_Robot_AutoDrive;
    public GameObject autoDriveTarget;
    public bool autoDrive;
    public Vector2 signal;

    private GameObject[] robots;

    private void Start() {

    }

    // Update is called once per frame
    void Update()
    {   
        if(robots == null || robots.Length==0) robots = GameObject.FindGameObjectsWithTag("robot");
        foreach (GameObject robot in robots){
            if(test_Robot_Movement) move(robot);
            if(test_Robot_AutoDrive){
                startAutoDrive(robot);
                Vector3  linearVelocity = new Vector3(0,0,signal.y);
                Vector3  angularVelocity = new Vector3(0,signal.x,0);
                robot.transform.Translate(linearVelocity);
                robot.transform.Rotate(angularVelocity);  
            } 
        }

    }

    
    
    // move the robot in unity
    private void move(GameObject robot){  
        RoboterControl robotControl = robot.GetComponent<RoboterControl>();
        if(robotControl != null && robotControl.enabled){
            Vector2  controlSignal = robot.GetComponent<RoboterControl>().getControlSignal();
            Vector3  linearVelocity = new Vector3(0,0,controlSignal.y);
            Vector3  angularVelocity = new Vector3(0,controlSignal.x,0);
            robot.transform.Translate(linearVelocity);
            robot.transform.Rotate(angularVelocity);  
        }
        else{
            Vector2 controlSignal = GameObject.Find("Input").GetComponent<VRInput>().getSignal();
            Vector3  linearVelocity = new Vector3(0,0,controlSignal.y);
            Vector3  angularVelocity = new Vector3(0,controlSignal.x,0);
            robot.transform.Translate(linearVelocity);
            robot.transform.Rotate(angularVelocity);  
        }
    }

     private void startAutoDrive(GameObject robot){
       
        float distance = Vector3.Distance(autoDriveTarget.transform.position, robot.transform.position);
        if(distance < 1){
            signal =  new Vector2(0,0);
            autoDrive = false;
        }
        if(autoDrive){
            float angleDiff = Vector3.Angle( autoDriveTarget.transform.position - robot.transform.position, robot.transform.forward);

            if(angleDiff > 1){
                Vector3 normal = Vector3.Cross(autoDriveTarget.transform.position, robot.transform.position);
                float direction = Mathf.Sign (Vector3.Dot(normal,robot.transform.forward));
                signal =  new Vector2(direction * 0.2f, 0.01f * (180-angleDiff)/180);
            }
            else{
                //signal =  new Vector2(0,velocitySpeed);
                signal =  new Vector2(0,0.1f);
            }

        }
        
        
    }
}
