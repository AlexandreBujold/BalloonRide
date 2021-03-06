﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BalloonGame.GustHazard
{
    public class GustSource : MonoBehaviour
    {
        protected enum Direction
        {
            NULL,
            RIGHT,
            LEFT,
            UP,
            DOWN
        }

        [Header("Wind Gust Direction")]
        [SerializeField] protected Direction m_facing;

        protected delegate void GustDel();

        protected GustDel gustDelegate;

        [Space]
        [Header("LayerMask")]
        [SerializeField] protected LayerMask layerFilter;

        [Space]
        [Header("Gimbal Object")]
        [SerializeField] protected GameObject visualGimbal;

        [Space]
        [Header("Wind Particle")]
        [SerializeField] protected ParticleSystem windParticle;

        [Space]
        [Header("Float Variables")]
        [SerializeField] protected float capsuleSize = 5f;
        [SerializeField] protected float gustStrength;

        protected RaycastHit2D results;

        protected virtual void OnEnable()
        {
            visualGimbal = transform.GetChild(0).transform.gameObject;
            windParticle = visualGimbal.transform.GetComponentInChildren<ParticleSystem>();

            InitializeDirection(m_facing);
            gustDelegate = GustConsistent;
        }

        protected virtual void Update()
        {
            gustDelegate();
        }

        /**<summary>
         * GunConsistent is the functionality for the gust hazard. A very simple switch statement that changes the direction of a 
         * capsule cast. The capsule cast only interacts with the balloon and pushes it based on the gust strength.
         * GustSourceWithDelay.cs uses this function to do the same thing
         * </summary>
         */
        protected void GustConsistent()
        {
            switch(m_facing)
            {
                case Direction.LEFT:
                    if (results = Physics2D.CapsuleCast(transform.position, new Vector2(capsuleSize, capsuleSize), CapsuleDirection2D.Horizontal, 0, Vector2.left, Mathf.Infinity, layerFilter))
                    {
                        if (results.transform.GetComponentInChildren<BalloonController>())
                        {
                            BalloonController controller = results.transform.GetComponentInChildren<BalloonController>();
                            controller.m_balloonLocomotion.MoveBalloon(Vector2.left * gustStrength);
                            Debug.Log(results.collider);
                        }
                    }
                    Debug.DrawLine(transform.position, transform.position - new Vector3(3, 0, 0), Color.red);
                    break;

                case Direction.UP:
                    if (results = Physics2D.CapsuleCast(transform.position, new Vector2(capsuleSize, capsuleSize), CapsuleDirection2D.Vertical, 0, Vector2.up, Mathf.Infinity, layerFilter))
                    {
                        if (results.transform.GetComponentInChildren<BalloonController>())
                        {
                            BalloonController controller = results.transform.GetComponentInChildren<BalloonController>();
                            controller.m_balloonLocomotion.MoveBalloon(Vector2.up * gustStrength);
                            Debug.Log(results.collider);
                        }
                    }
                    Debug.DrawLine(transform.position, transform.position + new Vector3(0, 3, 0), Color.red);
                    break;

                case Direction.DOWN:
                    if (results = Physics2D.CapsuleCast(transform.position, new Vector2(capsuleSize, capsuleSize), CapsuleDirection2D.Vertical, 0, Vector2.down, Mathf.Infinity,layerFilter))
                    {
                        if (results.transform.GetComponentInChildren<BalloonController>())
                        {
                            BalloonController controller = results.transform.GetComponentInChildren<BalloonController>();
                            controller.m_balloonLocomotion.MoveBalloon(Vector2.down * gustStrength);
                            Debug.Log(results.collider);
                        }
                    }
                    Debug.DrawLine(transform.position, transform.position - new Vector3(0, 3, 0), Color.red);
                    break;

                case Direction.RIGHT:
                    if (results = Physics2D.CapsuleCast(transform.position, new Vector2(capsuleSize, capsuleSize), CapsuleDirection2D.Vertical, 0, Vector2.right, Mathf.Infinity, layerFilter))
                    {
                        if (results.transform.GetComponentInChildren<BalloonController>())
                        {
                            BalloonController controller = results.transform.GetComponentInChildren<BalloonController>();
                            controller.m_balloonLocomotion.MoveBalloon(Vector2.right * gustStrength);
                            Debug.Log(results.collider);
                        }
                    }
                    Debug.DrawLine(transform.position, transform.position + new Vector3(3, 0, 0), Color.red);
                    break;

                default:
                    Debug.DrawLine(transform.position, transform.position + new Vector3(3, 0, 0), Color.red);
                    break;
            }
        }

        //Initialize a basic direction if no direction is assigned so as to prevent errors
        protected void InitializeDirection(Direction dir)
        {
            if (m_facing == Direction.NULL)
            {
                m_facing = Direction.RIGHT;
            }
            
            if(m_facing == Direction.RIGHT)
            {
                visualGimbal.transform.Rotate(0, 0, -90);
            }
            else if(m_facing == Direction.LEFT)
            {
                visualGimbal.transform.Rotate(0, 0, 90);
            }
            else if(m_facing == Direction.DOWN)
            {
                visualGimbal.transform.Rotate(0, 0, 180);
            }
        }
    }
}
