using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BalloonGame.Balloon.Fuel
{
    public class BalloonFuel : MonoBehaviour
    {
        [SerializeField]
        private float fuel = 10;
        [SerializeField]
        private float maxFuel = 100;
        [SerializeField]
        private float depletionRate = 1;
        public bool inUse = false;

        private void Start()
        {
            SetFuel(maxFuel);
        }

        public bool UseFuel()
        {
            if (fuel > 0)
            {
                fuel = Mathf.Clamp(fuel - ( depletionRate * Time.deltaTime) , 0, maxFuel);
                inUse = true;
                return true;
            }
            inUse = false;
            return false;
        }

        public void AddFuel(float amount)
        {
            fuel = Mathf.Clamp(fuel + amount, 0, maxFuel);
        }

        public void SetFuel(float amount)
        {
            fuel = Mathf.Clamp(amount, 0, maxFuel);
        }

        public float GetFuel()
        {
            return fuel;
        }

        public float GetMaxFuel()
        {
            return maxFuel;
        }
    } 
}
