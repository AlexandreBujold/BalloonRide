using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BalloonGame.Input;
using BalloonGame.Balloon.Locomotion;
using BalloonGame.Balloon.Physics;
using BalloonGame.Balloon.Fuel;


public class BalloonController : MonoBehaviour //Input, Art / Animation, Effects, UI, Resources Usage
{
    [Header("Config & References")]
    private InputData inputData;
    public BalloonLocomotion m_balloonLocomotion;
    public BalloonPhysicsController m_balloonPhysicsController;
    public BalloonFuel m_balloonFuel;

    // Start is called before the first frame update
    void Start()
    {
        inputData = InputController.Instance.GetInputData(InputController.PlayerControl.Player1);

        m_balloonLocomotion = m_balloonLocomotion == null ? GetComponentInChildren<BalloonLocomotion>() : m_balloonLocomotion;
        m_balloonPhysicsController = m_balloonPhysicsController == null ? GetComponentInChildren<BalloonPhysicsController>() : m_balloonPhysicsController;
        m_balloonFuel = m_balloonFuel == null ? GetComponentInChildren<BalloonFuel>() : m_balloonFuel;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = Vector2.zero;
        if (null != inputData)
        {
            input = new Vector2(inputData.horizontalInput.value, inputData.verticalInput.value);
        }


        if (0 < input.y)
        {
            if (null != m_balloonFuel)
            {
                if (true != m_balloonFuel.UseFuel()) //If no fuel
                {
                    input.y = 0;
                }
            }
        }
        else
        {
            if (null != m_balloonFuel)
            {
                m_balloonFuel.inUse = false;
            }
        }

        if (null != m_balloonLocomotion)
        {
            m_balloonLocomotion.MoveBalloon(input);
        }
    }
}


