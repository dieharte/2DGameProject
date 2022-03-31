using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// During replay, the movement input are disabled. But you can control the time of it
/// </summary>
public class PS_Replay : PlayerState
{
    List<InputCommand> _commandsHistoric;
    Vector3 _initialPosition;

    float timeElapsed;

    public PS_Replay(Rigidbody2D rb, PlayerInputSO inputSO, PlayerController controller) : base(rb, inputSO, controller) { }

    public override void Prepare(params object[] args)
    {
        _commandsHistoric = args[0] as List<InputCommand>;
        _initialPosition = (args[1] as Vector3Class).value;        
    }

    public override void OnEnterState()
    {
        if (_commandsHistoric.Count == 0)
        {
            _controller.ChangeIdleState();
            return;
        }

        _controller.SetIdle();
        _controller.rigidbody.transform.position = _initialPosition;
        timeElapsed = 0;
    }

    private void End()
    {
        _controller.ChangeIdleState();
    }


    public override void MyFixedUpdate()
    {
        timeElapsed += Time.fixedDeltaTime;

        while (_commandsHistoric[0].time <= timeElapsed)
        {
            InputCommand cmd = _commandsHistoric[0];

            //_playerController.Move(cmd.input);
            _controller.transform.position = cmd.input;

            _commandsHistoric.RemoveAt(0);

            if (_commandsHistoric.Count == 0)
            {
                End();
                return;
            }
        }
    }
}
