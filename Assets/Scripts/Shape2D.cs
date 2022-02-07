using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class Shape2D : MonoBehaviour
{
    [SerializeField] private float mass = 1f;
    public float Mass => mass;
    public float InverseMass { get; private set; }
    public float MomentOfInertia { get; private set;  }
    public float InverseMomentOfInertia { get; private set; }

    protected abstract float CalculateMomentOfInertia();
    
    private void Awake()
    {
        UpdateInverseMassAndMomentOfInertia();
    }

    private void OnValidate()
    {
        UpdateInverseMassAndMomentOfInertia();
    }

    private void UpdateInverseMassAndMomentOfInertia()
    {
        Assert.IsFalse(Mathf.Approximately(mass, 0), "unsuport mass 0");
        InverseMass = Mathf.Approximately(mass, 0) ? 0 : 1.0f / mass;

        MomentOfInertia = CalculateMomentOfInertia();
        InverseMomentOfInertia = 1.0f / MomentOfInertia;
    }
}
