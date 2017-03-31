using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRFirstPersonCameraController : MonoBehaviour
{
    Quaternion gyro;

    [SerializeField]
    float longitude = 0.0f;
    [SerializeField]
    float latitude = 0.0f;

    [SerializeField]
    float speed = 0.5f;

    bool canMouseControl = false;

    void Awake()
    {
        if (Application.isEditor)
        {
            GameObject obj = GameObject.Find("GvrViewerMain");

            if(obj != null)
            {
                //obj.SetActive(false);
            }
        }
    }

    void Start()
    {
        Input.gyro.enabled = true;

        GameObject obj = GameObject.Find("GvrViewerMain");

        if(obj == null)
        {
            //VRモードがオフであるということ
            this.enabled = false;
        }
        else
        {
            GetComponent<FirstPersonCameraController>().enabled = false;
        }
    }

    void Update()
    {
        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) ChangeMouseControl();
            EditorCameraController();
            return;
        }
        if (!Input.gyro.enabled) return;
        gyro = Input.gyro.attitude;
        //ジャイロはデフォルトで下を向いているので90度修正。X軸もY軸も逆のベクトルに変換
        gyro = Quaternion.Euler(90.0f, 0.0f, 0.0f) * (new Quaternion(-gyro.x, -gyro.y, gyro.z, gyro.w));
        transform.localRotation = gyro;
    }

    void EditorCameraController()
    {
        Vector2 input = GetKeyInputVector();
        input *= Time.deltaTime * speed;
        longitude += input.x;
        latitude += input.y;

        longitude = longitude % 360.0f;
        latitude = Mathf.Clamp(latitude, -60.0f, 80.0f);

        Vector3 targetPosition = transform.position + KKUtilities.SphereCoordinate(longitude, latitude, 10.0f);
        transform.LookAt(targetPosition);
    }

    Vector2 GetKeyInputVector()
    {
        Vector2 input;
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        if (canMouseControl)
        {
            input.x = Input.GetAxis("Mouse X");
            input.y = Input.GetAxis("Mouse Y");
        }

        return input;
    }

    void ChangeMouseControl()
    {
        canMouseControl = !canMouseControl;

        if (canMouseControl)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
