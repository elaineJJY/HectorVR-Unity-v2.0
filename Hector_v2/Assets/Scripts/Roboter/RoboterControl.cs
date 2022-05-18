/* RoboterControl.cs
 * authors: Lydia Ebbinghaus, Yannic Seidler
 * modified: Jingyi
 */

using System.Collections;
using UnityEngine;
using RosSharp.RosBridgeClient;

// Class to ontrol the Robot in Unity and getting control signals to the controller.
// Attached to robot prefabs (in folders Telemax, Asterix) that get instantiated in simulation scene.

public class RoboterControl : MonoBehaviour
{
    private RobotInformation info;
    float moveforward,movebackward,rotateL,rotateR;

    // Subscriber of position and inputreader of VR-controller.
    private PoseStampedSubscriber robot_pose_sub; 
    public VRInput input { get; set; }
    Vector2 controlSignal;

    // Calibration variables.
    private int changed;
    public int calibrationtime;
    public float speed;

    // Start is called before the first frame update.
    // Starting coroutine and declerating variables.
    void Start()
    {
        changed=0;
        robot_pose_sub = GameObject.Find("RosBridge").GetComponent<PoseStampedSubscriber>();
        input = GameObject.Find("Input").GetComponent<VRInput>();
        info = this.GetComponent<RobotInformation>();

        StartCoroutine("timer");
        Debug.Log("Roboter Control: Starting to update position of robot");
    }

    // Update is called once per frame.
    // Calibrates robot in the first "calibrationtime" cycles.
    void Update()
    {
        if(changed<calibrationtime)
        {
            calibrate();
            changed++;
        }

        switch (info.robotType)
        {
            // Orientation of asterix different
            case robotType.asterix_ugv:
                controlSignal = input.getSignal();
                controlSignal.y = -controlSignal.y;
                break;
            default:
                controlSignal = input.getSignal();
                break;
        }
        
        // Enable to control robot in unity and in simulation parallel. Needs proper calibration.
        //moveRobot();
    }

    // Moving robot in unity.
    private void moveRobot()
    {
        Vector3 b = new Vector3(-movebackward,0,0)*Time.deltaTime;
        transform.Translate(b, Space.Self);

        Vector3 m = new Vector3(moveforward,0,0)*Time.deltaTime;
        transform.Translate(m, Space.Self);

        Vector2 r = new Vector2(rotateR, -rotateL) *speed*10* Time.deltaTime;
        transform.Rotate(0, r.x, 0);
        transform.Rotate(0, r.y, 0);
    }

    // Set position of robot to position of real robot.
    private void calibrate()
    {
        this.transform.position = robot_pose_sub.position;
        this.transform.rotation = robot_pose_sub.rotation;
    }

    // Timer in a coroutine to calibrate every 0.05f seconds.
    IEnumerator timer()
    {
        while(true)
        {   
            // ! Test
            calibrate(); 
            yield return new WaitForSeconds(0.05f);
        }
    }

    // Returns control signal if robot is not locked. If robot locked : return (0,0,0).
    public Vector2 getControlSignal()
    {   
        // ! Test        
        return controlSignal;
    }
}
