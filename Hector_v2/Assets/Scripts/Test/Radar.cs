using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if( other.gameObject.layer == LayerMask.NameToLayer("Invisible")){
            foreach(Transform tran in  other.gameObject.GetComponentsInChildren<Transform>()){
			    tran.gameObject.layer = LayerMask.NameToLayer("LiDAR");
		    }
        }
            
    }
}
