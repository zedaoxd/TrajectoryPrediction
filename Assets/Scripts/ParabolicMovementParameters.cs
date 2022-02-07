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
        var simulationTime = EstimateTimeToReachYPositon(launchParameters, endY);

        var positionCount = Mathf.RoundToInt(simulationTime / dt);
        var positions = new Vector2[positionCount];
        var t = 0.0f;
        
        if (positionCount > 0)
        {
            positions[0] = launchParameters.Position;
            for (var i = 1; i < positions.Length; i++)
            {
                t += dt;
                var posX = launchParameters.Position.x + launchParameters.Velocity.x * t;
                var posY = launchParameters.Position.y + launchParameters.Velocity.y * t - launchParameters.Gravity * t * t * 0.5f;
                positions[i] = new Vector2(posX, posY);
            }
        }

        return positions;
    }

    public static float EstimateTimeToReachYPositon(ParabolicMovementParameters initialParameters, float endY)
    {
        var a = -initialParameters.Gravity * 0.5f;
        var b = initialParameters.Velocity.y;
        var c = -(endY - initialParameters.Position.y);

        var delta = b * b - 4 * a * c;
        
        if (delta < 0) return 0;

        // sempre vai ser t2
        /* float t1 = (-b + Mathf.Sqrt(delta)) / (2 * a); */
        var t2 = (-b - Mathf.Sqrt(delta)) / (2 * a);
        return t2;
    }
}
