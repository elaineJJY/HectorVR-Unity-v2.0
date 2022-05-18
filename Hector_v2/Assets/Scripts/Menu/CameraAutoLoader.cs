using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAutoLoader : MonoBehaviour
{
    public MenuMaker CameraContent;

    private void OnEnable() {
        if(InteractionManagement.Instance != null)
            InteractionManagement.Instance.SetCamera(CameraContent);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
