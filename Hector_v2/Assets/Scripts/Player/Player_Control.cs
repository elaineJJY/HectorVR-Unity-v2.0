/* Player_Control.cs
 * author: Yannic Seidler
 * modified: Jingyi   deleted TeleportingGround controls, change speed
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;
using Valve.VR;

// Handles TeleportingGround controls
// Attached to TeleportingGround
public class Player_Control : MonoBehaviour
{
    public bool upDownEnabled = true;
    public SteamVR_ActionSet defaultActionSet;
    public SteamVR_ActionSet playerControlActionSet;
    public SteamVR_Action_Boolean button_down;
    public SteamVR_Action_Boolean button_up;
    
    // continous motion if true.
    public bool smooth_Y; 
    public float up_down_speed;
    public double percentageOfRadius;

    Player player;
    const double standardSize = 5.0;
    double maxRadius;
    double usedRadius;

    // Start is called before the first frame update.
    void Start()
    {
        InitializeButtons();
        InitializeTeleportRadius(percentageOfRadius);
        Vector3 initPosition = player.feetPositionGuess;
        initPosition.y=0;
        transform.position = initPosition;
    }

    // Calculate the max radius a player is able to move in. If the radius is exceeded, center the plane again with.
    private void InitializeTeleportRadius(double percentageOfRadius)
    {
        player = Player.instance;
        double r1 = standardSize * (double)transform.localScale.x;
        double r2 = standardSize * (double)transform.localScale.z;
        maxRadius = (double)Mathf.Min((float)r1, (float)r2);

        if (0 < percentageOfRadius && percentageOfRadius < 1)
        {
            usedRadius = (double)maxRadius * percentageOfRadius;
        }
        else
        {
            usedRadius = maxRadius;
        }
    }

    // Initialize button actions for y position controls of player.
    private void InitializeButtons()
    {
        button_down.onStateDown += Button_down_onStateDown;
        button_down.onState += Button_down_onState;
        button_down.onStateUp += Button_down_onStateUp;
        button_up.onStateDown += Button_up_onStateDown;
        button_up.onState += Button_up_onState;
        button_up.onStateUp += Button_up_onStateUp;
        playerControlActionSet.Activate();
    }

    private void Button_up_onStateUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        defaultActionSet.Activate();
    }

    private void Button_down_onStateUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        defaultActionSet.Activate();
    }

    private void Button_down_onState(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (smooth_Y  && (player.feetPositionGuess.y)>0 && upDownEnabled)
        {
            defaultActionSet.Deactivate();
            //transform.Translate(Vector3.down * up_down_speed * Time.deltaTime);
            player.trackingOriginTransform.Translate(Vector3.down * up_down_speed * Time.deltaTime);
        }
    }

    private void Button_down_onStateDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (!smooth_Y && (player.feetPositionGuess.y)>0 && upDownEnabled)
        {
            defaultActionSet.Deactivate();
            //transform.Translate(Vector3.down * 2 * up_down_speed * Time.deltaTime);
            player.trackingOriginTransform.Translate(Vector3.down * 2 * up_down_speed * Time.deltaTime);
        }
    }

    private void Button_up_onState(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (smooth_Y && upDownEnabled)
        {
            defaultActionSet.Deactivate();
            player.trackingOriginTransform.Translate(Vector3.up * up_down_speed * Time.deltaTime);
           //transform.Translate(Vector3.up * up_down_speed * Time.deltaTime);
        }
    }

    private void Button_up_onStateDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (!smooth_Y && upDownEnabled)
        {
            defaultActionSet.Deactivate();
            player.trackingOriginTransform.Translate(Vector3.up * 2 * up_down_speed * Time.deltaTime);
            //transform.Translate(Vector3.up * up_down_speed * 2 * Time.deltaTime);
        }
    }

    // Recenter plane
    void Update()
    {   
        //transform.position = player.feetPositionGuess;
        /*
        var ray = new Ray(player.hmdTransform.position, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (gameObject.Equals(hit.transform.gameObject))
            {
                if ((double)Mathf.Abs(Vector3.Distance(transform.position, hit.point)) > usedRadius)
                {   
                    Vector3 move = hit.point - transform.position;
                    move.y = 0;
                    transform.Translate(move);
                }
            }
        }
        else
        {
            Debug.Log("Player_Control.cs: Nothing hit. May occur if percentageOfRadius = 1");
            Vector3 move =player.feetPositionGuess - transform.position;
            move.y = 0;
            transform.Translate(move);
        }
        */

        Vector3 move = player.feetPositionGuess - transform.position;
        move.y = 0;
        transform.Translate(move);


    }

    void OnDestroy()
    {
        button_down.onStateDown -= Button_down_onStateDown;
        button_down.onState -= Button_down_onState;
        button_down.onStateUp -= Button_down_onStateUp;
        button_up.onStateDown -= Button_up_onStateDown;
        button_up.onState -= Button_up_onState;
        button_up.onStateUp -= Button_up_onStateUp;
        playerControlActionSet.Deactivate();
    }
}
