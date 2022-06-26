using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerControl : Photon.Pun.MonoBehaviourPun
{
    [SerializeField] private float speedM = 5.0f;
    [SerializeField] private float speedH = 0.1f;
    [SerializeField] private float speedV = 0.1f;
    

    //점프 관련 변수들
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask ground;

    private Vector2 moveInput;
    private Vector3 moveVec;
    private Rigidbody body;
    private Animator animator;
    private TextMesh NicknameText;
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private float cameraMovementX, cameraMovementY;

    [SerializeField] private GameObject playerCamera;
    [SerializeField] private Color[] colors = null;


    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        NicknameText = GetComponentInChildren<TextMesh>();

        Cursor.lockState = CursorLockMode.Locked;

        if (!photonView.IsMine)
        {
            playerCamera.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        
        if(body == null)
        {
            return;
        }
        body.velocity = new Vector3(0, body.velocity.y, 0); // 서있을 때 미끄러지는 것 방지

        
        moveVec = moveInput.x * transform.right + moveInput.y * transform.forward;
        transform.position = transform.position + moveVec * Time.fixedDeltaTime * speedM;

        if (moveInput.x == 0 && moveInput.y == 0)
        {
            animator.SetBool("IsRun", false);
            return;
        }
    }

    private void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        yaw += speedH * cameraMovementX;
        pitch -= speedV * cameraMovementY;

        transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);
        playerCamera.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }

    public void OnMove(InputValue input)
    {
        animator.SetBool("IsRun", true);
        moveInput = input.Get<Vector2>();
    }

    public void OnLook(InputValue input)
    {
        Vector2 movementVector = input.Get<Vector2>();

        cameraMovementX = movementVector.x;
        cameraMovementY = movementVector.y;
    }

    public void OnJump(InputValue input)
    {
        // 이 게임에서 굳이 점프가 필요한가에 대한 고찰이 필요
        /*
        if (GroundCheck())
        {
            body.velocity = new Vector3(body.velocity.x, jumpForce, body.velocity.z);
        }
        */
    }

    public void SetMaterial(int _playerNum)
    {
        Debug.Log(_playerNum + " : " + colors.Length);
        if (_playerNum > colors.Length) return;
        // 색상
        
        this.GetComponent<MeshRenderer>().material.color = colors[_playerNum - 1];
        NicknameText.color = colors[_playerNum - 1];
    }

    public void NicknameDisplay(string _nickname)
    {
        if (_nickname == null)
        {
            return;
        }

        NicknameText.text = _nickname;
    }

    private bool GroundCheck()
    {
        return Physics.CheckSphere(groundCheckTransform.position, .1f, ground);
    }
}
