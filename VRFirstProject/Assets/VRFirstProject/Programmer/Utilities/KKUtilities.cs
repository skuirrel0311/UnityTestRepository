using System.Collections;
using System;
using UnityEngine;

public class KKUtilities
{
    public static IEnumerator Delay(float duration, Action action)
    {
        yield return new WaitForSeconds(duration);
        action.Invoke();
    }

    public static float FloatLerp(float a, float b, float t)
    {
        t = Mathf.Clamp(t, 0, 1);

        return a + ((b - a) * t);
    }

    public static float GetAngleY(Vector3 vec1, Vector3 vec2)
    {
        Vector3 temp = vec2 - vec1;
        float vecY = temp.y;
        //X方向だけのベクトルに変換
        temp = Vector3.right * temp.magnitude;
        temp.y = vecY;

        return Vector3.Angle(Vector3.right, temp) * 2.0f;
    }

    /// <summary>
    /// 指定した角度の球体座標を返します
    /// </summary>
    /// <param name="longitude">経度</param>
    /// <param name="latitude">緯度</param>
    /// <returns></returns>
    public static Vector3 SphereCoordinate(float longitude, float latitude, float distance)
    {
        Vector3 position = Vector3.zero;

        //重複した計算
        float temp1 = distance * Mathf.Cos(latitude * Mathf.Deg2Rad);
        float temp2 = longitude * Mathf.Deg2Rad;

        position.x = temp1 * Mathf.Sin(temp2);
        position.y = distance * Mathf.Sin(latitude * Mathf.Deg2Rad);
        position.z = temp1 * Mathf.Cos(temp2);

        return position;
    }
}
