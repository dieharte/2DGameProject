using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS_Air : PlayerState
{
    GroundCheck _groundCheck;

    public PS_Air(Rigidbody2D rb, PlayerInputSO inputSO, PlayerController controller,
        GroundCheck groundCheck ) 
        : base(rb, inputSO, controller) {
        _groundCheck = groundCheck;
    }

    public override void MyLateUpdate()
    {
        if (Mathf.Abs(_rb.velocity.x) > _inputSO.maxVelX)
            _rb.velocity = new Vector2(_rb.velocity.x > 0 ? _inputSO.maxVelX : -_inputSO.maxVelX, _rb.velocity.y);

        _inputSO.SaveHistoric(_rb.transform.position);
    }

    public override void MyFixedUpdate()
    {
        if (_groundCheck.isGrounded)
            _controller.ChangeState(PlayerStateType.Ground);
    }

    public override void OnEnterState()
    {
        _groundCheck.enabled = true;
    }

    public override void OnExitState()
    {
        _groundCheck.enabled = false;
    }

    public override void HandleInput(Vector2 input)
    {
        // Move
        input = CalculateInput(input);
        _rb.AddForce(input, ForceMode2D.Force);
    }

    private Vector2 CalculateInput(Vector2 input)
    {
        if (input.y > 0)
        {
            return new Vector2(input.x, 0);
        }
        else
            return input;
    }
}
