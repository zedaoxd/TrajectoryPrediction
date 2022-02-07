using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SimpleRigidboody2D))]
public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform target;

    private SimpleRigidboody2D _simpleRigidboody2D;

    private void Awake()
    {
        _simpleRigidboody2D = GetComponent<SimpleRigidboody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 targetPosition = target.position;
        Vector2 transformPosition = transform.position;
        
        var d = Vector2.Distance(targetPosition, transformPosition);
        var force = ((targetPosition - transformPosition).normalized * d) - _simpleRigidboody2D.Velocity;

        _simpleRigidboody2D.AddForce(force, SimpleForceMode.Force);
        _simpleRigidboody2D.LinearDrag = _simpleRigidboody2D.Velocity.magnitude;
    }
}
