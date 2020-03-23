using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BalloonGame.Input;
using BalloonGame.Balloon.Locomotion;
using BalloonGame.Balloon.Physics;


public class BalloonController : MonoBehaviour //Input, Art / Animation, Effects, UI, Resources Usage
{
    [Header("Config & References")]
    private InputData inputData;
    public BalloonLocomotion m_balloonLocomotion;
    public BalloonPhysicsController m_balloonPhysicsController;

    // Start is called before the first frame update
    void Start()
    {
        inputData = InputController.Instance.GetInputData(InputController.PlayerControl.Player1);

        m_balloonLocomotion = m_balloonLocomotion == null ? GetComponentInChildren<BalloonLocomotion>() : m_balloonLocomotion;
        m_balloonPhysicsController = m_balloonPhysicsController == null ? GetComponentInChildren<BalloonPhysicsController>() : m_balloonPhysicsController;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = Vector2.zero;
        if (null != inputData)
        {
            input = new Vector2(inputData.horizontalInput.value, inputData.verticalInput.value);
        }

        if (null != m_balloonLocomotion)
        {
            m_balloonLocomotion.MoveBalloon(input);
        }
    }
}


