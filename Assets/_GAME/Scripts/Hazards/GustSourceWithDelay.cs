using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BalloonGame.GustHazard
{
    public class GustSourceWithDelay : GustSource
    {
        [Header("Gust Coroutine")]
        [SerializeField] private IEnumerator gustCoroutine;

        [Space]
        [Header("Gust Delay Timer")]
        [SerializeField] private float delay;

        [Space]
        [Header("Coroutine Bool")]
        [SerializeField] private bool delayCoroutineStarted = false;

        [Space]
        [Header("Gust Functionality Bool")]
        [SerializeField] private bool gustEnabled = false;

        protected override void OnEnable()
        {
            InitializeDirection(m_facing);
            gustDelegate = GustConsistent;
            gustCoroutine = GustDelay(delay);
            if (!delayCoroutineStarted)
            {
                Debug.Log("Started");
                StartCoroutine(gustCoroutine);
                delayCoroutineStarted = true;
            }
        }

        protected override void Update()
        {
            if (gustEnabled)
            {
                base.Update();
            }
        }

        private IEnumerator GustDelay(float waitTime)
        {
            while (true)
            {
                Debug.Log("Delay");
                yield return new WaitForSeconds(waitTime);
                Debug.Log("Enable");
                gustEnabled = true;
                yield return new WaitForSeconds(waitTime);
                Debug.Log("Disable");
                gustEnabled = false;
            }
        }
    }
}
