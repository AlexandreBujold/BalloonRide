using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BalloonGame.Balloon.Locomotion;
using BalloonGame.Balloon.Health;

public class Spikes : MonoBehaviour
{
    [Header("Impulse Force")]
    [SerializeField] private float impulseForce = 3f
        ;
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
    }
}
