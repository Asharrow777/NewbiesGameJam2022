using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    public float movementSpeed;
    public float crouchSpeed = 4f;
    public float sprintSpeed = 8f;
    public float jumpHeight = 3f;
    private float coyoteTimeTimer;
    [SerializeField] private Vector3 velocity;
    private CharacterController playerController;
    private float gravity = -9.81f;

    [Header("Player Input")]
    private float inputX;
    private float inputZ;

    [Header("Ground Check")]
    [SerializeField] private bool isGrounded;
    private Transform groundCheck;
    public float groundDistance = 0.4f;
    private LayerMask groundLayer;
    private float jumpBufferTimer;

    // Start is called before the first frame update
    void Start()
    {
        groundCheck = transform.Find("Ground Check");
        groundLayer = LayerMask.GetMask("Ground");
        playerController = GetComponent<CharacterController>();
        movementSpeed = sprintSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        ApplyGravity();
        MoveInputDetection();
    }

    private void FixedUpdate()
    {
        DecrementTimers();
    }

    private void DecrementTimers()
    {
        coyoteTimeTimer -= Time.deltaTime;
        jumpBufferTimer -= Time.deltaTime;
    }

    private void ApplyGravity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);
        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }
    }

    private void MoveInputDetection()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputZ = Input.GetAxisRaw("Vertical");
        MovePlayer();
    }

    private void MovePlayer()
    {
        GroundCheck();
        CrouchCheck();
        playerController.Move((transform.right * inputX + transform.forward * inputZ) * movementSpeed * Time.deltaTime);
        JumpCheck();
        velocity.y += gravity * Time.deltaTime;
        playerController.Move(velocity * Time.deltaTime);
    }

    private void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);
        if (isGrounded) coyoteTimeTimer = .25f;
    }

    private void JumpCheck()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        } else if (jumpBufferTimer > 0f && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        else if (coyoteTimeTimer > 0f && Input.GetButtonDown("Jump") && !isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        else if (Input.GetButtonDown("Jump") && !isGrounded)
        {
            jumpBufferTimer = .25f;
        }
    }

    private void CrouchCheck()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            playerController.height /= 2;
            movementSpeed = crouchSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            playerController.height *= 2;
            movementSpeed = sprintSpeed;
        }
    }
}
