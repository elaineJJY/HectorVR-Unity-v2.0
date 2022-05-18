using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetViewManager : MonoBehaviour
{
    GameObject robot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable() {
         robot =  GameObject.FindGameObjectWithTag("robot");
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0,robot.transform.eulerAngles.y);
    }

    

}
