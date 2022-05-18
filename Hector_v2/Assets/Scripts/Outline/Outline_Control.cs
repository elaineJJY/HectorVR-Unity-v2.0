/* Outline_Control.cs
 * author: Yannic Seidler
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

// Class that sets the color of the outliner and the outline width. The Outline.cs script outlines the robots if the laser hitts them.
// Attached to robots in the SelectRobot scene.

public class Outline_Control : MonoBehaviour
{
    private Outline outLiner;
    
    private void Awake()
    {
        outLiner = GetComponent<Outline>();
        outLiner.OutlineColor = Color.yellow;
        outLiner.OutlineWidth = 5f;
        outLiner.enabled = false;
    }
}
