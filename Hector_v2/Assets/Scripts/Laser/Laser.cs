/* Laser.cs
 * author: Yannic Seidler
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using RosSharp.RosBridgeClient;

// Class that lets the player select a robot for simulation with a laser.
// Attached to GameObject Laser pointer (Scene: SelectRobot). 

public class Laser : MonoBehaviour
{
    public SteamVR_ActionSet SelectRobot;
    public SteamVR_Action_Boolean trigger;
    public Transform rightHand;
    public float laserLength;
    LineRenderer line;
    GameObject currentRobotSelected;
    private GameObject[] robots;
    private RosConnector rosbridge;
    
    // Start is called before the first frame update.
    // RosBridge is used to decide whether Ros is connected to Unity. Only than the simulation scene can be started. 

    void Start()
    {
        rosbridge = GameObject.Find("RosBridge").GetComponent<RosConnector>();

        line = GetComponent<LineRenderer>();
        line.startWidth = 0.005f;
        line.endWidth = 0.001f;
        line.positionCount = 2;

        robots = GameObject.FindGameObjectsWithTag("robot");

        trigger.onStateDown += Trigger_onStateDown;
        SelectRobot.Activate();
    }

    // Selects a robot and loads the simulation scene with the robot.

    private void Trigger_onStateDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (currentRobotSelected != null && rosbridge.connected)
        {
            GameObject.Find("SceneManagement").GetComponent<SceneManagement>().loadSceneAsync
                (currentRobotSelected.GetComponent<RobotInformation>().robotType);
            Destroy(this);
        }
    }

    //Specified robot gets outlined. Outliner for the other robots in list gets deactivated.
    // If no robot is specified the outliner for all robots gets deactivated.

    private void OutlineRobot(robotType robotToOutline, GameObject[] robots)
    {
        foreach (GameObject robot in robots)
        {
            if (robotToOutline != robotType.noRobot)
            {
                if (robot.GetComponent<RobotInformation>().robotType == robotToOutline)
                {
                    robot.GetComponent<Outline>().enabled = true;
                    continue;
                }
            }

            robot.GetComponent<Outline>().enabled = false;
        }
    }

    // Update is called once per frame.
    // Laser logic: renders laser in scene.
    // If the laser hits the robot the robot gets outlined. If you pull the trigger the robot gets selected.

    void Update()
    {

        transform.position = rightHand.position;
        transform.rotation = rightHand.rotation;
        Vector3 point1 = rightHand.position;
        Vector3 point2;
        
        Ray ray = new Ray(point1, rightHand.TransformDirection(Vector3.forward));
        RaycastHit hit;
        bool hitted = Physics.Raycast(ray, out hit, laserLength);
        
        if(hitted)
        {
            if (hit.transform.root.tag == "robot")
            {
                switch (hit.transform.root.gameObject.GetComponent<RobotInformation>().robotType)
                {
                    case robotType.drz_telemax:
                        OutlineRobot(robotType.drz_telemax, robots);
                        break;

                    case robotType.asterix_ugv:
                        OutlineRobot(robotType.asterix_ugv, robots);
                        break;
                }

                currentRobotSelected = hit.transform.root.gameObject;
            }
            else
            {
                OutlineRobot(robotType.noRobot, robots);
                currentRobotSelected = null;
            }
            point2 = hit.point;
        }
        
        else
        {
            point2 = point1 + rightHand.TransformDirection(Vector3.forward * laserLength);
            OutlineRobot(robotType.noRobot, robots);
            currentRobotSelected = null;
        }
        
        Vector3 [] points = new Vector3 [2] {point1 , point2};
        line.SetPositions(points);
    }

    // Removes the selection event.
    private void OnDestroy()
    {
        trigger.onStateDown -= Trigger_onStateDown;
        SelectRobot.Deactivate();
    }
}
