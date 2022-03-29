using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerReplay : MonoBehaviour
{
    [SerializeField]
    PlayerInputSO inputSO;

    [SerializeField]
    PlayerController _playerController;
    List<InputCommand> _commandsHistoric;
    Vector3 _initialPosition;

    bool playingReplay;
    float timeElapsed;

    private void Awake()
    {
        inputSO.SetControllerReplay(this);
    }

    public void SetCommandHistoric(List<InputCommand> commandsHistoric, Vector3 initialPosition)
    {
        _commandsHistoric = commandsHistoric;
        _initialPosition = initialPosition;
    }

    public void Play()
    {
        if (_commandsHistoric.Count == 0) return;

        _playerController.SetIdle();
        _playerController.rigidbody.transform.position = _initialPosition;
        playingReplay = true;
        timeElapsed = 0;
    }

    private void End()
    {
        playingReplay = false;
        inputSO.EndReplay();
    }

    private void FixedUpdate()
    {
        if (playingReplay)
        {
            timeElapsed += Time.fixedDeltaTime;

            while (_commandsHistoric[0].time <= timeElapsed)
            {
                InputCommand cmd = _commandsHistoric[0];

                //_playerController.Move(cmd.input);
                _playerController.transform.position = cmd.input;

                _commandsHistoric.RemoveAt(0);

                if (_commandsHistoric.Count == 0)
                {
                    End();
                    return;
                }
            }
        }
    }


}
