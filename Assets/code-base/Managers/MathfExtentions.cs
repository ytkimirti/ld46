using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class M
{
    public static bool Change(this float change)//Misstyped it :,(
    {
        return Random.Range(0, 101) < change;
    }

    public static bool Flip()//Returns true or false %50
    {
        return Random.Range(0, 2) == 0;
    }

    public static float FlipSign()//Returns -1 or 1 %50 of the times
    {
        return Random.Range(0, 2) == 0 ? 1 : -1;
    }
    /* CODE SNIPPETS TO USE

    OPTIMIZING A METHOT AT UPDATE TO WORK FOR INSTANCE EVERY 3 FRAMES
    public static bool OptimizePerFrame(int perFrame)
    {
        for (int i = 0; i < perFrame; i++)
        {
            yield return false;
        }

        yield return true;
    }
    */

    public static int GetDigits(this float n)//Returns the digits in a number
    {
        return n == 0 ? 1 : 1 + (int)Mathf.Log10(Mathf.Abs(n));
    }


    public static bool IsTrueNull(this UnityEngine.Object obj)
    {
        return (object)obj == null;
    }
}

public static class RectTransformExtensions
{
    public static void SetLeft(this RectTransform rt, float left)
    {
        rt.offsetMin = new Vector2(left, rt.offsetMin.y);
    }

    public static void SetRight(this RectTransform rt, float right)
    {
        rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
    }

    public static void SetTop(this RectTransform rt, float top)
    {
        rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
    }

    public static void SetBottom(this RectTransform rt, float bottom)
    {
        rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
    }
}

public static class Vector2Extension
{
    public static float SumLenght(this Vector2 v)//Returns the abs of x + y, its much faster, but no where near accurate
    {
        return Mathf.Abs(v.x) + Mathf.Abs(v.y);
    }

    public static float SqareLenght(Vector2 v)//Returns x*x + y*y, it's %100 percent accurate if you are using it for comparison to other vectors
    {
        return v.x * v.x + v.y * v.y;
    }

    public static float CalculateMeshArea(this Mesh mesh)//Only works for 2D
    {
        float area = 0;
        for (int i = 0; i < mesh.triangles.Length / 3; i++)
        {
            area += CalculateTriangleArea(mesh.vertices[mesh.triangles[i * 3 + 0]], mesh.vertices[mesh.triangles[i * 3 + 1]], mesh.vertices[mesh.triangles[i * 3 + 2]]);
        }
        return area;
    }

    public static float CalculateTriangleArea(Vector2 A, Vector2 B, Vector2 C)
    {
        float a = (A - B).magnitude
        , b = (C - A).magnitude
        , c = (C - B).magnitude;

        float s = (a + b + c) / 2;

        return Mathf.Sqrt(Mathf.Abs(s * (s - a) * (s - b) * (s - c)));
    }

    public static Vector2 Rotate(this Vector2 v, float degrees)
    {
        float radians = degrees * Mathf.Deg2Rad;
        float sin = Mathf.Sin(radians);
        float cos = Mathf.Cos(radians);

        float tx = v.x;
        float ty = v.y;

        return new Vector2(cos * tx - sin * ty, sin * tx + cos * ty);
    }

    public static Vector2 ToVector2(this Vector3 v)
    {
        return new Vector2(v.x, v.z);
    }

    public static Vector3 ToVector3(this Vector2 v)
    {
        return new Vector3(v.x, 0, v.y);
    }

    public static Vector2 RadianToVector2(this float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    public static Vector2 DegreeToVector2(this float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }

    public static bool IsInsideTriangle(this Vector2 s, Vector2 a, Vector2 b, Vector2 c)
    {
        float as_x = s.x - a.x;
        float as_y = s.y - a.y;

        bool s_ab = (b.x - a.x) * as_y - (b.y - a.y) * as_x > 0;

        if ((c.x - a.x) * as_y - (c.y - a.y) * as_x > 0 == s_ab) return false;

        if ((c.x - b.x) * (s.y - b.y) - (c.y - b.y) * (s.x - b.x) > 0 != s_ab) return false;

        return true;
    }


    public static float ToAngle(this Vector2 vector)
    {
        return Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
    }

    public static float ToAnglePrincibal(this Vector2 vector)//Returns the princibal angle, no negative numbers, ex. not -90 but 270 degree
    {
        float degree = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;

        if (degree < 0) degree += 360;

        return degree;
    }

    public static float ToRadians(this Vector2 vector)
    {
        return Mathf.Atan2(vector.y, vector.x);
    }

    public static float ToRadiansPrincibal(this Vector2 vector)//Returns the princibal angle, no negative numbers, ex. not -90 but 270 degree
    {
        float degree = Mathf.Atan2(vector.y, vector.x);

        if (degree < 0) degree += Mathf.PI * 2;

        return degree;
    }

    public static bool FloatingEquals(this float a, float b, float change)//ex. 0.0000001 == 0
    {
        if (a - change < b && a + change > b)
            return true;
        return false;
    }

    public static bool FloatingEquals(this Vector2 a, Vector2 b, float change)//makes
    {
        if (a.x - change < b.x && a.x + change > b.x && a.y - change < b.y && a.y + change > b.y)
            return true;
        return false;
    }

    public static Vector3[] MultiLerpLine(this Vector3[] points, float time)
    {
        float t = time * (points.Length - 1);

        Vector3 pointA = Vector3.zero;
        Vector3 pointB = Vector3.zero;

        List<Vector3> nodes = new List<Vector3>();

        for (int i = 0; i < points.Length; i++)
        {
            if (t < (float)i)
            {
                pointA = points[i - 1];
                pointB = points[i];
                nodes.Add(Vector3.Lerp(pointA, pointB, t - (i - 1)));
                break;
            }
            else if (t == (float)i)
            {
                nodes.Add(points[i]);
                break;
            }
            nodes.Add(points[i]);
        }

        if (nodes.Count != points.Length)
        {
            for (int i = 0; i < points.Length - nodes.Count; i++)
            {
                nodes.Add(nodes[nodes.Count - 1]);
            }
        }


        return nodes.ToArray();
    }

    public static Vector3 MultiLerp(this Vector3[] points, float time)
    {
        if (points.Length == 1)
            return points[0];
        else if (points.Length == 2)
            return Vector3.Lerp(points[0], points[1], time);

        if (time == 0)
            return points[0];

        if (time == 1)
            return points[points.Length - 1];

        float t = time * (points.Length - 1);

        Vector3 pointA = Vector3.zero;
        Vector3 pointB = Vector3.zero;

        for (int i = 0; i < points.Length; i++)
        {
            if (t < (float)i)
            {
                pointA = points[i - 1];
                pointB = points[i];
                return Vector3.Lerp(pointA, pointB, t - (i - 1));
            }
            else if (t == (float)i)
            {
                return points[i];
            }
        }
        return Vector3.zero;
    }


}