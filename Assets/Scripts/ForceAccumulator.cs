using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SimpleRigidboody2D))]
public class ForceAccumulator : MonoBehaviour
{
    [SerializeField] private Vector2 force;
    [SerializeField] private float torque;
    [SerializeField] private SimpleForceMode mode;

    private SimpleRigidboody2D _simpleRigidboody2D;
    
    private void Awake()
    {
        _simpleRigidboody2D = GetComponent<SimpleRigidboody2D>();
    }

    private void FixedUpdate()
    {
        _simpleRigidboody2D.AddForce(force, mode);
        _simpleRigidboody2D.AddTorque(torque, mode);
        RefreshEnableState();
    }

    private void OnValidate()
    {
        enabled = true;
    }

    private void RefreshEnableState()
    {
        enabled = mode == SimpleForceMode.Force || mode == SimpleForceMode.Acceleration;
    }
}
