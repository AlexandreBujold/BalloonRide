using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BalloonGame;

namespace BalloonGame.Balloon.Physics
{
    public class BalloonPhysicsController : RaycastController2D //Responsible for adjusting physics and locomotion depending on situation
    {
        [Header("Physics Control References")]
        //BalloonPhysicsController detects if a collision occurs at 
        public BalloonController m_balloonController;
        public Rigidbody2D m_balloonRigidbody;
        private bool grounded = false;
        private float lastGravityScale = 0.05f;

        // Start is called before the first frame update
        void Start()
        {
            base.Setup();
            m_balloonController = m_balloonController == null ? GetComponentInParent<BalloonController>() : m_balloonController;
            if (m_balloonController != null && m_balloonRigidbody == null)
            {
                if (m_balloonController.m_balloonLocomotion != null)
                {
                    m_balloonRigidbody = m_balloonRigidbody == null ? m_balloonController.m_balloonLocomotion.m_rigidbody2D : m_balloonRigidbody;
                }
            }
        }

        private void FixedUpdate()
        {
            //This is scuffed to all hell, don't rely on this RaycastController2D LULW
            grounded = IsGrounded();
            if (grounded)
            {
                StopVerticalVelocity();
            }
        }

        public void StopVerticalVelocity()
        {
            if (true == grounded && null != m_balloonRigidbody)
            {
                if (m_balloonRigidbody.velocity.y < 0)
                {
                    m_balloonRigidbody.velocity = new Vector2(m_balloonRigidbody.velocity.x, 0);
                    if (m_balloonRigidbody.gravityScale > 0)
                    {
                        lastGravityScale = m_balloonRigidbody.gravityScale;
                    }
                    m_balloonRigidbody.gravityScale = 0;
                }
                else
                {
                    m_balloonRigidbody.gravityScale = lastGravityScale;
                }
            }
        }

        public bool IsGrounded()
        {
            RaycastDown();
            return hittingDown == true ? true : false;
        }

        private void OnDrawGizmos()
        {
            IsGrounded();
        }
    } 
}
