/* MenuMaker.cs
 * author: Trung-Hoa Ha & Lydia Ebbinghaus.
 */
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Class that creates a menu, containing buttons with camera images and corresponding name of camera.
// Attached to Content (LeftHand > Camera Scroll Menu > Scroll View > Viewport > Content.
public class MenuMaker : MonoBehaviour
{    
    public GameObject buttonPrefab;
    public GameObject ParentOfCamera;
    public Transform buttonContainer;
    public Transform firstButtonPosition;
    public MeshRenderer meshRenderer;
    private CameraCreator cameraCreator ;
    private Texture2D texture2D;
    private bool retrievedCameras;
    private GameObject button;
    private Dictionary<string,GameObject> buttons;

    void Start()
    {
        if(buttonPrefab == null || ParentOfCamera == null || buttonContainer == null || firstButtonPosition == null || meshRenderer == null){
            Debug.Log("MenuMaker: Public properties of MenuMaker script are not set. Please set them or menu won't work properly");
        }
        buttons = new Dictionary<string, GameObject>();
        cameraCreator = GameObject.Find("VRCamera").GetComponent<CameraCreator>();
    }

    // If camera images are received. Loads example images (current image) on a corresponding button. Renames button and creates a description of camera.
    void Update()
    {
        if ((retrievedCameras != true) && (cameraCreator.topicsSorted))
        {
            int cameranumber = ParentOfCamera.transform.childCount;            
            for (int i = 0; i < cameranumber; i++)
            {
                // Creates button with camera image and name for a specific camera.
                button = Instantiate(buttonPrefab, firstButtonPosition.position + new Vector3(0,-i,0), Quaternion.identity, buttonContainer);
                string cameraName = ParentOfCamera.transform.GetChild(i).name;
                button.name = cameraName +"Button";
                button.transform.GetChild(0).GetComponentInChildren<TMPro.TextMeshProUGUI>().text = cameraName.Split('/')[1] + " " + cameraName.Split('/')[2];
                button.transform.GetChild(0).GetComponentInChildren<TMPro.TextMeshProUGUI>().name = cameraName;
                loadTexture(cameraName);

                // Image does not fit properly in canvas. Wrong orientation.
                // Place and orientate button correctly in menu.
                button.transform.rotation = button.transform.parent.rotation;
                button.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,0);
                button.transform.localPosition = new Vector3(this.transform.localPosition.x,this.transform.localPosition.y,0);

                buttons.Add(button.name,button);
                Debug.Log(cameraName + " added to menu.");
            }

            Debug.Log("Menu: Images loaded to menu.");

            if (cameranumber > 0 ){
                Debug.Log("cameras should be set");
                retrievedCameras = true;
            }    
        }
    }

    // Loads imaga data that is associated on a button for a camera with name :cameraName.
    public void loadTexture(string cameraName)
    {
        if(retrievedCameras){
            string name = cameraName+"Button";
            button = buttons[name];
        }
       
        texture2D = new Texture2D(1, 1);
        bool isLoaded = texture2D.LoadImage(cameraCreator.getImageData(cameraName));
        
        if (isLoaded)
        {
            button.transform.GetChild(2).GetComponent<RawImage>().texture = texture2D;
            Debug.Log("Texture set" + button);
        }
    }
}
