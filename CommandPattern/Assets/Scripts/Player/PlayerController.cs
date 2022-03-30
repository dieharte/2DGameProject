using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script will be instantiated in a prefab
public class PlayerController : MonoBehaviour
{
    #region EDITOR VARIABLES
    [SerializeField]
    PlayerInputSO inputSO;
    [SerializeField]
    Rigidbody2D rb;
    #endregion

    public Rigidbody2D rigidbody => rb;
    private PlayerState _playerState;
    public PlayerState playerState;
    private Dictionary<PlayerStateType, PlayerState> _playerStates;

    private void Awake()
    {
        inputSO.SetController(this);

        // After all init done: Maybe even a callback
        // set RB for states
        InitStates();
    }

    private void InitStates()
    {
        _playerStates = new Dictionary<PlayerStateType, PlayerState>();
        _playerStates.Add(PlayerStateType.Ground, new PS_Ground(rb, inputSO));
        _playerStates.Add(PlayerStateType.Replay, new PS_Replay(rb, inputSO));
        _playerState = _playerStates[PlayerStateType.Ground];
    }

    bool update2;
    private void Update()
    {
        update2 = !update2;
        if(update2 && rb.velocity != Vector2.zero)
            Debug.Log("VEL " + rb.velocity);

        _playerState.MyUpdate();
    }

    private void FixedUpdate()
    {
        _playerState.MyFixedUpdate();
    }

    private void LateUpdate()
    {
        _playerState.MyLateUpdate();
    }

    public void HandleInput(Vector2 input)
    {
        _playerState.HandleInput(input);
    }

    public void ChangeState(PlayerStateType type)
    {
        if (_playerState == _playerStates[type])
        {
            Debug.LogWarning("CHANGING STATE BUT IT THE SAME STATE");
            return;
        }

        _playerState.OnExitState();
        _playerState = _playerStates[type];
        _playerState.OnEnterState();
    }

    public void ChangeState(PlayerStateType type, params object[] args)
    {
        if (_playerState == _playerStates[type])
        {
            Debug.LogWarning("CHANGING STATE BUT IT THE SAME STATE");
            return;
        }

        _playerState.OnExitState();
        _playerState = _playerStates[type];
        _playerState.Prepare(args);
        _playerState.OnEnterState();
    }

    // TODO: Maybe add a Idle State because if the replay ends exactly a in part that is not
    // a ground, it will be able to move as ground in air.
    public void ChangeIdleState()
    {
        _playerState.OnExitState();
        _playerState = _playerStates[PlayerStateType.Ground];
        _playerState.OnEnterState();
    }

    /// <summary>
    /// Set Player to idle position when start a replay or new level
    /// </summary>
    public void SetIdle()
    {
        rb.velocity = Vector2.zero;
    }
}
