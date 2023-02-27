using UnityEngine;

public class Player : MonoBehaviour
{
    public Weapon weapon;
    public Projectile projectilePrefab;
    public float moveSpeed = 4f;
    public float rotationSpeed = 1f;
    private float verticalInput;
    private float horizontalInput;
    private Rigidbody2D rb2d;

    public void ChangeWeapon(Weapon weapon)
    {
        this.weapon = weapon;
    }

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
            GameManager.Instance.ToggleGameOverMenu();
        }
    }

    private void GetInput()
    {
        this.verticalInput = Input.GetAxisRaw("Vertical");
        this.horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Fire1"))
        {
            weapon.HandleFire(this.transform.position, -this.transform.up, this.transform.rotation);
        }
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
