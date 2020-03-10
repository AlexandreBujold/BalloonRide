using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BalloonGame.Input;
using BalloonGame.Balloon.Locomotion;


public class BalloonController : MonoBehaviour //Input, Art / Animation, Effects, UI, Resources Usage
{
    [Header("Config & References")]
    private InputData inputData;
    public BalloonLocomotion m_balloonLocomotion;

    // Start is called before the first frame update
    void Start()
    {
        inputData = InputController.Instance.GetInputData(InputController.PlayerControl.Player1);

        if (m_balloonLocomotion == null)
        {
            m_balloonLocomotion = GetComponentInChildren<BalloonLocomotion>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = Vector2.zero;
        if (inputData != null)
        {
            input = new Vector2(inputData.horizontalInput.value, inputData.verticalInput.value);
        }

        if (m_balloonLocomotion != null)
        {
            m_balloonLocomotion.MoveBalloon(input);
        }
    }
}


