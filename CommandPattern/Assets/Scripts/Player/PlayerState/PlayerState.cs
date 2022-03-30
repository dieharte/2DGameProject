using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// How to handle 'empty' states? It will have?
// What about transition of states? Can I call it anywhere? Or should be in a 'late frame'?
public class PlayerState
{
    protected Rigidbody2D _rb;
    protected PlayerInputSO _inputSO;

    public PlayerState(Rigidbody2D rb, PlayerInputSO inputSO)
    {
        _rb = rb;
        _inputSO = inputSO;
    }


    public virtual void Prepare(params object[] args) { 
    
    }

    public virtual void OnEnterState()
    {
        // What about transition of states?
        // Can I call it anywhere? Or should be in a 'late frame'?
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

public enum PlayerStateType
{
    Ground,
    Replay,
    Air,
    // Idle
}

public class Vector3Class
{
    public Vector3 value;
}