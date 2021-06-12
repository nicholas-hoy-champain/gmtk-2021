using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] [Range(0.01f, 0.99f)] float cameraMovespeed;

    const float CAMERA_Z_POSITION = -10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // So it's only once per frame
    void LateUpdate()
    {
        Vector2 newDistance = Vector2.Lerp(transform.position, target.transform.position, cameraMovespeed);
        transform.position = new Vector3(newDistance.x, newDistance.y, CAMERA_Z_POSITION); 
    }
}
