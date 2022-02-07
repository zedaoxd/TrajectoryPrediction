using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SimpleRigidboody2D))]
public class ForceAccumulator : MonoBehaviour
{
    [SerializeField] private Vector2 force;
    [SerializeField] private SimpleForceMode mode;

    private SimpleRigidboody2D _simpleRigidboody2D;
    
    private void Awake()
    {
        _simpleRigidboody2D = GetComponent<SimpleRigidboody2D>();
    }

    private void FixedUpdate()
    {
        _simpleRigidboody2D.AddForce(force, mode);
        //so aplicamos força todo update se a força for gradual
        enabled = mode == SimpleForceMode.Force || mode == SimpleForceMode.Acceleration;
    }

    private void OnValidate()
    {
        enabled = true;
    }
}
