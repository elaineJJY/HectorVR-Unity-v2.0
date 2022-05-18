using UnityEngine;
using System.Collections;
using Valve.VR;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

// Class that handles controller input.
// Attached to GameObject Input in the hierachy.
public class VRInput : MonoBehaviour
{
    // public ScrollRect scrollRect;
    
    // public Hand leftHand;
    // public Hand rightHand;
 
    // If more functions are added: a possibility to differentiate between a game mode and menu mode.
    // public GameObject Player;

    public GameObject CurrentMode;
    public InteractionManagement InteractionManagement;
    public bool inMenu => InteractionManagement.Instance.Menu_Opened;
    public bool isLocked => InteractionManagement.Instance.Robot_Locked;

    public Vector2 touchValue;    
    
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
  

    // calculate the velocity and angle according to the mode
    public Vector2 getTouchValue()
    {   
        touchValue = CurrentMode.GetComponent<IMode>().Signal;
        return CurrentMode.GetComponent<IMode>().Signal;
    }

    // Change the Mode， if not existed, change it to Default Mode
    public void SetMode(GameObject mode){
        if( this.CurrentMode != mode){
            this.CurrentMode.SetActive(false);
            mode.SetActive(true);
            this.CurrentMode = mode;
        } 
    }

}
