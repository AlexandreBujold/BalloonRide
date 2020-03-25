using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffect : MonoBehaviour
{
    public GameObject effectPrefab;

    public void Spawn()
    {
        if (effectPrefab != null)
        {
            Instantiate(effectPrefab, transform.position, Quaternion.identity, gameObject.transform);
        }
    }
}
