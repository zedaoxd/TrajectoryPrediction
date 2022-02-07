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
            var dv = (rb.NetForce * rb.InverseMass * dt) + (rb.InstantNetForce * rb.InverseMass);
            rb.Velocity = (rb.Velocity + dv) * (1 - rb.LinearDrag * dt);
            rb.Position += rb.Velocity * dt;
            
            // torque
            var dw = (rb.NetTorque * rb.InverseMomentOfInercia * dt) + rb.InstantNetTorque * rb.InverseMomentOfInercia;
            rb.angularVelocity = (rb.angularVelocity - dw) * (1 - dt * rb.angularDrag);
            rb.Orientation += rb.angularVelocity * dt;
            
            rb.ResetForces();
        }
    }
}
