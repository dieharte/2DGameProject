using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "ScriptableObjects/Player/PlayerInputSO", order = 1)]
public class PlayerInputSO : ScriptableObject
{
    enum ReplayType
    {
        Input,
        Position,
    }

    #region PLAYER VALUE SETTINGS
    public float maxVelX = 5;
    public float minVelX = 1;
    public float inputSensetiveX = 1f;
    #endregion

    PlayerController _playerController;
    public PlayerController playerController { get => _playerController; private set => _playerController = value; }

    #region REPLAY
    List<InputCommand> commandsHistoric;
    Vector3 _initialPosition;

    TimeCounter _timeCounter;
    ReplayType _replayType;
    #endregion

    private bool isPlayingReplay => _playerController.playerState is PS_Replay;
    //private bool isPlayingReplay => _playerController.playerState.GetType() == typeof(PS_Replay);

    private void OnEnable()
    {
        commandsHistoric = new List<InputCommand>();
        _replayType = ReplayType.Position;
    }

    public void SetController(PlayerController playerController)
    {
        _playerController = playerController;
    }

    public void SetTimeCounter(TimeCounter timeCounter)
    {
        _timeCounter = timeCounter;
        // TODO: Set the start in other place
        _timeCounter.StartCount();
    }

    public void Move(Vector2 input, float timeElapsed)
    {
        // Is this a optmization?
        // Probably depend of the calculation done after this
        if (input == Vector2.zero || isPlayingReplay) return;

        Vector2 inputCalculated = new Vector2(input.x * inputSensetiveX, input.y);
        //Debug.Log("INPUT " + inputCalculated);
        if (commandsHistoric.Count == 0)
            _initialPosition = _playerController.rigidbody.transform.position;
        if(_replayType == ReplayType.Input)
            SaveHistoric(inputCalculated, timeElapsed);
        
        // maybe create a interface/connector for input that transform the input
        // depending of the state of the character
        _playerController.HandleInput(inputCalculated);
    }


    /// <summary>
    /// Save historic based on replay type
    /// </summary>
    /// <param name="input">Position or input value</param>
    public void SaveHistoric(Vector2 input)
    {
        if (isPlayingReplay) return;

        if(_replayType == ReplayType.Position)
        {
            if(commandsHistoric.Count == 0 || commandsHistoric[commandsHistoric.Count-1].input != input)
                commandsHistoric.Add(new InputCommand(input, _timeCounter.timeElapsed));
        }
        else if (_replayType == ReplayType.Input)
        {
            commandsHistoric.Add(new InputCommand(input, _timeCounter.timeElapsed));
        }
    }

    /// <summary>
    /// Save historic input
    /// </summary>
    /// <param name="input"></param>
    /// <param name="timeElapsed"></param>
    public void SaveHistoric(Vector2 input, float timeElapsed)
    {
        commandsHistoric.Add(new InputCommand(input, _timeCounter.timeElapsed));
    }

    public void StartReplay()
    {
        _playerController.ChangeState(PlayerStateType.Replay, commandsHistoric,
            new Vector3Class(_initialPosition) );
    }
}
