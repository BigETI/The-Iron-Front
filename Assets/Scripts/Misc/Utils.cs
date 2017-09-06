using UnityEngine;

// Utility class
public static class Utils
{

    // Get random element from array
    public static T GetRandomElement<T>(T[] arr)
    {
        T ret = default(T);
        if (arr != null)
        {
            if (arr.Length > 0)
                ret = arr[Random.Range(0, arr.Length)];
        }
        return ret;
    }

    // Direction with inaccuracy
    public static Vector2 DirectionInaccuracy2D(Vector2 direction, float inaccuracyDegrees)
    {
        float theta = (Random.Range(inaccuracyDegrees * -0.5f, inaccuracyDegrees * 0.5f) * Mathf.PI) / 180.0f;
        float cos = Mathf.Cos(theta);
        float sin = Mathf.Sin(theta);
        return (new Vector2((direction.x * cos) - (direction.y * sin), (direction.x * sin) + (direction.y * cos))).normalized;
    }

    // Direction with inaccuracy
    public static Vector3 DirectionInaccuracy(Vector3 direction, float inaccuracyDegrees)
    {
        float theta = (Random.Range(inaccuracyDegrees * -0.5f, inaccuracyDegrees * 0.5f) * Mathf.PI) / 180.0f;
        float cos = Mathf.Cos(theta);
        float sin = Mathf.Sin(theta);
        return (new Vector3((direction.x * cos) - (direction.y * sin), (direction.x * sin) + (direction.y * cos), 0.0f)).normalized;
    }
}
