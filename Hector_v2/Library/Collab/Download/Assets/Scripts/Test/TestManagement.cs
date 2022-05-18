using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using UnityEngine.SceneManagement;
using Valve.VR;
using UnityEngine.UI;

public class TestManagement : MonoBehaviour
{
    public static TestManagement Instance { get; private set; }
    
    // ------- Test Information-------------

    public float TotalTime = 600;   // default test time

    // robot info
    [Tooltip("Number of collisions detected")]
    public int triggerCount = 0;

    [Tooltip("Total driving distance. (meter)")]
    public float totalDriveDistance = 0;

     [Tooltip("Total driving time. (second)")]
    public float totalDriveTime = 0;

     [Tooltip("Adverage speed. (m/s)")]
    public float averageSpeed = 0;

    // target info
    public int totalTarget = 0;
    public int rescuedTarget = 0;    

    [Tooltip("visible target but not rescued")]
    public int visibleTarget = 0; 

    [Tooltip("Not detected target")]
    public int unvisibleTarget = 0;  
    
    // --------------


    public GameObject[] targetList;
    TestRobot currentTestRobot;
    SteamVR_LoadLevel loader;
    // Start is called before the first frame update
    void Start()
    {   
        Instance = this;
        loader = gameObject.GetComponent<SteamVR_LoadLevel>();
        StartCoroutine(Time());
        targetList = GameObject.FindGameObjectsWithTag("target");
        currentTestRobot = GameObject.FindGameObjectWithTag("robot").GetComponent<TestRobot>();
        totalTarget = targetList.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTestRobot != null){
            triggerCount = currentTestRobot.triggerCount;
            totalDriveDistance =  currentTestRobot.totalDriveDistance;
            totalDriveTime = currentTestRobot.totalDriveTime;
            averageSpeed = currentTestRobot.averageSpeed;
        }
        
        // caculate the target info   
        visibleTarget = 0;
        foreach(GameObject target in targetList){
            if(target!=null && target.layer != LayerMask.NameToLayer("Invisible")){
                visibleTarget++;
            }
        }
        unvisibleTarget = totalTarget - rescuedTarget - visibleTarget;
    }

    public void loadSceneAsync(string name)
    {   
        GameObject.Find("Input").GetComponent<VRInput>().CurrentMode.SetActive(false);
        Destroy(Player.instance.gameObject);

        // delete targets and robt
        foreach(Transform child in transform){
             Destroy(child.gameObject);
        }

        loader.levelName = name;
        loader.Trigger();
        //SceneManager.LoadSceneAsync(name);
    }

    IEnumerator Time()
    {
        while (TotalTime > 0)
        {
            yield return new WaitForSeconds(1);

            // all the people are rescued, end test
            if(totalTarget == rescuedTarget){
                break;
            }
            TotalTime--;
        }
        endTest();
    }

    // rescue the selected person
    public void rescue(GameObject target){
        if( target.layer != LayerMask.NameToLayer("Invisible")){
            GameObject.Destroy(target);
            rescuedTarget ++;
            InteractionManagement.Instance.SetPlayerText("Rescued People:  "+ (rescuedTarget-1) + " => " + rescuedTarget,5,false);
        }
    }

    public void endTest(){  
        var testinfo ="";
        testinfo += "End Test: " +  SceneManager.GetActiveScene().name + "\n";
        testinfo += "Remained Time: " + TotalTime + "\n";
        
        // Robot
        testinfo += "Collision:" + triggerCount + "\n";
        testinfo += "Drive Distance: " +  totalDriveDistance + "\n";
        testinfo += "Total driving time:" + totalDriveTime + "\n";
        testinfo += "Adverage speed: " + averageSpeed + "\n";

        // Target
        testinfo += "Rescued Target:" +  rescuedTarget + "/" + totalTarget +  "\n";
        testinfo += "Remained Visible Target: " +  visibleTarget + "\n";
        testinfo += "Remained Unvisible Target: " +  unvisibleTarget + "\n";
        Debug.Log(testinfo);

        InteractionManagement.Instance.SetMenu(true);
        InteractionManagement.Instance.SetPlayerText("The test has ended. Please wait.",5,false);
    }
}
