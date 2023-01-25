using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float rotationSpeed = 1f;
    private float verticalInput;
    private float horizontalInput;
    private Rigidbody2D body;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void GetInput()
    {
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Fire");
        }
    }

    private void HandleMovement()
    {
        body.AddRelativeForce(new Vector2(0, -verticalInput) * moveSpeed);
        body.AddTorque(-horizontalInput * rotationSpeed);
    }
}
