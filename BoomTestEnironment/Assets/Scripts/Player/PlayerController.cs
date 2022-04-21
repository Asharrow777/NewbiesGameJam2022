using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    private float inputX;
    private float inputZ;
    public float jumpHeight = 3f;
    private Transform groundCheck;
    public float groundDistance = 0.4f;
    private LayerMask groundLayer;
    private bool isGrounded;
    public float movementSpeed = 8f;
    [SerializeField] private Vector3 velocity;
    private CharacterController playerController;
    private float gravity = -9.81f;

    // Start is called before the first frame update
    void Start()
    {
        groundCheck = transform.Find("Ground Check");
        groundLayer = LayerMask.GetMask("Ground");
        playerController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);

        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }
        MoveInputDetection();
    }

    private void MoveInputDetection()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputZ = Input.GetAxisRaw("Vertical");
        MovePlayer();
    }

    private void MovePlayer()
    {
       playerController.Move( (transform.right * inputX + transform.forward*inputZ) * movementSpeed * Time.deltaTime);
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        playerController.Move(velocity * Time.deltaTime);
    }
}
