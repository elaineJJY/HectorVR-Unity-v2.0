/* SceneHandler.cs
 * author: Lydia Ebbinghaus
 */
using UnityEngine;
using UnityEngine.UI;
using System;

// Class that handles interactions made with a modified SteamVR laser pointer.
// Attached to GameObject Camera Scroll Menu (> LeftHand > SteamVRObjects > Player) in the hierachy.
public class SceneHandler : MonoBehaviour
{
    public SteamVR_LaserPointerMod laserPointer;
    public ScrollRect scrollRect;
    private PointerEventArgs klicked;

    // Just for tesing.
    public bool klick;

    void Awake()
    {
        laserPointer.PointerIn += PointerInside;
        laserPointer.PointerOut += PointerOutside;
        laserPointer.PointerClick += PointerClick;

        // Experimental: Enable to change realization of scrolling to : scrolling with laser pointer.
        // Needs to be improved. If enabled, disable scrolling in VRInput.

        //laserPointer.PointerDown += PointerScrollDown;
        //laserPointer.PointerUp += PointerScrollUp;
    }

    // If a pointerklick event was triggered and target was a button. Activate buttons script.
    // In this case call ActivateDeactivateCamera.
    public void PointerClick(object sender, PointerEventArgs e)
    {
        try
        {
            // !Test
            //e.target.GetComponent<Button>().onClick.Invoke();
            e.target.GetComponent<Button>().ButtonDown.Invoke();
            klicked =e;
            klick = true;
        }
        catch(NullReferenceException)
        {
            klick = false;
            Debug.Log("SceneHandler.cs: No camera to choose.");
        }
    }

    // Actual scrolling in script : VRInput, just Experimental.
    // Needs to be modified if scrolling should be realized with laser pointer or any other way than a button.
    // Scroll up in menu.
    public void PointerScrollUp(object sender, PointerEventArgs e)
    {
       if(scrollRect.verticalNormalizedPosition <=0.9f)
        {
            scrollRect.verticalNormalizedPosition+=0.1f;
            Debug.Log("SceneHandler.cs: Scroll up "+ e.target.name);
        }    
    }

    // Actual scrolling in script : VRInput, just Experimental.
    // Needs to be modified if scrolling should be realized with laser pointer or any other way than a button.
    // Scroll down in menu.
    public void PointerScrollDown(object sender, PointerEventArgs e)
    {
        if(scrollRect.verticalNormalizedPosition >=0.1)
        {
            scrollRect.verticalNormalizedPosition-=0.1f;
            Debug.Log("SceneHandler.cs: Scroll down "+ e.target.name);
        }
    }

    public void PointerInside(object sender, PointerEventArgs e)
    {
       // Debug.Log("Object was entered");
    }

    public void PointerOutside(object sender, PointerEventArgs e)
    {
       // Debug.Log("Object was exited");
    }

    public PointerEventArgs getLaserClick()
    {
        return klicked;
    }
}