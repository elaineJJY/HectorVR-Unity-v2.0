/* NoVRRoboterControl.cs
 * authors: Lydia ebbinghaus, Ayumi Bischoff, Yannic Seidler
 */

using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using RosSharp.RosBridgeClient;
using Valve.VR.InteractionSystem;

// Class to Control the Robot in Unity and getting control signals over the Controller.
// Attach to robot prefab in 2D mode (not recommended)

public class NoVRRoboterControl : MonoBehaviour
{

    // Just for using XBoxController &Keayboard for control.
    XBoxController controler;
    float moveforward;
    float movebackward;
    float rotateL;
    float rotateR;


    // Subscriber of position and inputreader of VR-controller.
    private PoseStampedSubscriber robot_pose_sub; 
    Vector2 controlSignal;

    // Calibration variables.
    private int changed;
    public int calibrationtime;
    public float speed;

    private void Awake()
    {
        controler = new XBoxController();
        MovementSetUp();
    }

    // Setup of control with Input of old gamepad or keyboard..
    private void MovementSetUp()
    {
        controler.Movement.Moveforward.performed += (InputAction.CallbackContext context) => moveforward = context.ReadValue<float>() * speed;
        controler.Movement.Moveforward.canceled += (InputAction.CallbackContext context) => moveforward = 0;
        controler.Movement.Movebackward.performed += (InputAction.CallbackContext context) => movebackward = context.ReadValue<float>() * speed;
        controler.Movement.Movebackward.canceled += (InputAction.CallbackContext context) => movebackward = 0;
        controler.Movement.RotationL.performed += (InputAction.CallbackContext context) => rotateL = context.ReadValue<float>() * speed;
        controler.Movement.RotationL.canceled += (InputAction.CallbackContext context) => rotateL = 0;
        controler.Movement.RotationR.performed += (InputAction.CallbackContext context) => rotateR = context.ReadValue<float>() * speed;
        controler.Movement.RotationR.canceled += (InputAction.CallbackContext context) => rotateR = 0;
    }

    private void OnEnable()
    {
        Debug.Log("Controller enabled");
        controler.Movement.Enable();
    }

    private void OnDisable()
    {
        Debug.Log("Controller disabled");
        controler.Movement.Disable();
    }

    // Start is called before the first frame update.
    // Starting coroutine and declerating variables.
    void Start()
    {
        changed=0;
        robot_pose_sub = GameObject.Find("RosBridge").GetComponent<PoseStampedSubscriber>();
        Debug.Log("Starting to update position");
        StartCoroutine("timer");
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
        // Experimental: Enable to control robot in unity and in simulation parallel.
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

    // Timer in a coroutine to calibrate every 0.1f seconds.
    IEnumerator timer()
    {
        while(true)
        {
            calibrate();
            yield return new WaitForSeconds(0.05f);
        }
    }

    // Returns control signal for linear and angular velocity for old gamepad input
    public (float lin, float ang) getControlSignalXBox()    
    {
        float x,y;

        if(moveforward==0)
        {
            x=movebackward;
        }else
        {
            x=-moveforward;
        }

        if(rotateL==0){
            y=rotateR;
        }else
        {
            y=-rotateL;
        }

        return (x*Time.deltaTime*10, y*speed*10* Time.deltaTime);
    }
}
