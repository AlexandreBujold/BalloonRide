using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;            // The position that that camera will be following.
    public float smoothing = 5f;        // The speed with which the camera will be following.
    
    Vector3 offset;                     // The initial offset from the target.

    public bool callInFixedUpdate;
    public bool constrainLowestYPosition;
    public bool autoSetLowYOnAwake = false;
    public float lowY;                            // The lowest point the camera will travel.
    
    private void Awake() 
    {
        // Calculate the initial offset.
        offset = transform.position - target.position;

        if (target != null)
        {
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        }

        //calculate the starting Y position
        if (autoSetLowYOnAwake)
        {
            lowY = transform.position.y;
        }
    }

    void Start ()
    {
        
    }
    
    void FixedUpdate ()
    {
        if (callInFixedUpdate)
            MoveCamera();
    }

    void Update()
    {
        if (!callInFixedUpdate)
            MoveCamera();
    }

    void MoveCamera()
    {
        // Create a postion the camera is aiming for based on the offset from the target.
        if (target != null) {
            Vector3 targetCamPos = target.position + offset;

            // Smoothly interpolate between the camera's current position and it's target position.
            transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
            if (constrainLowestYPosition)
            {
                if (transform.position.y < lowY)
                {
                    transform.position = new Vector3 (transform.position.x, lowY, transform.position.z);
                }
            }
        }
    }

}