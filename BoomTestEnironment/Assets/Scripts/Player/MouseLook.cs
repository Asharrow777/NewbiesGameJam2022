using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("Camera")]
    public float mouseSensitivity = 120f; 
    [Header("Player Movement")]
    private float inputX;
    private float inputY;
    private float xRotation = 0f;
    public Transform playerGameObject;
    // Start is called before the first frame update
    void Start()
    {
        playerGameObject = GameObject.FindGameObjectWithTag("Player").transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        MoveInputDetection();
    }

    private void MoveInputDetection()
    {

        inputX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        inputY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        RotatePlayer(inputX, inputY);
    }

    private void RotatePlayer(float x, float y)
    {
        //print("rotating player");
        xRotation -= inputY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerGameObject.Rotate(Vector3.up * x);

    }
}
