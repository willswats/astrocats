using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D body;
    float horizontalInput;
    float verticalInput;
    public float moveSpeed = 4f;
    public float rotationSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerInput();
    }

    // Frame-rate independent for physics calculations
    void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }

    void GetPlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal"); // -1 left, 1 right
        verticalInput = Input.GetAxisRaw("Vertical"); // -1 down, 1 up
    }

    void MovePlayer()
    {
        body.AddForce(-transform.up * Mathf.Clamp01(verticalInput) * moveSpeed);
    }

    void RotatePlayer()
    {
        float rotation = -horizontalInput * rotationSpeed;
        transform.Rotate(Vector3.forward * rotation);
        body.AddTorque(rotation);
    }
}
