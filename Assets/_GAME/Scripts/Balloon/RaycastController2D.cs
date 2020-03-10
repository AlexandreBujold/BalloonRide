using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastController2D : MonoBehaviour
{

    [Header("Raycast Settings")]
    public float distanceBetweenRays = 0.2f;
    public const float skinWidth = 0.02f;

    public RaycastOrigins raycastOrigins;
    public LayerMask collisionMask;

    public BoxCollider2D myCollisionCollider;

    [HideInInspector]
    public int horizontalRayCount = 4;
    [HideInInspector]
    public int verticalRayCount = 4;

    [HideInInspector]
    public float horizontalRaySpacing;
    [HideInInspector]
    public float verticalRaySpacing;

    public virtual void Setup()
    {
        if (myCollisionCollider == null)
        {
            myCollisionCollider = GetComponentInChildren<BoxCollider2D>();
        }
        CalculateRaySpacing();
    }

    public void UpdateRaycastOrigins()
    {
        //Shrink bounds inwards based on box colider used for collision detection
        Bounds bounds = myCollisionCollider.bounds;
        bounds.Expand(skinWidth * -2);

        //Assign raycast origins based on new bounds
        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    public void CalculateRaySpacing()
    {
        Bounds bounds = myCollisionCollider.bounds;
        bounds.Expand(skinWidth * -2);

        float boundsWidth = bounds.size.x;
        float boundsHeight = bounds.size.y;

        horizontalRayCount = Mathf.RoundToInt(boundsHeight / distanceBetweenRays);
        verticalRayCount = Mathf.RoundToInt(boundsWidth / distanceBetweenRays);

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        //Calculate the spacing between rays
        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    public struct RaycastOrigins
    {
        //These values are calculated in UpdateRaycastOrigins()
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }
}
