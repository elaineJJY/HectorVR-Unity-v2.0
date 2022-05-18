/* ActivateCameraScript.cs
 * author: Lydia Ebbinghaus, Jana-Sophie Schönfeld, Trung-Hoa Ha
 */
using System.Collections.Generic;
using UnityEngine;
using RosSharp.RosBridgeClient;
using UnityEngine.UI;

// Script that activates/deletes and shows/hides a camera when selected with a laser pointer.
// Attached to child of Button (Camera Scroll Menu > Scrollview > Viewport > Content).
public class ActivateCameraScript : MonoBehaviour
{
    Dictionary<string,ImageSubscriberMod> imageSubscribers;
    GameObject cameraPlane;
    Color oldColor;

    // Creating dictonary to store and access current image subscribers.
    public void Start()
    {
        imageSubscribers = new Dictionary<string, ImageSubscriberMod>();
    }

    // Reacts on pushed camera button in menu.
    // If button was already selected: deactivate and hide camera.
    // If button was not selected: activate and show camera.
    public void ActivateDeactivateCamera()
    {
        
        // Getting name of selected camera.
        string cameraName = transform.parent.GetChild(0).GetComponentInChildren<TMPro.TextMeshProUGUI>().name;
        if(cameraName == null){
            Debug.Log("ActivateCameraScript.cs: No name was found for the camera. Check if script is attached to the button as a child.");
        }
        GameObject parentCamera = GameObject.Find("Automatic Cameras");
        if(parentCamera == null){
            Debug.Log("ActivateCameraScript.cs: Automatic Cameras object was not found. Check if Automatic Cameras has the correct name.");
        }
        Transform[] trs = parentCamera.GetComponentsInChildren<Transform>(true);
        
        // Searching in automatic camers for selected camera.
        foreach(Transform t in trs){
            if(t.name == cameraName){
                Debug.Log("ActivateCameraScript.cs: found camera" + t.name + cameraName);
                cameraPlane = t.gameObject;
            }
        }

        if (cameraPlane != null){
            if (cameraPlane.activeSelf == true){
                DeactivateCamera();
            }
            else
            {
                ActivateCamera();
            }
        }
        else
        {
            Debug.LogError("ActivateCameraScript.cs: Camera " + cameraName + " not found. Can't activate/deactivate. Check if name of camera object is the same as text of button.");
        }
    }

    // Activates camera and shows at defined position.
    private void ActivateCamera()
    {
        GameObject cam = GameObject.Find("VRCamera");
        cameraPlane.SetActive(true);

        // Choosing location where camera should appear.
        cameraPlane.transform.rotation = cam.transform.rotation;

        cameraPlane.transform.Rotate(new Vector3(0,180,0));
        // Use this instead to use a plane.
        //cameraPlane.transform.Rotate(new Vector3(90,180,0));

        cameraPlane.transform.position = cam.transform.position + cam.transform.forward * 0.5f;

        // Adds specific image subscriber for camera.
        var currentImageSubscriber = GameObject.Find("RosBridge").AddComponent<ImageSubscriberMod>();
        currentImageSubscriber.meshRenderer = cameraPlane.GetComponent<MeshRenderer>();
        currentImageSubscriber.Topic = cameraPlane.name;

        // Higlights selected camera in menu.
        this.transform.parent.GetChild(2).GetComponent<RawImage>().color=Color.green;

        imageSubscribers.Add(cameraPlane.name,currentImageSubscriber);
    }

    // Deactivates camera.
    private void DeactivateCamera()
    {
        // Hides camera and deletes higlight in menu.
        cameraPlane.SetActive(false);
        this.transform.parent.GetChild(2).GetComponent<RawImage>().color=Color.white;

        // Destroys image subscriber.
        // Otherwise huge lags, which cause small fps. Contributes to VR-sickness.
        Destroy(imageSubscribers[cameraPlane.name]);
        imageSubscribers.Remove(cameraPlane.name);
    }
}
