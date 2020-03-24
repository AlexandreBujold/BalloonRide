using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BalloonGame.Balloon.Fuel;

public class BalloonFuelUI : MonoBehaviour
{
    public Slider fuelSlider;
    public TMPro.TextMeshProUGUI fuelText;
    public BalloonFuel m_balloonFuel;

    // Start is called before the first frame update
    void Start()
    {
        //if (null != fuelSlider)
        //{
        //    if (null != m_balloonFuel)
        //    {
        //        fuelSlider.maxValue = m_balloonFuel.GetMaxFuel();
        //    }
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (null != fuelSlider)
        {
            if (null != m_balloonFuel)
            {
                fuelSlider.value = m_balloonFuel.GetFuel() / m_balloonFuel.GetMaxFuel();

                if (fuelText != null)
                {
                    fuelText.SetText(m_balloonFuel.GetFuel().ToString("F1"));
                }
            }
        }
    }
}
