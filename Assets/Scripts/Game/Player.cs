using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 100;
    public float moveSpeed = 4f;
    public float rotationSpeed = 0.5f;
    private float verticalInput;
    private float horizontalInput;
    private Rigidbody2D rb2d;

    private void Awake()
    {
        GameManager.Instance.UpdateHealthUI(health);
        this.rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        HandleVerticalInput();
        HandleHorizontalInput();
    }

    public void DamagePlayer(int damage)
    {
        this.health -= damage;
        GameManager.Instance.UpdateHealthUI(health);
        if (this.health <= 0)
        {
            Destroy(gameObject);
            GameManager.Instance.KillPlayer();
        }
    }

    private void GetInput()
    {
        this.verticalInput = Input.GetAxisRaw("Vertical");
        this.horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    private void HandleVerticalInput()
    {
        this.rb2d.AddRelativeForce(new Vector2(0, -verticalInput) * moveSpeed);
    }

    private void HandleHorizontalInput()
    {
        this.rb2d.AddTorque(-horizontalInput * rotationSpeed);
    }
}
