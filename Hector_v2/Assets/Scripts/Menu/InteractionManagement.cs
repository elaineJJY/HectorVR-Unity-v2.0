/*
 * Control center, processing interaction, menu display, robot lock etc.
 * 
 */

using UnityEngine;
using System.Collections;
using Valve.VR;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;
using Valve.VR.InteractionSystem.Sample;
using TMPro;

public class InteractionManagement : MonoBehaviour
{
    public static InteractionManagement Instance { get; private set; }
    public bool Robot_Locked = false;
    public bool Menu_Opened = true;
    public GameObject MenuGUI;
    public TextMeshPro playerText;
    public GameObject playerTextPanel;
    public  CameraCreator cc;

    public GameObject[] Laser;
    
    // Listener(Handle the Change of the attribute)
    private bool Robot_Locked_Listener{
        get{return Robot_Locked; }
        set{
            if(Robot_Locked){
                SetPlayerText("Robot is locked",7,false); 
            }
            else{
                SetPlayerText ("Robot is unlocked",7,false); 
            }
        }
    }

    private bool Menu_Opened_Listener{
        get{return Menu_Opened; }
        set{
            if(Menu_Opened)
            {   
                // open the menu
                SetPlayerText("Open the Menu", 3, false);
                foreach(GameObject child in Laser){
                    child.SetActive(true);
                }
                MenuGUI.SetActive(true);
                LockRobot(true);
                ShowController();
            }
            else
            {
                MenuGUI.SetActive(false);
                foreach(GameObject child in Laser){
                    child.SetActive(false);
                }
            }
        }
    }
    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
      
    }


    // Open the menu if open = true
    public void SetMenu(bool open){
        Menu_Opened = open;
        Menu_Opened_Listener = Menu_Opened;
       
    }

    // Closes menu if already open. Opens and updates menu if menu was closed.
    public void SetMenu(){
        Menu_Opened = !Menu_Opened;
        Menu_Opened_Listener = Menu_Opened;
    }
    
    // Lock Robot, if lockRobot = true
    public void LockRobot(bool lockRobot)
    {
        Robot_Locked = lockRobot;
        Robot_Locked_Listener = Robot_Locked;
    }
    // Locks robot if robot was not locked. Unlocks robot if robot was locked before.
    public void LockRobot()
    {
        Robot_Locked = !Robot_Locked;
        Robot_Locked_Listener = Robot_Locked;
    }


    // Show messages to palyer
    // time: the duraton(second) that msg will remain 
    public void SetPlayerText(string msg, float time = 0, bool showOldText = true){
        if(time == 0 ){
            playerText.text = msg; 
            playerTextPanel.SetActive(true);
        }
        else{
            StartCoroutine(PrintInfoForXSeconds(msg,time,showOldText));

        }
    }

    // close the message box
    public void SetPlayerText(){
        playerText.text = "";
        playerTextPanel.SetActive(false);
    }

    IEnumerator PrintInfoForXSeconds(string text, float seconds, bool showOldText)
    {
        string oldPlayerText = playerText.text;
        SetPlayerText(text);
        yield return new WaitForSecondsRealtime(seconds);

        if(showOldText) 
            SetPlayerText(oldPlayerText);
        else
        {
            SetPlayerText();
        }       
    }
    
    // Updates camera images and loads new image data on buttons. 
    // Need CameraAutoLoader.cs
    // has not been tested. This function is copied directly from the origin project -Jingyi
    public void SetCamera(MenuMaker CameraContent){
        cc.updateCams();
        StartCoroutine(Load(CameraContent));
        //CameraGUI.SetActive(true);
    }

    IEnumerator Load(MenuMaker CameraContent)
    {
        yield return new WaitWhile(() => !cc.finishedUpdating);

        Debug.Log("VRInput.cs: Starting to render updated camera Images");

        foreach(string camera in cc.getDictionary().Keys)
        {
            Debug.Log("VRInput.cs: Load : " + camera);
            GameObject button = GameObject.Find("/camera360/right/image_raw/compressedButton");
            CameraContent.loadTexture(camera);
        }

        SetPlayerText("Cameras Updated.");
           
        yield return new WaitForSeconds(2);
        SetPlayerText();
        
    }
    
     // Depending on input scrolls menu up or down.
    public void Scroll(string direction, ScrollRect CameraScrollView){
        if(direction == "down")
        {
            if(CameraScrollView.verticalNormalizedPosition >=0.03)
            {
                CameraScrollView.verticalNormalizedPosition-=0.05f;
                Debug.Log("VRInput.cs: Scroll down ");
            }
        }
        else if(direction == "up")
        {
            if(CameraScrollView.verticalNormalizedPosition <=0.98)
            {
                CameraScrollView.verticalNormalizedPosition+=0.05f;
                Debug.Log("VRInput.cs: Scroll up ");
            }   
        }
    }


    //SteamVR.Interactionsystem.Samples SelektonUIOptions
    public void SetHandSelekton(RenderModelChangerUI prefabsLeft,RenderModelChangerUI prefabsRight)
    {
        for (int handIndex = 0; handIndex < Player.instance.hands.Length; handIndex++)
        {
            Hand hand = Player.instance.hands[handIndex];
            if (hand != null)
            {
                if (hand.handType == SteamVR_Input_Sources.RightHand)
                    hand.SetRenderModel(prefabsRight.rightPrefab);
                if (hand.handType == SteamVR_Input_Sources.LeftHand)
                    hand.SetRenderModel(prefabsLeft.leftPrefab);
            }
        }
    }

    public void ShowController()
    {
        for (int handIndex = 0; handIndex < Player.instance.hands.Length; handIndex++)
        {
            Hand hand = Player.instance.hands[handIndex];
            if (hand != null)
            {
                hand.ShowController(true);
            }
        }
    }

    public void HideController()
    {
        for (int handIndex = 0; handIndex < Player.instance.hands.Length; handIndex++)
        {
            Hand hand = Player.instance.hands[handIndex];
            if (hand != null)
            {
                hand.HideController(true);
            }
        }
    }
}
