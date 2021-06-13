using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] [Range(0.01f, 0.99f)] float cameraMovespeed;
    
    Vector2 BottomLeftBoundary = new Vector2(-2.97f, -5.2f);
    Vector2 TopRightBoundary   = new Vector2( 2.97f, 5.95f) ;


    const float CAMERA_Z_POSITION = -10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // So it's only once per frame
    void LateUpdate()
    {
        Vector2 newDistance = Vector2.Lerp(transform.position, target.transform.position, cameraMovespeed);

        if (newDistance.x < BottomLeftBoundary.x)
            newDistance = new Vector2(BottomLeftBoundary.x, newDistance.y);
        if (newDistance.y < BottomLeftBoundary.y)
            newDistance = new Vector2(newDistance.x, BottomLeftBoundary.y);


        if (newDistance.x > TopRightBoundary.x)
            newDistance = new Vector2(TopRightBoundary.x  , newDistance.y);
        if (newDistance.y > TopRightBoundary.y)
            newDistance = new Vector2(newDistance.x  , TopRightBoundary.y);

        transform.position = new Vector3(newDistance.x, newDistance.y, CAMERA_Z_POSITION); 
    }
}
