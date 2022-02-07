using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


public enum SimpleForceMode
{
    Force, //Aplica uma força gradual na frame
    Impulse, //Aplica toda a força em uma frame só
    Acceleration, //Aplica uma força independente da massa (tudo vira aceleração)
    VelocityChange, //Aplica toda força de uma vez, independente da massa (tudo vira mudança de velocidade)
}

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
    [SerializeField] private float mass = 1f;
    
    [Min(0)] public float LinearDrag;
    
    public Vector2 NetForce { get; private set; }
    public Vector2 InstantNetForce { get; private set; }
    public float InverseMass { get; private set; }

    public float Orientation
    {
        get => transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
        set
        {
            var rot = transform.rotation.eulerAngles;
            rot.z = value * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(rot);
        }
    }
    
    [Space]
    [Header("Rotations")]
    public float angularVelocity;
    public float angularAcceleration;
    public float angularDrag;

    private void Awake()
    {
        UpdateInverseMass();
        // TODO: PhysicsWorld2D deve ser um singleton
        physicsWorld = FindObjectOfType<PhysicsWorld2D>();
        physicsWorld.Register(this);
    }

    private void OnDestroy()
    {
        if (physicsWorld != null)
        {
            physicsWorld.Unregister(this);
        }
    }

    private void OnValidate()
    {
        UpdateInverseMass();
    }

    private void UpdateInverseMass()
    {
        Assert.IsFalse(Mathf.Approximately(mass, 0), "unsuport mass 0");
        InverseMass = Mathf.Approximately(mass, 0) ? 0 : 1.0f / mass;
    }

    public void ResetForces()
    {
        NetForce = InstantNetForce = Vector2.zero;
    }

    public void AddForce(Vector2 force, SimpleForceMode mode)
    {
        switch (mode)
        {
            case SimpleForceMode.Force:
                NetForce += force;
                break;
            case SimpleForceMode.Impulse:
                InstantNetForce += force;
                break;
            case SimpleForceMode.Acceleration:
                NetForce += (force * mass);
                break;
            case SimpleForceMode.VelocityChange:
                InstantNetForce += (force * mass);
                break;
            default:
                throw new NotImplementedException($"Modo não implementado {mode}");
        }
    }
}
