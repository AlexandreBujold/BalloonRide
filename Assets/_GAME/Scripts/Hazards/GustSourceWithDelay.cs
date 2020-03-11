using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GustSourceWithDelay : GustSource
{
    [Header("Gust Coroutine")]
    [SerializeField] private IEnumerator gustCoroutine;

    [Space]
    [Header("Gust Delay")]
    [SerializeField] private float delay;

    [Space]
    [Header("Coroutine Bool")]
    [SerializeField] private bool delayCoroutineStarted = false;

    protected override void OnEnable()
    {
        InitializeDirection(m_facing);
        gustDelegate = GustDelayed;
        gustCoroutine = Gust(delay);
    }

    private void GustDelayed()
    {
        if(!delayCoroutineStarted)
        {
            Debug.Log("Started");
            StartCoroutine(gustCoroutine);
            delayCoroutineStarted = true;
        }
    }

    private IEnumerator Gust(float waitTime)
    {
        while(true)
        {
            Debug.Log("Delay");
            yield return new WaitForSeconds(waitTime);
            Debug.Log()
        }
    }
}
