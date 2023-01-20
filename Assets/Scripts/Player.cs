using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D body;
    float horizontalInput;
    float verticalInput;
    public float moveSpeed = 4f;
    public float rotationSpeed = 0.5f;
    public Projectile projectilePrefab;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GetPlayerInput();
    }

    void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }

    void GetPlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal"); // -1 left, 1 right
        verticalInput = Input.GetAxisRaw("Vertical"); // -1 down, 1 up
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootProjectile();
        }
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

    void ShootProjectile()
    {
        Projectile projectile = Instantiate(projectilePrefab, transform.position + -transform.up, transform.rotation);
        projectile.Project(-transform.up);
    }
}
