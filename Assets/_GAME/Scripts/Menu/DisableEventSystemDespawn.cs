using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableEventSystemDespawn : MonoBehaviour
{
    public static DisableEventSystemDespawn instance;

    private bool initDontDestroyOnLoad = false;

    private void OnEnable()
    {
        if (!initDontDestroyOnLoad)
        {
            DontDestroyOnLoad(this.gameObject);
            initDontDestroyOnLoad = true;
        }

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
