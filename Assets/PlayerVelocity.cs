using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVelocity : MonoBehaviour
{
    private Vector3 _curPos;
    private Vector3 _prevPos;
    private Vector3 _velocity;

    public Vector3 Velocity => _velocity;

    void Start()
    {
        _prevPos = transform.position;
    }

    void FixedUpdate()
    {
        _curPos = transform.position;    
        _velocity = (_curPos - _prevPos) / Time.fixedDeltaTime;
        _prevPos = _curPos;
    }
}
