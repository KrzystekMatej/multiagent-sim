using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class Intersection
{
    public static int LineCircle(Vector2 point, Vector2 direction, Vector2 center, float radius, Vector2[] intersections)
    {
        float a = direction.x * direction.x + direction.y * direction.y;
        float b = 2 * (point.x * direction.x - direction.x * center.x + point.y * direction.y - direction.y * center.y);
        float c = point.x * point.x + point.y * point.y + center.x * center.x + center.y * center.y - radius * radius - 2 * (point.x * center.x + point.y * center.y);
        float d = b * b - 4 * a * c;

        if (d < 0) return 0;
        else if (Mathf.Abs(d) < Mathf.Epsilon && intersections.Length >= 1)
        {
            float t = -b / (2 * a);
            intersections[0] = point + direction * t;
            return 1;
        }
        else if (intersections.Length >= 2)
        {
            float root = Mathf.Sqrt(d);
            float t1 = (-b + root) / (2 * a);
            float t2 = (-b - root) / (2 * a);

            if (t1 < t2)
            {
                intersections[0] = point + direction * t1;
                intersections[1] = point + direction * t2;
            }
            else
            {
                intersections[1] = point + direction * t1;
                intersections[0] = point + direction * t2;
            }
            return 2;
        }

        throw new ArgumentException("The array is too small to hold all intersections.", nameof(intersections));
    }

    public static Vector2? LineLine(Vector2 a, Vector2 u, Vector2 b, Vector2 v)
    {
        float t;
        float denominator = u.x * v.y - u.y * v.x;

        if (Mathf.Abs(denominator) < Mathf.Epsilon)
        {
            return null;
        }

        t = ((b.x - a.x) * v.y - (b.y - a.y) * v.x) / denominator;
        return a + t * u;
    }

    public static bool PointPolygon(Vector2 point, Vector2[] polygonPoints)
    {
        bool inside = false;
        for (int i = 0; i < polygonPoints.Length; i++)
        {
            Vector2 a = polygonPoints[i];
            Vector2 b = polygonPoints[(i + 1) % polygonPoints.Length];

            if ((a.y > point.y) != (b.y > point.y))
            {
                float intersectX = (b.x - a.x) * (point.y - a.y) / (b.y - a.y) + a.x;

                if (point.x < intersectX) inside = !inside;
            }
        }

        return inside;
    }
}
