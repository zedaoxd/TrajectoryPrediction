using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RbForceAccumulator1 : MonoBehaviour
{
    [SerializeField] private Vector2 force;
    [SerializeField] private float torque;
    [SerializeField] private ForceMode2D mode;

    private Rigidbody2D rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(force, mode);
        rb.AddTorque(torque, mode);
        RefreshEnableState();
    }

    private void OnValidate()
    {
        enabled = true;
    }
    
    private void RefreshEnableState()
    {
        enabled = mode == ForceMode2D.Force;
    }
}
