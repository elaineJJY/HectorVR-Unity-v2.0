using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSelectEffect : MonoBehaviour
{
    public GameObject TaskDiscription;
    private void OnEnable() {
        TaskDiscription.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    private void OnDisable() {   
        TaskDiscription.SetActive(false);
    }
}
