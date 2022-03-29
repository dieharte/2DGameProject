using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script will be instantiated in a prefab
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    PlayerInputSO inputSO;

    [SerializeField]
    Rigidbody2D rb;

    public Rigidbody2D rigidbody => rb;
    private bool onGround;
    private PlayerState _playerState;
    private List<PlayerState> _playerStates;

    /// <summary>
    /// If player is not moving, the first movement have a initial speed movement.
    /// If player changes the direction of movement, the player will gradually decrease speed (like sonic)
    /// </summary>
    bool firstFrameMove;
    bool stopMovingInX => Mathf.Abs(rb.velocity.x) < Mathf.Epsilon;

    private void Awake()
    {
        inputSO.SetController(this);
        firstFrameMove = false;


        // After all init done: Maybe even a callback
        // set RB for states
        SetStates();
    }

    private void SetStates()
    {
        _playerState = new PS_Ground(rb, inputSO);
    }

    bool update2;
    private void Update()
    {
        update2 = !update2;
        if(update2 && rb.velocity != Vector2.zero)
            Debug.Log("VEL " + rb.velocity);

        _playerState.MyUpdate();
    }

    private void LateUpdate()
    //private void FixedUpdate()
    {
        _playerState.MyLateUpdate();

        //if (Mathf.Abs(rb.velocity.x) > inputSO.maxVelX)
        //    rb.velocity = new Vector2(rb.velocity.x > 0 ? inputSO.maxVelX : -inputSO.maxVelX, rb.velocity.y);
        //else if (firstFrameMove && rb.velocity != Vector2.zero && Mathf.Abs(rb.velocity.x) < inputSO.minVelX)
        //{
        //    firstFrameMove = false;
        //    rb.velocity = new Vector2(rb.velocity.x > 0 ? inputSO.minVelX : -inputSO.minVelX, rb.velocity.y);
        //}

        //inputSO.SaveHistoric(transform.position);
    }



    public void HandleInput(Vector2 input)
    {
        _playerState.HandleInput(input);
        
        //input = CalculateJumpForce(input);
        //rb.AddForce(input, ForceMode2D.Force);

        //if(stopMovingInX)
        //    firstFrameMove = true;
    }


    private Vector2 CalculateJumpForce(Vector2 input)
    {
        if(input.y == 1 && onGround)
        {
            return new Vector2(input.x, 5);
        }
         else
            return input;
    }


    /// <summary>
    /// Set Player to idle position when start a replay or new level
    /// </summary>
    public void SetIdle()
    {
        rb.velocity = Vector2.zero;
    }
}
