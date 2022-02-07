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

[RequireComponent(typeof(Shape2D))]
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
    [Min(0)] public float LinearDrag;
    public Vector2 NetForce { get; private set; }
    public Vector2 InstantNetForce { get; private set; }
    public float NetTorque { get; private set; }
    public float InstantNetTorque { get; private set; }

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

    private Shape2D Shape => GetComponent<Shape2D>();
    public float InverseMass => Shape.InverseMass;
    public float mass => Shape.Mass;
    public float InverseMomentOfInercia => Shape.InverseMomentOfInertia;
    public float MomentOfInertia => Shape.MomentOfInertia;
    
    [Space]
    [Header("Rotations")]
    public float angularVelocity;
    public float angularAcceleration;
    public float angularDrag;

    private void Awake()
    {
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

    public void ResetForces()
    {
        NetForce = InstantNetForce = Vector2.zero;
        NetTorque = InstantNetTorque = 0.0f;
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

    public void AddTorque(float torque, SimpleForceMode mode)
    {
        switch (mode)
        {
            case SimpleForceMode.Force:
                NetTorque += torque;
                break;
            case SimpleForceMode.Impulse:
                InstantNetTorque += torque;
                break;
            case SimpleForceMode.Acceleration:
                NetTorque += (torque * MomentOfInertia);
                break;
            case SimpleForceMode.VelocityChange:
                InstantNetTorque += (torque * MomentOfInertia);
                break;
            default:
                throw new NotImplementedException($"Modo não implementado {mode}");
        }
    }
}
