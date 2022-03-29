using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    private float _timeElapsed;
    public float timeElapsed { get => _timeElapsed; private set => _timeElapsed = value; }
    private bool canCountTime;

    public void StartCount()
    {
        canCountTime = true;
        _timeElapsed = 0;
    }

    private void FixedUpdate()
    {
        if (canCountTime)
            _timeElapsed += Time.fixedDeltaTime;
    }
}
