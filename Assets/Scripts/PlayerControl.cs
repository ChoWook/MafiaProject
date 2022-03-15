using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : Photon.Pun.MonoBehaviourPun
{
    public float speedM = 5.0f;
    public float speedH = 0.5f;
    public float speedV = 0.5f;

    private Vector2 moveInput;
    private Vector3 moveVec;
    private Rigidbody body;
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private float movementX, movementY;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        moveVec = moveInput.x * transform.right + moveInput.y * transform.forward;
        transform.position = transform.position + moveVec * Time.fixedDeltaTime;
    }

    private void Update()
    {
        yaw += speedH * movementX;
        pitch -= speedV * movementY;

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }

    public void OnMove(InputValue input)
    {
        moveInput = input.Get<Vector2>();
    }

    public void OnLook(InputValue input)
    {
        Vector2 movementVector = input.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }
}
