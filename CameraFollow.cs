using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float mouseSensitivity = 100.0f;
    private float xRotation = 0.0f;

    void Start()
    {
        LockCursor();
    }

    void Update()
    {
        RotateCamera();
        FollowPlayer();
    }

    void FollowPlayer()
    {
        Vector3 desiredPosition = target.position + target.TransformDirection(offset);
        transform.position = new Vector3(desiredPosition.x, target.position.y + offset.y, desiredPosition.z);
        transform.rotation = Quaternion.Euler(10f, target.eulerAngles.y, 0f);
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        target.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
