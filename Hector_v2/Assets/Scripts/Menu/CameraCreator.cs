/*
 * author: Jana-Sophie Schönfeld, Lydia Ebbinghaus
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Valve.VR.InteractionSystem;
using Valve.VR.InteractionSystem.Sample;
using RosSharp.RosBridgeClient;

// Attached to GameObject VRCamera (> SteamVRObjects > Player) in the hierachy.
// Class that creates object on which image data of real cameras is rendered, sort out not used cameras and updates camera images in menu.
// It is possible to attach cameras to a parent see line 121.

public class CameraCreator : MonoBehaviour
{
    private bool gotTopics = false;
    public bool topicsSorted = false;
    //private GameObject allCameras;
    public string[] topics;
    public bool finishedUpdating;
    public GameObject cameraParent;
    public GameObject cameraPlane;
    private MeshRenderer mesh;

    // Dictonarys of camernames and corresponding image data.
    Dictionary<string, byte[]> dict;
    Dictionary<string,byte[]> update;

    // Start is called before the first frame update.
    void Start()
    {
        if (cameraParent == null || cameraPlane == null) Debug.LogError("CameraCreator.cs: Prefab missing. Please head over to the inspector and make sure Prefabs are dragged into cameraParent and cameraPlane. Script attached to VRCamera > SteamVRObjects > Player");
    }

    // Update is called once per frame.
    // As soon as it got the camera topics from the RosBridge CreateCams() is called.
    // Calling CreateCams() is a one time action.
    void Update()
    {
        if (!gotTopics)
        {
            topics = GameObject.Find("RosBridge").GetComponent<CameraServiceSubscriber>().topics;
            dict = new Dictionary<string, byte[]>();

            if (topics.Length > 0)
            {
                SortOutTopics();
                gotTopics = !gotTopics;
                Destroy(GameObject.Find("RosBridge").GetComponent<CameraServiceSubscriber>());
            }
        }
    }

    // Called from VRInput if user opens menu again. Updates camera images in menu. Calls UpdateCameraImages to get image date of one camera.
    public void updateCams()
    {
        update = new Dictionary<string, byte[]>();
        finishedUpdating = false;
        var lastElement = false;
        var index = 0;

        foreach(string currentCamera in dict.Keys)
        {
            var currentImageSubscriber = GameObject.Find("RosBridge").AddComponent<ImageSubscriberMod>();
            currentImageSubscriber.stopProcessing = true;

            currentImageSubscriber.Topic = currentCamera;
            if(index == dict.Count-1) lastElement=true;
            index++;
            StartCoroutine(UpdateCameraImages(currentImageSubscriber,currentCamera,lastElement));
        }
    }

    // Waits 5 seconds, gets current camera image and stores name and corresponding imaga data in a dictonary.
    // last = true, if currentCamera is the last one to be updated.
    IEnumerator UpdateCameraImages(ImageSubscriberMod currentImageSubscriber, string currentCamera, bool last)
    {
        yield return new WaitForSeconds(5);

        if(!currentImageSubscriber.oneMessageReceived)
        {
            Debug.Log("CameraCreator.cs: Topic: "+ currentImageSubscriber.Topic + " not published");
        }
        else
        {
            byte[] imageData = currentImageSubscriber.imageData;
            update.Add(currentCamera,imageData);
        }
            
        Destroy(currentImageSubscriber);

        if(last)
        {
            dict = update;
            Debug.Log("CameraCreator.cs: Camera Images Updated");
            finishedUpdating=true;
        }
    }

    // Sorts out non publishing topics and writes publishing ones in the directionary.
    public void SortOutTopics()
    {
        Debug.Log("CameraCreator.cs: Sorting out not used Cameras.");
        var amountOfCameras = topics.Length;

        for (int i = 0; i < amountOfCameras; i++)
        {
            // Object image will be loading on.
            var currentCamera = Instantiate(cameraPlane,this.transform.position,this.transform.rotation);
            currentCamera.transform.SetParent(cameraParent.transform);
            
            // Making plane an interactable object, so it can be dragged.
            currentCamera.AddComponent<Interactable>();
            var interactableExp = currentCamera.AddComponent<InteractableExampleMod>();
            interactableExp.camera = this.transform;

            // Enable to attach cameras to a parent, like VRCamera.
            //currentCamera.transform.SetParent(cameraParent.transform);

            // Loading images onto object.
            var currentImageSubscriber = GameObject.Find("RosBridge").AddComponent<ImageSubscriberMod>();
            currentImageSubscriber.meshRenderer = currentCamera.GetComponent<MeshRenderer>();
            currentImageSubscriber.Topic = topics[i];
            
            currentCamera.SetActive(false);
            currentCamera.name=topics[i];

            // Checks if currentCamera is publishing any image Data.
            StartCoroutine(CheckCamera(i,currentImageSubscriber,currentCamera,amountOfCameras));
        }
    }

    // Checks if currentCamera is publishing any image data and destroys image subsciber afterwards (improves runtime).
    // i = index of currentCamera in diconary. 
    IEnumerator CheckCamera(int i, ImageSubscriberMod currentImageSubscriber, GameObject currentCamera,int amountOfCameras)
    {
        yield return new WaitForSecondsRealtime(5);
        
        Debug.Log("CameraCreator.cs: Loading Camera image of: "+ topics[i]);
            
        if(!currentImageSubscriber.oneMessageReceived)
        {
            Debug.Log("CameraCreator.cs: Topic: "+ topics[i] + " not published");
            Destroy(currentCamera); 

        }else
        {
            string topic = topics[i];
            byte[] imageData = currentImageSubscriber.imageData;

            try
            {
                dict.Add(topic, imageData);
            }
            catch (ArgumentException)
            {
                Debug.LogError("CameraCreator.cs: An element with Key = \"txt\" already exists. Duplicate of camera exists");
            }
        }
        // If currentCamera == last camera all cameras that are not publishing any data are sorted out.
        if(i == (amountOfCameras-1))
        {
            topicsSorted = !topicsSorted;
        }
        
        Destroy(currentImageSubscriber);
    }

    // Returns image data of camera with name : cameraName.
    public byte[] getImageData(string cameraName)
    {
        try
        {
            return dict[cameraName];
        }
        catch(KeyNotFoundException)
        {
            Debug.LogError("CameraCreator.cs: Trying to access camera image that does not exist.");
        }

        return null;
    }

    // Returns dictonary that contains camera names and corresponding image data.
    public Dictionary<string, byte[]> getDictionary()
    {
        return dict;
    }
}