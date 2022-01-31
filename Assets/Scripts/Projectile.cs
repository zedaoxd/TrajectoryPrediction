using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(SimpleRigidboody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float killY = -20;
    public SimpleRigidboody2D Rb => GetComponent<SimpleRigidboody2D>();

    private void FixedUpdate()
    {
        if (Rb.Position.y < killY)
        {
            Destroy(gameObject);
        }
    }
}
