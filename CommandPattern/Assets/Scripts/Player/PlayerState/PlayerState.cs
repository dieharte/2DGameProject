using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// How to handle 'empty' states? It will have?
// What about transition of states? Can I call it anywhere? Or should be in a 'late frame'?
public class PlayerState
{
    protected Rigidbody2D _rb;
    protected PlayerInputSO _inputSO;
    protected PlayerController _controller;

    public PlayerState(Rigidbody2D rb, PlayerInputSO inputSO, PlayerController controller)
    {
        _rb = rb;
        _inputSO = inputSO;
        _controller = controller;
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

    /// <summary>
    /// Get a calculated "handled input" to move the player.
    /// </summary>
    /// <param name="input"></param>
    protected virtual void HandleMovement(Vector2 input)
    {
        _rb.AddForce(input, ForceMode2D.Force);
        if (input.x > 0 && _rb.transform.localScale.x < 0)
            _rb.transform.localScale = new Vector3 (-_rb.transform.localScale.x, _rb.transform.localScale.y, _rb.transform.localScale.z);
        else if (input.x < 0 && _rb.transform.localScale.x > 0)
            _rb.transform.localScale = new Vector3(-_rb.transform.localScale.x, _rb.transform.localScale.y, _rb.transform.localScale.z);
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
    public Vector3Class(Vector3 val) => value = val;
}

public class Vector2Class
{
    public Vector2 value;
    public Vector2Class(Vector2 val) => value = val;
}