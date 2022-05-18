/* Player_Stats.cs
 * author: Yannic Seidler
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;
using RosSharp.RosBridgeClient;

// Class to display player/ robot information
// Attached to PlayerStats (Scene: Simulation)

public class Player_Stats : MonoBehaviour
{
    public Text player_text;
    public Text hmd_text;
    public Text robot_pose;
    Player player;
    PoseStampedSubscriber poseStampedSubscriber;
    Vector3 player_pos;
    Vector3 hmd_Camera_pos;

    // Start is called before the first frame update.
    void Start()
    {
        poseStampedSubscriber = GameObject.Find("RosBridge").GetComponent<PoseStampedSubscriber>();
        player = Player.instance;
    }

    // Updates the players/ robots status once per frame.
    void Update()
    {
        player_pos = player.trackingOriginTransform.position;
        hmd_Camera_pos = player.hmdTransform.position;
        player_text.text = player_text.name + " \tpose: " + player_pos.ToString();
        hmd_text.text = hmd_text.name + " \tpose: " + hmd_Camera_pos.ToString();
        robot_pose.text = robot_pose.name + "\tpose: " + poseStampedSubscriber.position.ToString();
    }
}
