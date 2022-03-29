using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField]
    private PlayerInputSO playerSO;

    DefaultInput inputs;
    Vector2 input_movement;

    [SerializeField]
    TimeCounter timeCounter;

    private void Awake()
    {
        // TODO: How to make the input to run multiplayer local
        inputs = new DefaultInput();

        inputs.Player.Movement.performed += e => input_movement = e.ReadValue<Vector2>();
        inputs.Enable();

        playerSO.SetTimeCounter(timeCounter);
    }

    private void FixedUpdate()
    {
        CalculateMovement();
    }

    private void CalculateMovement()
    {
        // Pass it to other class?
        playerSO.Move(input_movement, timeCounter.timeElapsed);
    }
}
