/*
 * author: Lydia Ebbinghaus, Ayumi Bischoff, Yannic Seidler
 */
using UnityEngine;
using System.Collections;
using Valve.VR;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

// Class that handles controller input.
// Attached to GameObject Input in the hierachy.
public class VRInputCopy : MonoBehaviour
{
    public ScrollRect scrollRect;
    public SteamVR_LaserPointerMod laserPointer;
    public GameObject cameraMenu;
      public GameObject playerTextPanel;
    public TextMesh playerText;
    public CameraCreator cc;
    public MenuMaker menuMaker;
    public Hand leftHand;
    public Hand rightHand;
    public bool inMenu, isLocked;

    // If more functions are added: a possibility to differentiate between a game mode and menu mode.
    public SteamVR_ActionSet menuSet;
    //public SteamVR_ActionSet gameSet;
    SteamVR_Action_Vibration vibration = SteamVR_Input.GetVibrationAction("Haptic");
    SteamVR_Action_Vector2 touchpad = SteamVR_Input.GetVector2Action("read_touchpad");
    SteamVR_Action_Boolean touchPressed = SteamVR_Input.GetBooleanAction("is_klicked");
    SteamVR_Action_Boolean scrollUp = SteamVR_Input.GetBooleanAction("TouchUp");
    SteamVR_Action_Boolean scrollDown = SteamVR_Input.GetBooleanAction("TouchDown");
    SteamVR_Action_Boolean locked = SteamVR_Input.GetBooleanAction("Lock");
    SteamVR_Action_Boolean menu = SteamVR_Input.GetBooleanAction("Menu");
    Vector2 touchValue;
    bool down, up, controlPressed, interactUI, menuPressed;
  
    
    private void Awake()
    {
        locked.onStateDown += Locked_onStateDown;
    }

    // Start is called before the first frame update. At the beginning user is in menu and robot is locked.
    void Start()
    {
        playerTextPanel.SetActive(false);

        inMenu = true;
        isLocked = true;
        menuSet.Activate();
    }

    // Haptic feedback for lock button.  
    private void Locked_onStateDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if(!inMenu)
        {
            // Experimental: Writes Information and deletes text after specifc time period.
            // PrintInfoForXSeconds("Push Trackpad to drive.",5,true);

            vibration.Execute(0f, 0.5f, 100f, 1f, SteamVR_Input_Sources.RightHand);
        } 
    }

    private void OnDestroy()
    {
        locked.onStateDown -= Locked_onStateDown;
    }

    // Update is called once per frame
    // Gets input of controllers and handles it.
    // Same Controller Input handled depending on if user is in menu or not.
    void Update()
    {   Debug.Log( "touchValue" + touchpad.GetAxis(SteamVR_Input_Sources.RightHand));
        menuPressed = menu.GetStateDown(SteamVR_Input_Sources.LeftHand);
        if(menuPressed) OpenCloseMenu();

        if(inMenu)
        {
            down = scrollDown.GetState(SteamVR_Input_Sources.LeftHand);
            up = scrollUp.GetState(SteamVR_Input_Sources.LeftHand);
            if(down || up) scroll();
        }
        else
        {
            touchValue = touchpad.GetAxis(SteamVR_Input_Sources.RightHand);
            controlPressed = touchPressed.GetState(SteamVR_Input_Sources.RightHand);
            interactUI = locked.GetStateDown(SteamVR_Input_Sources.RightHand);
            if(interactUI) lockUnlock();
        }
    }

    // Closes menu if already open. Opens and updates menu if menu was closed.
    private void OpenCloseMenu()
    {
        Debug.Log("VRInput.cs: Menu pressed");

        if(inMenu)
        {
            //menuSet.Deactivate();
            //gameSet.Activate();

            cameraMenu.SetActive(false);
            isLocked = false;
            laserPointer.active = false;
            lockUnlock();
            playerText.text = "Robot is locked"; 
            playerTextPanel.SetActive(true);
        }
        else
        {
            //gameSet.Deactivate();
            //menuSet.Activate();
            
            // To update Camera Pictures when opening menu.
            playerText.text = "Please wait for Cameras to update";
            playerTextPanel.SetActive(true);
            cc.updateCams();
            StartCoroutine(Load());
            
            cameraMenu.SetActive(true);
            isLocked = true;
            laserPointer.active = true;
        }
        inMenu = !inMenu;
    }

    // Locks robot if robot was not locked. Unlocks robot if robot was locked before.
    private void lockUnlock()
    {
        if(isLocked)
        {
            playerText.text = "";
            playerTextPanel.SetActive(false);
        }
        else
        {
            playerText.text = ("Robot is locked");
            playerTextPanel.SetActive(true);
        }
            
        isLocked = !isLocked;
    }
    
    // Depending on input scrolls menu up or down.
    private void scroll()
    {
        if(down)
        {
            if(scrollRect.verticalNormalizedPosition >=0.03)
            {
                scrollRect.verticalNormalizedPosition-=0.05f;
                Debug.Log("VRInput.cs: Scroll down ");
            }
        }
        else if(up)
        {
            if(scrollRect.verticalNormalizedPosition <=0.98)
            {
                scrollRect.verticalNormalizedPosition+=0.05f;
                Debug.Log("VRInput.cs: Scroll up ");
            }   
        }
    }
    
    // Experimental: Prints a text for "seconds" seconds in player text. If showOldText is true, shows text that was shown before after time is over.
    IEnumerator PrintInfoForXSeconds(string text, float seconds, bool showOldText)
    {
        string oldPlayerText = playerText.text;
        playerText.text = text;
        Debug.Log(text);
        yield return new WaitForSecondsRealtime(seconds);

        if(showOldText) 
            playerText.text = oldPlayerText;
        else
        {
            playerText.text = "";
            playerTextPanel.SetActive(false);
        }
             
    }

    // Updates camera images and loads new image data on buttons.
    IEnumerator Load()
    {
        yield return new WaitWhile(() => !cc.finishedUpdating);

        Debug.Log("VRInput.cs: Starting to render updated camera Images");

        foreach(string camera in cc.getDictionary().Keys)
        {
            Debug.Log("VRInput.cs: Load : " + camera);
            GameObject button = GameObject.Find("/camera360/right/image_raw/compressedButton");
            menuMaker.loadTexture(camera);
        }

        if(inMenu)
        {
            playerText.text = "Cameras Updated.";
            yield return new WaitForSeconds(2);
            if(inMenu)
            {
                playerText.text = "";
                playerTextPanel.SetActive(false);
            }
        }
    }

    public Vector2 getSignal()
    {   
        return touchValue;
    }


    // true, if driving is enabled (e.g trackpad pressed).
    public bool getControlPressed()
    {   
        return controlPressed;
    }
}
