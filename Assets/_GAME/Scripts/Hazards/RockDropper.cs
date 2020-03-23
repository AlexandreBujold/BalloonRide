using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BalloonGame.RockDropper
{
    public class RockDropper : MonoBehaviour
    {
        [Header("Gust Coroutine")]
        [SerializeField] private IEnumerator rockCoroutine;

        [Space]
        [Header("Rock Drop Object")]
        [SerializeField] private GameObject rock;

        [Space]
        [Header("Rock Drop Delay")]
        [SerializeField] private float delay = 3f;

        private bool rockDropStarted = false;

        private void OnEnable()
        {
            rock = Resources.Load<GameObject>("Rock");
            rockCoroutine = DropRockCoroutine(delay);
            if(!rockDropStarted)
            {
                StartCoroutine(rockCoroutine);
                rockDropStarted = true;
            }
        }

        private IEnumerator DropRockCoroutine(float waitTime)
        {
            while(true)
            {
                yield return new WaitForSeconds(waitTime);
                DropRock();
                Debug.Log("Drop rock");
            }
        }

        private void DropRock()
        {
            Instantiate(rock, transform.position, Quaternion.identity);
        }
    }
}
