using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public float offsetY;

    void LateUpdate ()
    {
        if (target != null) {
            Vector3 targetPoint = target.position;
            targetPoint.y += offsetY;
            //targetPoint.z += 7f;
            transform.position = Vector3.Lerp (transform.position, targetPoint, Time.deltaTime * 5f);
        }
    }
}
