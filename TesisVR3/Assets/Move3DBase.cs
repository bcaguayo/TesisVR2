using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move3DBase : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 1f;

    private Transform cam;
    private float pitch = 0f;

    void Start()
    {
        // Lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Find the camera if not assigned
        cam = Camera.main.transform;
    }

    void Update()
    {
        Move();
        Look();
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal"); // A/D
        float z = Input.GetAxis("Vertical");   // W/S

        Vector3 move = transform.right * x + transform.forward * z;
        transform.position += move * moveSpeed * Time.deltaTime;
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotate player (Y-axis)
        transform.Rotate(Vector3.up * mouseX);

        // Rotate camera (X-axis)
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -90f, 90f);
        cam.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }
}
