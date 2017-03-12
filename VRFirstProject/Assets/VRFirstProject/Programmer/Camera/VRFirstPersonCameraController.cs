using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRFirstPersonCameraController : MonoBehaviour
{
    Quaternion gyro;

    void Start()
    {
        Input.gyro.enabled = true;

    }

    void Update()
    {
        if (Application.isEditor) return;
        if (!Input.gyro.enabled) return;
        gyro = Input.gyro.attitude;
        //ジャイロはデフォルトで下を向いているので90度修正。X軸もY軸も逆のベクトルに変換
        gyro = Quaternion.Euler(90.0f, 0.0f, 0.0f) * (new Quaternion(-gyro.x, -gyro.y, gyro.z, gyro.w));
        transform.localRotation = gyro;
    }
}
