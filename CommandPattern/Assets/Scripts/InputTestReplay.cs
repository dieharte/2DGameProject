using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTestReplay : MonoBehaviour
{
    [SerializeField]
    PlayerInputSO inputSO;

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 400, 400), "Replay"))
            inputSO.StartReplay();
    }

    private void Start()
    {
        
    }
}
