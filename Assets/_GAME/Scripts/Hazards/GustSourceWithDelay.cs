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
            visualGimbal = transform.GetChild(0).transform.gameObject;
            windParticle = visualGimbal.transform.GetComponentInChildren<ParticleSystem>();

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
            //Does the same as the base class but only if enabled   
            if (gustEnabled)
            {
                base.Update();
            }
        }
        
        //Used to activate the gust functionality on a timed delay. Delay can be changed variably
        private IEnumerator GustDelay(float waitTime)
        {
            while (true)
            {
                Debug.Log("Delay");
                yield return new WaitForSeconds(waitTime);
                Debug.Log("Enable");
                gustEnabled = true;
                windParticle.Play();
                yield return new WaitForSeconds(waitTime);
                Debug.Log("Disable");
                gustEnabled = false;
                windParticle.Stop();
            }
        }
    }
}
