using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class GetName : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (text != null)
        {
            text.SetText(gameObject.name);
        }
        else
        {
            text = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        }
    }
}
