using UnityEngine;

public class Player : MonoBehaviour
{
    public Weapon weapon;
    public float moveSpeed = 4f;
    public float rotationSpeed = 1f;
    private float verticalInput;
    private float horizontalInput;
    private Rigidbody2D rb2d;

    private void Awake()
    {
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            Destroy(gameObject);
            // TODO: Decreease health
            if (GameManager.Instance.lives >= 1)
            {
                GameManager.Instance.DecrementLife();
                // TODO: Spawn player
            }
            else
            {
                GameManager.Instance.ToggleGameOverMenu();
            }
        }
    }

    public void ChangeWeapon(Weapon weapon)
    {
        this.weapon = weapon;
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
