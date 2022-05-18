/* SceneManagement.cs
 * author: Yannic Seidler
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using UnityEngine.SceneManagement;
using RosSharp.RosBridgeClient;

// Class that handels transition between scenes.
// Attached to GameObject SceneManagement (Scene: SelectRobot).

public class SceneManagement : MonoBehaviour
{
    public robotType robotToLoad { get; set; }
    public GameObject simRobot { get; set; }
    public GameObject[] robots;
    public string topic {get; set;}

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Loads the simulation scene asynchronously.
    public void loadSceneAsync(robotType robot)
    {
        try
        {
            simRobot = getRobot(robot);
        }
        catch (RobotNotFoundInListException e)
        {
            Debug.Log(e.Message);
        }
        Destroy(Player.instance.gameObject);
        SceneManager.LoadSceneAsync(1);
    }

    private GameObject getRobot(robotType robot)
    {
        for (int i = 0; i < robots.Length; i++)
        {
            if (robots[i] != null)
            {
                if (robots[i].GetComponent<RobotInformation>().robotType == robot)
                {
                    return robots[i];
                }
            }
        }
        throw new RobotNotFoundInListException("Robot could not be found in robot list");
    }

    private class RobotNotFoundInListException : System.Exception
    {
        public RobotNotFoundInListException(string message) : base(message) { } 
    }

}
