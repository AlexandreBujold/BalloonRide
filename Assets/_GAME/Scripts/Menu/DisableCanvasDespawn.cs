using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCanvasDespawn : MonoBehaviour
{
    private bool instanceInit = false;

    private void OnEnable()
    {
        if (!instanceInit)
        {
            DontDestroyOnLoad(this.gameObject);
            instanceInit = true;
            gameObject.SetActive(false);
        }
    }
}
