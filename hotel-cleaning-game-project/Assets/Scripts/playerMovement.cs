using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public Camera playerCamera;

    public Transform orientation;

    public float walkSpeed = 6f;
    public float runSpeed = 12f;

    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    private Vector3 moveDirection = Vector3.zero;
    float horizontalInput;
    float verticalInput;
    bool isRunning = false;

    private float rotationX = 0;

    Rigidbody rb;

    // Rotation Lock
    public bool rotationLocked = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        /// optional
        isRunning = Input.GetKey(KeyCode.LeftShift);

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (!rotationLocked)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    private void FixedUpdate()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * (isRunning ? runSpeed : walkSpeed), ForceMode.Force);
    }
}
