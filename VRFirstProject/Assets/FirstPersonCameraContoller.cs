using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCameraContoller : MonoBehaviour
{
    [SerializeField]
    GameObject player = null;

    [SerializeField]
    float rotationSpeedX = 150.0f;
    [SerializeField]
    float rotationSpeedY = 100.0f;

    public float maxRotationY = 80.0f;
    public float minRotationY = 15.0f;
    float rotationX = 180.0f;
    float rotationY = 15.0f;

    [SerializeField]
    Vector3 offset = new Vector3(0, 1.5f, 0);

    //カメラを制御するか？
    public bool IsWork = true;

    bool CanMouseControl = false;

    void Start()
    {
        SetPlayer(player);

        if (player != null)
        {
            transform.SetParent(player.transform);
            transform.localPosition = offset;
        }
    }

    void SetPlayer(GameObject player = null)
    {
        if (player == null)
        {
            GameObject g = GameObject.FindGameObjectWithTag("Player");
            if (g != null) player = g;
        }

        if (player == null)
        {
            GameObject g = GameObject.Find("Player");
            if (g != null) player = g;
        }

        if (player == null)
        {
            Debug.Log("player is null");
            return;
        }
    }

    void Update()
    {
        if (player == null) return;
        if (!IsWork) return;

        if(Input.GetKeyDown(KeyCode.E)) ChangeMouseControl();

        Vector2 inputVector = GetInputVector();

        rotationX += inputVector.x * rotationSpeedX * Time.deltaTime;
        rotationY += inputVector.y * rotationSpeedY * Time.deltaTime;


        rotationX = rotationX % 360.0f;
        rotationY = Mathf.Clamp(rotationY, minRotationY, maxRotationY);

        Vector3 targetPosition = transform.position + KKUtilities.SphereCoordinate(rotationX, rotationY, 1.0f);

        transform.LookAt(targetPosition);

        player.transform.localRotation = Quaternion.Euler(new Vector3(0.0f, transform.eulerAngles.y, 0.0f));
    }

    Vector2 GetInputVector()
    {
        Vector2 rightStick;
        rightStick.x = Input.GetAxis("Horizontal2");
        rightStick.y = Input.GetAxis("Vertical2");

        if (rightStick == Vector2.zero)
        {
            if (Input.GetKey(KeyCode.LeftArrow)) rightStick.x = -1;
            if (Input.GetKey(KeyCode.RightArrow)) rightStick.x = 1;
            if (Input.GetKey(KeyCode.UpArrow)) rightStick.y = 1;
            if (Input.GetKey(KeyCode.DownArrow)) rightStick.y = -1;
        }

        if (CanMouseControl)
        {
            rightStick.x = Input.GetAxis("Mouse X");
            rightStick.y = Input.GetAxis("Mouse Y");
        }

        return rightStick;
    }

    void ChangeMouseControl()
    {
        CanMouseControl = !CanMouseControl;

        if (CanMouseControl)
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
