using UnityEngine;
using System.Collections;
using Valve.VR;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class VRInput : MonoBehaviour
{
    public GameObject CurrentMode;
    public Vector2 signal; 
    
    private void Awake()
    {   
        
    }

    // Start is called before the first frame update. At the beginning user is in menu and robot is locked.
    void Start()
    {
        if(CurrentMode != null && !CurrentMode.activeSelf){
            CurrentMode.SetActive(true);
        }
    }

    
    // Update is called once per frame
    void Update()
    {  

    }
  

    // update the velocity and angle according to the mode
    public Vector2 getSignal()
    {   
        Vector2 signal = Vector2.zero;
        if(CurrentMode!=null){
            signal = CurrentMode.GetComponent<IMode>().Signal;
        }
        this.signal = signal;
        return signal;
            
    }

    // Change the Mode so that it can get its signal
    public void SetMode(GameObject mode){
        if( this.CurrentMode != mode){
            if(this.CurrentMode != null){
                this.CurrentMode.SetActive(false);
            }
            mode.SetActive(true);
            this.CurrentMode = mode;
        } 
    }

}
