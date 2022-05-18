using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using UnityEngine.SceneManagement;
using Valve.VR;
using System.Text;
using System.IO;

public class TestManagement : MonoBehaviour
{
    public static TestManagement Instance { get; private set; }
    public int TestID = 1;
    public string csvPath = @"C:\Users\alley\Desktop\Thesis-Hector-VR\User Study\TestResult";
    public int TestMode = 1;
    public GameObject[] ModeList;
    
    // ------- Test Information-------------

    public float TotalTime = 300;   // default test time

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

    [HideInInspector]
    public GameObject[] targetList;
    TestRobot currentTestRobot;
    SteamVR_LoadLevel loader;
    // Start is called before the first frame update
    void Start()
    {   
        // synchronization
        if(Instance!=null){
            TestID = Instance.TestID;
            TestMode = Instance.TestMode;
            csvPath = Instance.csvPath;
        }
        Instance = this;

        // Handle the sequence of the mode to test in simulation
        if(SceneManager.GetActiveScene().name == "Simulation"){
            initModeList();
            ModeList[TestMode-1].SetActive(true);
        }

        loader = gameObject.GetComponent<SteamVR_LoadLevel>();
        StartCoroutine(Time());
        targetList = GameObject.FindGameObjectsWithTag("target");
        currentTestRobot = GameObject.FindGameObjectWithTag("robot").GetComponent<TestRobot>();
        totalTarget = targetList.Length;
    }


    // Caculate the sequence of the modes based on Latin Square (size = 4)
    private void initModeList()
    {   
        // if the ID is illegal, the default Sequence will be used
        // all the date will be saved in 0.csv as a test date
        bool illegalID = false;
        if(TestID <=0){
            TestID = 1;
            illegalID = true;
        }

        var modes =  ModeList;
        ModeList = new GameObject[4];
    
        for(int i =0;i<modes.Length;i++){
            ModeList[i] =  modes[(TestID-1+i)%4];
        } 
        if(illegalID){
            TestID = 0;
        }
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

        // delete targets and robot
        foreach(Transform child in transform){
            Destroy(child.gameObject);
        }

        // change scene
        loader.levelName = name;
        loader.Trigger();
        
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

        writeCSV();
        InteractionManagement.Instance.SetMenu(true);
        InteractionManagement.Instance.SetPlayerText("The test has ended. Please wait.",5,false);
        
        // next text
        TestMode++;
    }

    private void writeCSV(){
        
        string strFilePath = csvPath + @"\" + TestID +".csv";
        
        string strSeperator = ",";
        string line = "";
        if(!File.Exists(strFilePath)){
            line += "participant" + strSeperator;
            line += "condition" + strSeperator;
            line += "Remained Time" + strSeperator;
            line += "Collision" + strSeperator;
            line += "Drive Distance" + strSeperator;
            line += "Total driving time" + strSeperator;
            line += "Adverage speed" + strSeperator;
            line += "Rescued Target" + strSeperator;
            line += "Remained Visible Target" + strSeperator;
            line += "Remained Unvisible Target" + strSeperator;
            line += "time" + strSeperator;
            line += "\n";

            // Create and write the csv file
            File.WriteAllText(strFilePath, line);
            line = "";
        }

        line += TestID + strSeperator;
        line += SceneManager.GetActiveScene().name + strSeperator;
        line += TotalTime + strSeperator;
        line += triggerCount + strSeperator;
        line += totalDriveDistance + strSeperator;
        line += totalDriveTime + strSeperator;
        line += averageSpeed + strSeperator;
        line += rescuedTarget + strSeperator;
        line += visibleTarget + strSeperator;
        line += unvisibleTarget + strSeperator;
        line += System.DateTime.Now.ToString(("yyyy/MM/dd HH:mm"))+ strSeperator;
        line += "\n";

        // To append line to the csv file
        File.AppendAllText(strFilePath, line);
    }
}
