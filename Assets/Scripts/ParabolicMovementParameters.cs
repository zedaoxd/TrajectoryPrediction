using System.Collections.Generic;
using UnityEngine;

public class ParabolicMovementParameters
{
    public Vector2 Position;
    public Vector2 Velocity;

    public float Gravity;
}

public static class ParabolicMovementCalculator
{
    public static Vector2[] SimulateParabolicTrajectory(ParabolicMovementParameters launchParameters, float endY, float dt)
    {
        var listPoints = new List<Vector2>();
        var gravity = new Vector2(0, -launchParameters.Gravity);
        var cont = 1;
        var t = cont * dt;
        // p = p1 + v * t + (a * t ^ 2) / 2
        var point = launchParameters.Position + (launchParameters.Velocity * t) + 0.5f * gravity * t * t;
        
        while (point.y >= endY)
        {
            listPoints.Add(point);
            cont += 1;
            t = cont * dt;
            point = launchParameters.Position + (launchParameters.Velocity * t) + 0.5f * gravity * t * t;
        }

        return listPoints.ToArray();
    }
}
