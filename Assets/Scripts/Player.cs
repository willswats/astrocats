using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float rotationSpeed = 0.5f;
    public Projectile projectilePrefab;
    private Rigidbody2D body;
    private float horizontalInput;
    private float verticalInput;
    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GetPlayerInput();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }

    private void GetPlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal"); // -1 left, 1 right
        verticalInput = Input.GetAxisRaw("Vertical"); // -1 down, 1 up
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootProjectile();
        }
    }

    private void MovePlayer()
    {
        body.AddForce(-transform.up * Mathf.Clamp01(verticalInput) * moveSpeed);
    }

    private void RotatePlayer()
    {
        float rotation = -horizontalInput * rotationSpeed;
        transform.Rotate(Vector3.forward * rotation);
        body.AddTorque(rotation);
    }

    private void ShootProjectile()
    {
        Projectile projectile = Instantiate(projectilePrefab, transform.position + -transform.up, transform.rotation);
        projectile.Project(-transform.up);
    }
}
