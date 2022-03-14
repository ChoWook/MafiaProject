using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : Photon.Pun.MonoBehaviourPun
{
    public float Speed = 5.0f;

    Vector3 moveVec;
    Rigidbody body;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        transform.position = transform.position + moveVec * Time.fixedDeltaTime;
    }

    public void OnMove(InputValue input)
    {
        Vector2 inputVec = input.Get<Vector2>();

        moveVec = new Vector3(inputVec.x, 0, inputVec.y);
    }

}
