/* Movement.cs
 * author: Yannic Seidler
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class that rotates the robots in the SelectRobot scene.
// Attached to GameObject RobotPresentation (Scene: SelectRobot).

public class Movement : MonoBehaviour
{
    public GameObject[] robots;
    
    // Start is called before the first frame update.
    void Start()
    {
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        float sign = -1f;
        float startTime;
        for (; ; )
        {
            sign = -sign;
            startTime = Time.time;
            
            while (Time.time - startTime < 7f)
            {
                foreach (GameObject robot in robots)
                {
                    robot.transform.Rotate(new Vector3(0, 45, 0) * sign * Time.deltaTime, Space.Self);
                }
                yield return null;
            }
            yield return new WaitForSecondsRealtime(3f);

        }
    }
   
}
