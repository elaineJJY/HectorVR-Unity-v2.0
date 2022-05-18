using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestState : MonoBehaviour
{
    // Start is called before the first frame update
    public Text info;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var testinfo ="";
        testinfo += "Remained Time: " + TestManagement.Instance.TotalTime + "\n";
        testinfo += "Target: " + TestManagement.Instance.rescuedTarget + "/" + TestManagement.Instance.totalTarget + "\n";
        info.text = testinfo;
    }
}
