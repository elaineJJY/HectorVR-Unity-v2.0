using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class SpeedLinearEditor : MonoBehaviour
{
   
    public float speed;
    public Text speedText;
    public  LinearMapping  linearMapping;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        speed = linearMapping.value;
        speedText.text = (linearMapping.value).ToString();
    }
}
