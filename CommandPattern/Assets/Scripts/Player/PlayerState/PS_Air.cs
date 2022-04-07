using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS_Air : PlayerState
{
    GroundCheck _groundCheck;
    bool _perfomedDash;
    Vector2 _input;
    float dashForce = 50f;

    /// <summary>
    /// Indicate how many frames the player have to dash using >> (foward foward) input
    /// </summary>
    readonly int framesToActiveDash = 30;
    int framesCountToDash;
    bool isCountingToDash;
    /// <summary>
    /// Indicate that start to analyze the first input > to perform a dash (you are in a 'start
    /// state" to perform dash)
    /// 0 is equal false, != 0 is the direction
    /// </summary>
    //bool canCountToDash;
    int canCountToDash;
    bool movedOneTime;


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
        //Debug.Log(string.Format(" dash {0} - frames to dash {1}", _perfomedDash, framesCountToDash));
        if (!_perfomedDash && framesCountToDash >= 0 && canCountToDash != 0)
        {
            framesCountToDash++;
            if(framesCountToDash > framesToActiveDash)
            {
                canCountToDash = 0;
                framesCountToDash = -1;
                Debug.Log("cancel dash COUNT");
            } else if (_input.x == 0)
            {
                Debug.Log("Moved once");
                movedOneTime = true;
            }
        }

        if (_groundCheck.isGrounded)
            _controller.ChangeState(PlayerStateType.Ground);
        //Debug.Log(_input);

        //if(hadInput)
        //    HandleMovement(_input);

        if (!hadInput)
        {
            Debug.Log("NO Input");
            _input = Vector2.zero;
            lastInputZero = true;
        }
        hadInput = false;
    }

    public override void OnEnterState()
    {
        _groundCheck.enabled = true;
        _perfomedDash = false;
        isCountingToDash = false;
        canCountToDash = 0;
        movedOneTime = false;
        framesCountToDash = -1;
    }

    public override void OnExitState()
    {
        _groundCheck.enabled = false;
    }

    public override void HandleInput(Vector2 input)
    {
        //lastInputZero = _input.x != 0 ? false : true;
        // input value cant be used to calculate dash because
        // if the player got a boost in speed, this value will be modified as well.
        if (!_perfomedDash)
        {
            Debug.Log(string.Format("Frames {0} = last input {1}", framesCountToDash, _input));
            //if(_input.x == 0 || _input.x != input.x)
            if (lastInputZero && input.x != 0)
            {
                canCountToDash = input.x > 0 ? 1 : -1;
                framesCountToDash = 0;
                Debug.Log("2 - canCountToDash ");
            }

            //if (isCountingToDash && input.x != 0 && framesCountToDash > 5 )
            if (movedOneTime && input.x != 0)
            {
                bool wentSameDirection = input.x > 0 && canCountToDash == 1 ||
                    input.x < 0 && canCountToDash == -1;

                if (wentSameDirection)
                {
                    Debug.Log("4 - DASHHHH " + _input);
                    _perfomedDash = true;
                    _input = input;
                    input *= dashForce;
                    _rb.velocity = new Vector2(_rb.velocity.x, 2);
                }else
                {
                    isCountingToDash = false;
                    canCountToDash = 0;
                }

            }
            //else if (canCountToDash && input.x != 0)
            //else if (canCountToDash && lastInputZero && input.x != 0)
            else if (movedOneTime && canCountToDash != 0 && input.x != 0)
            {
                Debug.Log("3 - COUNT DASH " + input);
                //framesCountToDash = 0;
                isCountingToDash = true;
                _input = input;
            }
            else
            {
                _input = input;
            }
        }

        //_input = input;

        // Move
        input = CalculateInput(input);
        HandleMovement(input);
        hadInput = true;
    }


    bool lastInputZero;
    bool hadInput;
    public override void Prepare(params object[] args)
    {
        _input = (args[0] as Vector2Class).value;
        Debug.Log("1 - FIRST input " + _input);
        lastInputZero = _input.x != 0 ? false : true;
        hadInput = lastInputZero;
        //if (lastInputZero)
        //    canCountToDash = true;
    }

    private Vector2 CalculateInput(Vector2 input)
    {
        // Can fall more quickier?
        if (input.y > 0)
        {
            return new Vector2(input.x, 0);
        }
        else
            return input;
    }
}
