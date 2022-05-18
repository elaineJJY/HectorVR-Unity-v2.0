/* control_lock_color.cs
 * author: Yannic Seidler
 * modified:  Jingyi
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class that outlines the robot if the robot is locked.
// Attached to robot prefabs (in folders Telemax, Asterix) that get instantiated in the simulation scene.

public class control_lock_color : MonoBehaviour
{
    VRInput vrInput;
    Outline outLiner;
    
    // Start is called before the first frame update
    
    void Start()
    {
        outLiner = GetComponent<Outline>();
    }

    // Update is called once per frame
    // Enables the Outline.cs script which is also attached to the robot prefabs if the robot is locked. 
    void Update()
    {
        if (InteractionManagement.Instance != null)
        {
            if(InteractionManagement.Instance.Robot_Locked == true)
            {
                outLiner.enabled = true;
            }
            else
            {
                outLiner.enabled = false;
            }
        }
    }
}
