using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Rigidbody2D _rb;
    protected PlayerInputSO _inputSO;

    public PlayerState(Rigidbody2D rb, PlayerInputSO inputSO)
    {
        _rb = rb;
        _inputSO = inputSO;
    }

    public virtual void OnEnterState()
    {

    }

    public virtual void OnExitState()
    {

    }

    public virtual void MyUpdate()
    {
        
    }

    public virtual void MyFixedUpdate()
    {

    }

    public virtual void MyLateUpdate()
    {

    }


    public virtual void HandleInput(Vector2 input)
    {

    }
}
