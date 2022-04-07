using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS_Ground : PlayerState
{
    /// <summary>
    /// If player is not moving, the first movement have a initial speed movement.
    /// If player changes the direction of movement, the player will gradually decrease speed (like sonic)
    /// </summary>
    bool firstFrameMove;

    public PS_Ground(Rigidbody2D rb, PlayerInputSO inputSO, PlayerController controller) : base(rb, inputSO, controller) { }

    bool stopMovingInX => Mathf.Abs(_rb.velocity.x) < Mathf.Epsilon;


    public override void MyLateUpdate()
    {
        if (Mathf.Abs(_rb.velocity.x) > _inputSO.maxVelX)
            _rb.velocity = new Vector2(_rb.velocity.x > 0 ? _inputSO.maxVelX : -_inputSO.maxVelX, _rb.velocity.y);
        else if (firstFrameMove && _rb.velocity != Vector2.zero && Mathf.Abs(_rb.velocity.x) < _inputSO.minVelX)
        {
            firstFrameMove = false;
            _rb.velocity = new Vector2(_rb.velocity.x > 0 ? _inputSO.minVelX : -_inputSO.minVelX, _rb.velocity.y);
        }

        _inputSO.SaveHistoric(_rb.transform.position);
    }

    public override void OnEnterState()
    {
        firstFrameMove = false;
    }

    public override void HandleInput(Vector2 input)
    {
        // Move
        input = CalculateJumpForce(input);
        HandleMovement(input);

        if (stopMovingInX)
            firstFrameMove = true;
    }

    private Vector2 CalculateJumpForce(Vector2 input)
    {
        // TODO: Need to calculate on Ground? Because this state doen't already mean
        // that i am on ground?

        //if (input.y == 1 && onGround)
        if (input.y > 0)
        {
            _controller.ChangeState(PlayerStateType.Air, new Vector2Class(input) );
            // LIMITATION: By Unity Input System, if you just jump, the value in y input could be up to 1,
            // while if you jump during walking, the value could be up to 0.7
            return new Vector2(input.x, 45 * input.y);
            
        }
        else
            return input;
    }
}
