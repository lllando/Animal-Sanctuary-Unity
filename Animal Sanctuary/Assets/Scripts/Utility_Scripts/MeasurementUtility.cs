using UnityEngine;

public class MeasurementUtility : MonoBehaviour
{
    public static bool IsNear(Vector2 a, Vector2 b, float threshold)
    {
        float distance = Vector2.Distance(a, b);
        return distance <= threshold;
    }
}
