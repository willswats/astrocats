using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float rotationSpeed = 0.5f;
    public Projectile projectilePrefab;
    private float horizontalInput;
    private float verticalInput;
    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GetPlayerInput();
    }

    private void FixedUpdate()
    {
        HandleVerticalInput();
        HandleHorizonalInput();
    }

    private void GetPlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal"); // -1 left, 1 right
        verticalInput = Input.GetAxisRaw("Vertical"); // -1 down, 1 up
        if (Input.GetButtonDown("Fire1"))
        {
            InstantiateProjectile();
        }
    }

    private void HandleVerticalInput()
    {
        body.AddRelativeForce(new Vector2(0, -verticalInput) * moveSpeed);
    }

    private void HandleHorizonalInput()
    {
        body.AddTorque(-horizontalInput * rotationSpeed);
    }

    private void InstantiateProjectile()
    {
        Projectile projectile = Instantiate(projectilePrefab, transform.position + -transform.up, transform.rotation);
        projectile.Project(-transform.up);
    }
}
