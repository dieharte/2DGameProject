using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct InputCommand 
{
    public float time;
    public Vector2 input;

    public InputCommand(Vector2 input, float time)
    {
        this.time = time;
        this.input = input;
    }
}
