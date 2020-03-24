using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BalloonGame.Balloon.Fuel;

namespace BalloonGame.Balloon.Rendering
{
    public class BalloonFuelEffect : MonoBehaviour
    {
        public BalloonFuel m_balloonFuel;
        public ParticleSystem m_particleEffect;
        private ParticleSystem.EmissionModule emissionModule;

        [Space]
        public float particleRateDuringUse = 50f;
        public float particleRateNormal = 5f;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (null != m_balloonFuel)
            {
                if (null != m_particleEffect)
                {
                    emissionModule = m_particleEffect.emission;
                    emissionModule.rateOverTime = m_balloonFuel.inUse == true ? particleRateDuringUse : particleRateNormal;
                }
            }
        }
    } 
}
