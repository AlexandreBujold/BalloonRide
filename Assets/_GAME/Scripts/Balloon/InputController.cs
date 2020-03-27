using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BalloonGame.Input
{
    public class InputController : MonoBehaviour
    {
        public static InputController Instance;
        public enum PlayerControl { Player1, Player2 }

        public InputData player1;
        public InputData player2;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else if (Instance.gameObject != this.gameObject)
            {
                Destroy(gameObject);
                Debug.Log("Destroying duplicate of " + this.GetType().ToString());
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            //player1 = new InputData();
            //player2 = new InputData();
        }

        public InputData GetInputData(PlayerControl player)
        {
            return player == PlayerControl.Player1 ? player1 : player2;
        }

        // Update is called once per frame
        void Update()
        {
            if (player1 != null)
            {
                player1.PollInput();
            }
        }
    }

    [System.Serializable]
    public class InputData
    {
        public InputType verticalInput;
        public InputType horizontalInput;
        public InputType fire;
        public InputType secondaryFire;
        public int facingDirection = -1;

        public void PollInput()
        {
            verticalInput.Update(UnityEngine.Input.GetAxis(verticalInput.axis));
            horizontalInput.Update(UnityEngine.Input.GetAxis(horizontalInput.axis));
            fire.Update(UnityEngine.Input.GetAxis(fire.axis));
            secondaryFire.Update(UnityEngine.Input.GetAxis(secondaryFire.axis));

            if (horizontalInput.value != 0)
            {
                facingDirection = horizontalInput.value < 0 ? -1 : 1;
            }
        }

        [System.Serializable]
        public struct InputType
        {
            public string axis;
            public bool pressed;
            public float value;
            public float lastValue;
            public float timePressed;
            public float timeBeingPressed;

            public void SetAxis(string _key)
            {
                axis = _key;
            }

            public void Update(float _pressed)
            {
                lastValue = value;
                value = _pressed;
                pressed = _pressed != 0 ? true : false;

                if (pressed)
                {
                    timePressed = Time.time;
                    timeBeingPressed = Time.time - timePressed;
                }
                else
                {
                    timePressed = 0;
                    timeBeingPressed = 0;
                }
            }
        }
    } 
}

