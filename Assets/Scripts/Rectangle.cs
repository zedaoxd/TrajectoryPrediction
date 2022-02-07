using System;
using UnityEngine;

public class Rectangle : Shape2D
{
    [SerializeField] private Vector2 size;
    // I = (1/12) * m * (base^2 + h^2)
    protected override float CalculateMomentOfInertia()
    {
        return (1.0f / 12.0f) * Mass * (size.x * size.x + size.y * size.y);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, size);
    }
}