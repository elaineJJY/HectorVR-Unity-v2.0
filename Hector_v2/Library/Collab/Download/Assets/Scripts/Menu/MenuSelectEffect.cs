using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSelectEffect : MonoBehaviour
{
    public Text MenuTitle;
   
    Color32 oldColor = Color.white;
    Color32 ColorModeSelected = new Color32(255, 225, 64, 217);
    private void OnEnable() {

        // Change the color and content of the button in MenuGUI
        if(MenuTitle!=null){
            oldColor = MenuTitle.color;
            MenuTitle.color = ColorModeSelected;
            MenuTitle.fontStyle = FontStyle.Bold;
            MenuTitle.text = MenuTitle.text + " (Now)";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    private void OnDisable() {
        
        // Recover the color and content of Button in MenuGUI
        if(MenuTitle!=null){
            MenuTitle.color = oldColor;
            MenuTitle.fontStyle = FontStyle.Normal;
            MenuTitle.text = gameObject.name;
        }
    }
}
