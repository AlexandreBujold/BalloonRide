using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupReceiver : MonoBehaviour
{
    public Transform pickupTransform;
    public Pickup m_pickup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Pickup pickup = collision.gameObject.GetComponentInChildren<Pickup>();
        if (null != pickup)
        {
            Pickup(pickup);
        }
    }

    public void Pickup(Pickup newPickup)
    {
        Rigidbody2D rb = newPickup.gameObject.GetComponentInChildren<Rigidbody2D>();
        if (null != rb)
        {
            rb.isKinematic = true;
        }

        BoxCollider2D box2D = newPickup.gameObject.GetComponentInChildren<BoxCollider2D>();
        if (null != box2D)
        {
            box2D.enabled = false;
        }

        SetPickup(newPickup);
        SetPickupPosition(newPickup);
    }

    public void Drop()
    {
        if (null != m_pickup)
        {
            m_pickup.gameObject.transform.SetParent(null);
            Rigidbody2D rb = m_pickup.gameObject.GetComponentInChildren<Rigidbody2D>();
            if (null != rb)
            {
                rb.isKinematic = false;
            }

            BoxCollider2D box2D = m_pickup.gameObject.GetComponentInChildren<BoxCollider2D>();
            if (null != box2D)
            {
                box2D.enabled = false;
            }
            SetPickup(null);
        }
    }

    public void SetPickupPosition(Pickup newPickup)
    {
        if (newPickup != null)
        {
            newPickup.gameObject.transform.position = pickupTransform.position;
            newPickup.gameObject.transform.rotation = pickupTransform.rotation;
            newPickup.gameObject.transform.SetParent(pickupTransform);
        }
    }

    public void SetPickup(Pickup newPickup)
    {
        m_pickup = newPickup;
    }
}
