/* Loader.cs
 * author: Yannic Seidler & Lydia Ebbinghaus
 */

using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using RosSharp.RosBridgeClient;

// Class that prepares the scene for the simulation.
// Attached to GameObject ObjectLoader (Scene: Simulation).

public class Loader : MonoBehaviour
{
    public List<string> jointNames { get; set; }
    public List<JointStateWriterMod> jointStateWriters { get; set; }
    public string topic { get; set; }
    SceneManagement manager;
    private PoseStampedSubscriber robot_pose_sub;
    private GameObject rosBridge;
    Vector3 offsetFromRobot;
    float angle;
    
    
    // Start is called before the first frame update.
    // Sets robot and player into simulation scene and activates all scripts that are attached to the rosbridge.
    
    void Start()
    {
        rosBridge = GameObject.Find("RosBridge");
        foreach(MonoBehaviour script in rosBridge.GetComponents<MonoBehaviour>())
        {
            script.enabled = true;
        }

        robot_pose_sub = rosBridge.GetComponent<PoseStampedSubscriber>();
        manager = GameObject.Find("SceneManagement").GetComponent<SceneManagement>();
        GameObject robotToLoad = manager.simRobot!= null ? manager.simRobot : null;

        // Sets robot into scene.

        if (robotToLoad != null)
        {
           robotToLoad = Instantiate(robotToLoad, robot_pose_sub.position, robot_pose_sub.rotation);
        }
     
        SetupSimulation(robotToLoad, robotToLoad.GetComponent<RobotInformation>().robotType);

        // Sets player into scene.

        Vector3 offset = -(Player.instance.feetPositionGuess - Player.instance.trackingOriginTransform.position);
        Player.instance.trackingOriginTransform.position = robotToLoad.transform.position + offset + offsetFromRobot; 
        Player.instance.transform.GetChild(0).RotateAround(Player.instance.hmdTransform.position, Vector3.up, angle);

        // Sets Objects used for teleporting.

        GameObject.Find("Teleporting").transform.position = Player.instance.trackingOriginTransform.position;
        GameObject.Find("TeleportingGround").transform.position = Player.instance.trackingOriginTransform.position + offset;

        // Used for updating joints (works for telemax only until now).

        rosBridge.AddComponent<JointStateSubscriberMod>();
        Debug.Log("Jointsub added");
    }

        // Sets variables for positioning the player. Sets joints of a robot for the JointStateSubscriberMod.
        private void SetupSimulation(GameObject simRobot, robotType robot)
        {
            topic = "";
            jointNames = new List<string>();
            jointStateWriters = new List<JointStateWriterMod>();

            switch (robot)
            {
                case robotType.drz_telemax:

                    offsetFromRobot = -3f * simRobot.transform.forward;
                    angle = simRobot.transform.rotation.eulerAngles.y - Player.instance.hmdTransform.rotation.eulerAngles.y;

                // Configure joints

                    topic = "/telemax_control/joint_states";

                    try
                    {
                        jointNames.Add("arm_joint_0");
                        jointNames.Add("arm_joint_1");
                        jointNames.Add("arm_joint_2");
                        jointNames.Add("arm_joint_3");
                        jointNames.Add("arm_joint_4");
                        jointNames.Add("arm_joint_5");
                        jointNames.Add("flipper_back_left_joint");
                        jointNames.Add("flipper_back_right_joint");
                        jointNames.Add("flipper_front_left_joint");
                        jointNames.Add("flipper_front_right_joint");
                        jointNames.Add("gripper_joint");

                        List<JointStateWriterMod> temp = new List<JointStateWriterMod>(simRobot.GetComponentsInChildren<JointStateWriterMod>());

                        assignMatchingJoint(temp, jointStateWriters, "arm_link_0");
                        assignMatchingJoint(temp, jointStateWriters, "arm_link_1");
                        assignMatchingJoint(temp, jointStateWriters, "arm_link_2");
                        assignMatchingJoint(temp, jointStateWriters, "arm_link_3");
                        assignMatchingJoint(temp, jointStateWriters, "arm_link_4");
                        assignMatchingJoint(temp, jointStateWriters, "arm_link_5");
                        assignMatchingJoint(temp, jointStateWriters, "flipper_back_left_link");
                        assignMatchingJoint(temp, jointStateWriters, "flipper_back_right_link");
                        assignMatchingJoint(temp, jointStateWriters, "flipper_front_left_link");
                        assignMatchingJoint(temp, jointStateWriters, "flipper_front_right_link");
                        assignMatchingJoint(temp, jointStateWriters, "gripper_servo_link");
                    }

                    catch (System.Exception e)
                    {
                        Debug.Log(e.Message);
                    }

                    break;

                // To make this work attach "hinge joints" and "JointSTateWriterMod" to arm links and flipper.
                // Position and angle of joints doesn't fit to the one of real robot yet.
                case robotType.asterix_ugv:

                    offsetFromRobot = 2f * simRobot.transform.forward;
                    angle = 180f + simRobot.transform.rotation.eulerAngles.y - Player.instance.hmdTransform.rotation.eulerAngles.y;

                    //Configure joints
                    /*topic = "/arm_manipulator_control/joint_states";

                    try
                    {
                        jointNames.Add("arm_link_0");
                        jointNames.Add("arm_link_1");
                        jointNames.Add("arm_link_2");
                        jointNames.Add("arm_link_3");
                        jointNames.Add("arm_link_4");
                        jointNames.Add("arm_link_5");
                        jointNames.Add("flipper_back_left_joint");
                        jointNames.Add("flipper_back_right_joint");
                        jointNames.Add("flipper_front_left_joint");
                        jointNames.Add("flipper_front_right_joint");
                        jointNames.Add("gripper_joint");

                        List<JointStateWriterMod> temp = new List<JointStateWriterMod>(simRobot.GetComponentsInChildren<JointStateWriterMod>());

                        assignMatchingJoint(temp, jointStateWriters, "arm_joint_0");
                        assignMatchingJoint(temp, jointStateWriters, "arm_joint_1");
                        assignMatchingJoint(temp, jointStateWriters, "arm_joint_3");
                        assignMatchingJoint(temp, jointStateWriters, "arm_joint_3");
                        assignMatchingJoint(temp, jointStateWriters, "arm_joint_4");
                        assignMatchingJoint(temp, jointStateWriters, "arm_joint_5");
                        assignMatchingJoint(temp, jointStateWriters, "flipper_back_left_link");
                        assignMatchingJoint(temp, jointStateWriters, "flipper_back_right_link");
                        assignMatchingJoint(temp, jointStateWriters, "flipper_front_left_link");
                        assignMatchingJoint(temp, jointStateWriters, "flipper_front_right_link");
                        assignMatchingJoint(temp, jointStateWriters, "gripper_servo_link");
                    }

                    catch (System.Exception e)
                    {
                        Debug.Log(e.Message);
                    }*/

                break;

            }

        }
        
        // Helps assigning joints to a list that gets passed to the JointStateSubscriberMod.
        void assignMatchingJoint(List<JointStateWriterMod> inList, List<JointStateWriterMod> outList, string match)
        {
            foreach (JointStateWriterMod item in inList)
            {
                if (item.gameObject.name.Contains(match))
                {
                    outList.Add(item);
                    inList.Remove(item);
                    break;
                }
            }

        }
    }


   
