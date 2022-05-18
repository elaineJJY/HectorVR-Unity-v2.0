/*
 * Making the gameobject with 'tagName' as a target, so that it can be shown in the map
 *
 * Author: Jingyi Jia
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// add Target 
namespace RadarComponents
{
    public class TargetCombine : MonoBehaviour
    {
        public string tagName;
        public GameObject originObj;
        GameObject[] targets;
        private void Awake()
        {
           
            
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (tagName != null) {
                targets = GameObject.FindGameObjectsWithTag(tagName);
                if (targets.Length != 0)
                {
                    AddTargets();
                    this.enabled = false;
                }
            }
        }

        private void AddTargets(){
            foreach (GameObject targetObj in targets)
            {
                GameObject target = GameObject.Instantiate(originObj,  new Vector3(0, 0, 0), Quaternion.identity, targetObj.transform);
                target.transform.localPosition =  new Vector3(0, 0, 0);
                target.SetActive(true);
            }
        }
    }
}

