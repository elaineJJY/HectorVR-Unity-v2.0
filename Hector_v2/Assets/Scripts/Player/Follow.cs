/*
 * Enabling the gameObject to follow the target
 *
 * Author: Jingyi Jia
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject Target;
    public float offset = 0;
    public bool update = false;
    public float MaxDistance = 1;
    public int MaxAngle = 360;

    // Start is called before the first frame update
    private void Awake() {
        if(Target == null){
           Debug.Log("Set Default Target.");
           Target = GameObject.FindGameObjectWithTag("MainCamera");
        }
        
    }
    void OnEnable()
    {
        transform.position = Target.transform.position +  new Vector3(0, 0, Target.transform.forward.z * offset);
        changeDirection(); 
    }

    // Update is called once per frame
    
    void Update(){
        
        if(update && Target!=null){
            follow();
        }
       
    }

    public void changeDirection(){
        Vector3 direction = Target.transform.eulerAngles;
        direction.x = 0;
        direction.z = 0;
        transform.eulerAngles = direction;
    }

    public void follow(){
        Vector3 pos = Target.transform.position + new Vector3(0, 0, Target.transform.forward.z * offset);
        float distance = Vector3.Distance(pos, transform.position);
        float angle = Quaternion.Angle(transform.rotation, Target.transform.rotation);

        if (distance > MaxDistance || angle > MaxAngle)
        {   
            transform.position = Target.transform.position +  new Vector3(0, 0, Target.transform.forward.z * offset);
            //changeDirection(); 
        }
        
        
    }


}
