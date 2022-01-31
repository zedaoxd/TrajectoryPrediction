using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRigidboody2D : MonoBehaviour
{
    // posicao, velocidade, aceleracao
    public Vector2 Position
    {
        get => transform.position;
        set => transform.position = value;
    }

    private PhysicsWorld2D physicsWorld;

    public Vector2 Velocity;
    public Vector2 Acceleration;

    private void Awake()
    {
        // TODO: PhysicsWorld2D deve ser um singleton
        physicsWorld = FindObjectOfType<PhysicsWorld2D>();
        physicsWorld.Register(this);
    }

    private void OnDestroy()
    {
        physicsWorld.Unregister(this);
    }
}
