using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://medium.com/game-dev-journal/youre-grounded-a-quick-guide-to-writing-a-modular-ground-check-tool-in-unity2d-b04412ac838d

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private Transform[] _sensors;
    [SerializeField] private LayerMask _groundLayer;

    [SerializeField] private float _groundCheckDistance;
    [SerializeField] private Color _groundHit;
    [SerializeField] private Color _groundMiss;

    public bool isGrounded { private set; get; }

    private void OnEnable()
    {
        // Ground check will be enabled only in Player States that need to check ground
        // Like Air. When enter, it will enable this script and need to reset the value
        isGrounded = false;
    }

    // By Unity call ordere, OnEnable is always called before FixedUpdate, so its safe to not use code bellow
    //public void Init()
    //{
    //    isGrounded = false;
    //}

    private void FixedUpdate()
    {
        isGrounded = RaycastFromAllSensors();
    }

    private bool RaycastFromSensor(Transform sensor)
    {
        RaycastHit2D hit;
        var position = sensor.position;
        var foward = sensor.forward;
        //hit = Physics2D.Raycast(position, foward, _groundCheckDistance, _groundLayer);
        hit = Physics2D.Raycast(position, Vector3.down, _groundCheckDistance, _groundLayer);

        if( hit.collider != null)
        {
            Debug.DrawLine(position, foward * _groundCheckDistance, _groundHit);
            return true;
        }
        else
            Debug.DrawLine(position, foward * _groundCheckDistance, _groundMiss);

        return false;
    }

    private bool RaycastFromAllSensors()
    {
        foreach (var sensor in _sensors)
            if (RaycastFromSensor(sensor)) return true;

        return false;
    }
}
