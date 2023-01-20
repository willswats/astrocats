using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D body;
    Vector2 moveDirection;
    public float moveSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // -1 left, 1 right
        float vertical = Input.GetAxisRaw("Vertical"); // -1 down, 1 up

        moveDirection = new Vector2(horizontal, vertical).normalized;
    }

    // Frame-rate independent for physics calculations
    void FixedUpdate()
    {
        body.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
