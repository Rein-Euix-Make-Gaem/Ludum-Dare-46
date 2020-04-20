using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivity = 150f;
    public Transform target;

    private float rotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        var mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        var mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        rotation -= mouseY;
        rotation = Mathf.Clamp(rotation, -90, +90);

        transform.localRotation = Quaternion.Euler(rotation, 0, 0);
        target.Rotate(Vector3.up * mouseX);
    }
}
