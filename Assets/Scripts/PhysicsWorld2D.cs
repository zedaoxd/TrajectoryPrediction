using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PhysicsWorld2D : MonoBehaviour
{
    private List<SimpleRigidboody2D> rbs = new List<SimpleRigidboody2D>();

    public void Register(SimpleRigidboody2D rb)
    {
        Assert.IsFalse(rbs.Contains(rb), "Registro repetido de rigdboody não é suportado");
        rbs.Add(rb);
    }

    public void Unregister(SimpleRigidboody2D rb)
    {
        rbs.Remove(rb);
    }
    
    private void FixedUpdate()
    {
        Simulate(Time.fixedDeltaTime);
    }

    private void Simulate(float dt)
    {
        // simular todos os rigid bodies (fase de Dynamics)
        foreach (var rb in rbs)
        {
            // simula
            // v = v + a.dt
            // p = p + v.dt
            rb.Velocity += rb.Acceleration * dt;
            rb.Position += rb.Velocity * dt;
        }
    }
}
