/* RobotInformation.cs
 * author: Yannic Seidler
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for robot prop.
// Attached to all robot models.

// add robot type here if you are using a new robot. 
public enum robotType {drz_telemax, asterix_ugv, noRobot};

public class RobotInformation : MonoBehaviour
{
    public robotType robotType = robotType.drz_telemax;
}

