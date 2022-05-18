/* ControllerHints.cs
 * author: Ayumi Bischoff
 * modified: Jingyi
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

// Class that manages activation/ deactivation of controller hints.
// Attached to GameObject ButtonHints (Scene: Simulation)

public class ControllerHints : MonoBehaviour
{
    public SteamVR_ActionSet hints;
    public SteamVR_Action_Boolean hintsOnOff;
    public SteamVR_Action_Boolean moveUp;
    public SteamVR_Action_Boolean moveDown;
    public SteamVR_Action_Boolean turnLeft;
    public SteamVR_Action_Boolean turnRight;
    public SteamVR_Action_Boolean teleport;
    public SteamVR_Action_Boolean menu;
    public SteamVR_Action_Vector2 robotDirection;
    public SteamVR_Action_Single moveRobot;
    SteamVR_Action_Boolean unlock = SteamVR_Input.GetBooleanAction("Lock");
    SteamVR_Action_Boolean scrollUp = SteamVR_Input.GetBooleanAction("TouchUp");
    SteamVR_Action_Boolean scrollDown = SteamVR_Input.GetBooleanAction("TouchDown");
    SteamVR_Action_Boolean interactUI = SteamVR_Input.GetBooleanAction("InteractUI");
    SteamVR_Action_Boolean grab = SteamVR_Input.GetBooleanAction("GrabGrip");
    public Hand leftHand;
    public Hand rightHand;
    VRInput input;

    private bool isShowingHints;

    // Start is called before the first frame update.
    // Add hint button event and menu button event. 
    void Start()
    {
        input = GameObject.Find("Input").GetComponent<VRInput>();
        hintsOnOff.onStateDown += HintsOnOff_onStateDown;
        menu.onStateDown += Menu_onStateDown;
        hints.Activate();
        isShowingHints = false;
    }

    // State change of beeing in the menu or not triggers the hint button hint.
    private void Menu_onStateDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        ControllerButtonHints.ShowTextHint(leftHand, hintsOnOff, "Hints");
        isShowingHints = false;
    }

    // If the hint button is pressed the hints for the environment are shown or there is no hint except for the hint button hint.
    // Environtments are: the player is in the menu or the menu is closed.
    private void HintsOnOff_onStateDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (!isShowingHints)
        {
            if (InteractionManagement.Instance.Menu_Opened)
            {
                isShowingHints = ShowMenuHints();
            }
            else
            {
                isShowingHints = ShowSimHints();
            }
        }
        else
        {
                ControllerButtonHints.HideAllTextHints(leftHand);
                ControllerButtonHints.HideAllTextHints(rightHand);
                isShowingHints = false;
        }
    }

    // Update is called once per frame.
    // Clears current hints if the player changes environments.
    void Update()
    {
        if (InteractionManagement.Instance.Menu_Opened)
        {
            DeactivateSimHints();
        }
        else
        {
            DeactivateMenuHints();
        }

        if (!ControllerButtonHints.IsButtonHintActive(leftHand, hintsOnOff) && !isShowingHints)
        {
            ControllerButtonHints.ShowTextHint(leftHand, hintsOnOff, "Hints");
        }
    }

    public void setIsShowingHints(bool isShowing)
    {
        isShowingHints = isShowing;
    }

    // Shows all hints that advice the user how to control the player and robot.
    private bool ShowSimHints()
    {
        ControllerButtonHints.HideTextHint(leftHand, hintsOnOff);
        
        ControllerButtonHints.ShowButtonHint(leftHand, moveDown);
        ControllerButtonHints.ShowButtonHint(leftHand, turnLeft);
        ControllerButtonHints.ShowButtonHint(leftHand, turnRight);
        ControllerButtonHints.ShowTextHint(leftHand, teleport, "Teleport");
        ControllerButtonHints.ShowTextHint(leftHand, menu, "Menu");

           
        if(input.CurrentMode.name == "Handle"){
            ControllerButtonHints.ShowTextHint(leftHand, moveUp, "Up/Down/Left/Right");
            ControllerButtonHints.ShowTextHint(rightHand, robotDirection, "Robot Direction");
            ControllerButtonHints.ShowTextHint(rightHand, moveRobot, "Move Robot");
            ControllerButtonHints.ShowTextHint(rightHand, unlock, "(Un-)Lock Robot");
        }
        if(input.CurrentMode.name == "Lab"){
            ControllerButtonHints.ShowTextHint(rightHand, grab, "Pick up things");
            ControllerButtonHints.ShowTextHint(leftHand, grab, "Pick up things");
        }  
        if(input.CurrentMode.name == "Remote"){
            ControllerButtonHints.ShowTextHint(leftHand, moveUp, "Up/Down/Left/Right");
            ControllerButtonHints.ShowTextHint(rightHand, interactUI, "Set Destination");
            ControllerButtonHints.ShowTextHint(rightHand, unlock, "(Un-)Lock Robot");
            ControllerButtonHints.ShowTextHint(leftHand, menu, "Menu");
        }
        if(input.CurrentMode.name == "UI"){
            ControllerButtonHints.ShowTextHint(leftHand, moveUp, "Up/Down/Left/Right");
            ControllerButtonHints.ShowTextHint(rightHand, interactUI, "Select");
        }
        return true;
    }

    // Deactivates simulation hints.
    private void DeactivateSimHints()
    {
        ControllerButtonHints.HideTextHint(leftHand, moveUp);
        ControllerButtonHints.HideButtonHint(leftHand, moveDown);
        ControllerButtonHints.HideButtonHint(leftHand, turnLeft);
        ControllerButtonHints.HideButtonHint(leftHand, turnRight);
        ControllerButtonHints.HideTextHint(leftHand, teleport);
        if (!isShowingHints) ControllerButtonHints.HideTextHint(leftHand, menu);
        ControllerButtonHints.HideTextHint(rightHand, unlock);
        ControllerButtonHints.HideTextHint(rightHand, robotDirection);
        ControllerButtonHints.HideTextHint(rightHand, moveRobot);
    }

    // Shows all hints that advice the player to use the menu.
    private bool ShowMenuHints()
    {
        ControllerButtonHints.ShowTextHint(rightHand, interactUI, "Select");
        ControllerButtonHints.ShowTextHint(rightHand, scrollUp, "Scroll");
        ControllerButtonHints.ShowTextHint(leftHand, menu, "Close menu");
        ControllerButtonHints.ShowButtonHint(rightHand, scrollDown);
        ControllerButtonHints.HideTextHint(leftHand, hintsOnOff);
        return true;
    }

    // Deactivates menu hints.
    private void DeactivateMenuHints()
    {
        ControllerButtonHints.HideTextHint(rightHand, interactUI);
        ControllerButtonHints.HideTextHint(rightHand, scrollUp);
        if (!isShowingHints) ControllerButtonHints.HideTextHint(leftHand, menu);
        ControllerButtonHints.HideButtonHint(rightHand, scrollDown);
    }
}
