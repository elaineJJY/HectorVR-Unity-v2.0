/* SelectRobot_Hints.cs
 * author: Yannic Seidler
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

// Class that shows a hint for a button that can be used to select the robot for simulation.
// Attached to GameObject Hints (Scene: SelectRobot)

public class SelectRobot_Hints : MonoBehaviour
{
    SteamVR_Action_Boolean select;

    // Start is called before the first frame update.
    void Start()
    {
        select = SteamVR_Input.GetBooleanAction("SelectRobotWithLaser");
    }

    // Update is called once per frame.
    void Update()
    {
        try
        {
            if (!ControllerButtonHints.IsButtonHintActive(Player.instance.rightHand, select))
            {
                ControllerButtonHints.ShowTextHint(Player.instance.rightHand, select, "Select Robot");
            }

        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
       
    }

}
