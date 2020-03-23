using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BalloonGame
{
    public class RaycastController2D : MonoBehaviour
    {
        [Header("Raycast Settings")]
        public float distanceBetweenRays = 0.2f;
        public const float skinWidth = 0.02f;
        public float rayDistance = 1f;
        public ContactFilter2D ground;
        public bool hittingDown = false;
        [Space]
        public bool drawGizmos;

        [HideInInspector]
        public RaycastOrigins raycastOrigins;

        public BoxCollider2D myCollisionCollider;

        [HideInInspector]
        public int horizontalRayCount = 4;
        [HideInInspector]
        public int verticalRayCount = 4;

        [HideInInspector]
        public float horizontalRaySpacing;
        [HideInInspector]
        public float verticalRaySpacing;

        private RaycastHit2D[] lastHits;

        public virtual void Setup()
        {
            if (myCollisionCollider == null)
            {
                myCollisionCollider = GetComponentInChildren<BoxCollider2D>();
            }
            CalculateRaySpacing();
        }

        public RaycastHit2D[] RaycastDown()
        {
            if (null != myCollisionCollider)
            {
                UpdateRaycastOrigins();
                CalculateRaySpacing();
            }

            RaycastHit2D[] hits = new RaycastHit2D[1];
            Vector2 rayOrigin = raycastOrigins.bottomLeft;
            for (int i = 0; i <= horizontalRayCount; i++)
            {
                rayOrigin = raycastOrigins.bottomLeft + new Vector2((horizontalRaySpacing * i), 0);
                Vector2 direction = Vector3.down * rayDistance;

                Debug.DrawRay(rayOrigin, direction);

                if (Physics2D.Raycast(rayOrigin, direction, ground, hits, rayDistance) > 0)
                {
                    Debug.DrawRay(rayOrigin, direction, Color.green, 0.1f);
                    hittingDown = true;
                    return hits;
                }
                else
                {
                    hittingDown = false;
                }
            }
            return hits;
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

            horizontalRayCount = Mathf.RoundToInt(boundsWidth / distanceBetweenRays);
            verticalRayCount = Mathf.RoundToInt(boundsHeight / distanceBetweenRays);

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

        private void OnDrawGizmos()
        {
            RaycastDown();
        }
    } 
}
