using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BalloonGame.Balloon.Locomotion
{
    public class BalloonLocomotion : MonoBehaviour
    {
        [Header("Movement Configuration")]
        [SerializeField] private Vector2 velocity;
        [SerializeField] private MovementAxisConfiguration xAxis;
        [SerializeField] private MovementAxisConfiguration yAxis;

        [Header("References")]
        public Rigidbody2D m_rigidbody2D;

        // Start is called before the first frame update
        void Start()
        {
            //base.Setup();
            if (m_rigidbody2D == null)
            {
                m_rigidbody2D = GetComponentInParent<Rigidbody2D>();
            }
        }

        public void MoveBalloon(Vector2 input)
        {
            Vector2 movement = CalculateMovement(input);

            //velocity = new Vector2(Mathf.Abs(newVelocity.x) * calcDirection.x, Mathf.Abs(newVelocity.y) * calcDirection.y);
            if (m_rigidbody2D != null)
            {
                //m_rigidbody2D.velocity = new Vector2 (movement.x == 0 ? m_rigidbody2D.velocity.x)
                //m_rigidbody2D.MovePosition(m_rigidbody2D.position + (movement * Time.deltaTime));
                if (movement.magnitude != 0)
                {
                    m_rigidbody2D.AddForce(movement, ForceMode2D.Force);
                }
            }
        }

        public Vector2 CalculateMovement(Vector2 input)
        {
            Vector2 calcDirection = new Vector2((input.x >= 0 ? 1 : -1), (input.y >= 0 ? 1 : -1));
            velocity = new Vector2(input.x != 0 ? AccelerateInAxis(calcDirection.x, xAxis.maxVelocity, xAxis.accelerationTime, ref velocity.x) :
                DecelerateInAxis(xAxis.maxVelocity, xAxis.decelerationTime, ref velocity.x), input.y != 0 ?
                AccelerateInAxis(calcDirection.y, yAxis.maxVelocity, yAxis.accelerationTime, ref velocity.y) :
                DecelerateInAxis(yAxis.maxVelocity, yAxis.decelerationTime, ref velocity.y));

            return velocity;
        }

        public void ApplyBalloonImpulse(Vector2 direction, float strength)
        {
            Vector2 impulse = direction * strength;

            if (m_rigidbody2D != null)
            {
                m_rigidbody2D.AddForce(impulse, ForceMode2D.Impulse);
            }
        }

        [System.Serializable]
        public struct MovementAxisConfiguration
        {
            public float maxVelocity;
            public float accelerationTime;
            public float decelerationTime;
            private AnimationCurve curve;
        }

        #region Acceleration / Deceleration

        public float AccelerateInAxis(float input, float maxVelocity, float accelerationTime, ref float target)
        {
            float acceleration = (maxVelocity / accelerationTime) * Time.deltaTime;
            float newVelocity = Mathf.Clamp(target + (acceleration * Mathf.Sign(input)), -maxVelocity, maxVelocity);
            //Min Velocity
            if (Mathf.Abs(newVelocity) < 0.25f)
            {
                newVelocity = 0.25f * Mathf.Sign(input);
            }
            return newVelocity;
        }

        public float DecelerateInAxis(float maxVelocity, float decelerationTime, ref float target)
        {
            if (target == 0)
            {
                return 0;
            }

            //Calculate Deceleration
            float deceleration = (maxVelocity / decelerationTime) * Time.deltaTime;
            //Apply the deceleration to the current velocity in the opposite direction that the input is
            float newVelocity = Mathf.Clamp(target + (deceleration * -Mathf.Sign(target)), -maxVelocity, maxVelocity);

            //If decelerating past 0, set velocity to 0. 
            if (Mathf.Sign(newVelocity) == -Mathf.Sign(target)) //If input is 0, this will cause an instant deceleration in negative direction (since 0's sign is positive)
            {
                newVelocity = 0;
            }

            return newVelocity;
        }

        #endregion
    } 
}
