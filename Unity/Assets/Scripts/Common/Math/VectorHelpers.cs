using System;
using UnityEngine;

public static class VectorHelpers
{
    public static bool IsAngleConvexCC(Vector3 a, Vector3 b)
    {
        return Vector3.Cross(a, b).z >= 0;
    }

    public static float GetVectorRadAngle(Vector2 vector)
    {
        return Mathf.Atan2(vector.y, vector.x);
    }

    public static Vector2 PolarCoordinatesToVector2(float angleRad, float magnitude)
    {
        return new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)) * magnitude;
    }

    public static Vector2 GetSignedVector(Vector2 vector)
    {
        return new Vector2(Math.Sign(vector.x), Math.Sign(vector.y));
    }

    public static Vector2 GetAbsoluteVector(Vector2 vector)
    {
        return new Vector2(Mathf.Abs(vector.x), Mathf.Abs(vector.y));
    }

    public static Vector2 PerpendicularCC(Vector2 vector)
    {
        return new Vector2(vector.y, -vector.x);
    }

    public static Vector2 PerpendicularC(Vector2 vector)
    {
        return new Vector2(-vector.y, vector.x);
    }
}
