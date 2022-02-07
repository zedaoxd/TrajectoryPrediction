using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RbForceAccumulator1 : MonoBehaviour
{
    [SerializeField] private Vector2 force;
    [SerializeField] private ForceMode2D mode;

    private Rigidbody2D rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(force, mode);
        //so aplicamos força todo update se a força for gradual
        enabled = mode == ForceMode2D.Force;
    }

    private void OnValidate()
    {
        enabled = true;
    }
}
