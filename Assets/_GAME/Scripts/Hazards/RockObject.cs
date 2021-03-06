﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BalloonGame.Balloon.Locomotion;
using BalloonGame.Balloon.Health;

public class RockObject : MonoBehaviour
{
    private float timer;

    private void OnEnable()
    {
        timer = 0f;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject);

        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Balloon"))
        {
            if (collision.gameObject.GetComponentInChildren<BalloonLocomotion>())
            {
                BalloonLocomotion targetScript = collision.gameObject.GetComponentInChildren<BalloonLocomotion>();
                BalloonHealth targetHealthScript = collision.gameObject.GetComponentInChildren<BalloonHealth>();

                Vector2 impulseDirection = (targetScript.transform.position - transform.position).normalized;
                targetScript.ApplyBalloonImpulse(impulseDirection, 3f);
                targetHealthScript.Damage(1);
            }
        }
        Destroy(this.gameObject);
    }
}
