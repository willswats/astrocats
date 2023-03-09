using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 100;
    public float moveSpeed = 4f;
    public float rotationSpeed = 0.5f;
    private float verticalInput;
    private float horizontalInput;
    private Weapon weapon;
    private WeaponShotgun weaponShotgun;
    private Rigidbody2D rb2d;

    public void DamagePlayer(int damage)
    {
        this.health -= damage;
        UIManager.Instance.SetTextPlayerHealth(this.health);
        if (this.health <= 0)
        {
            Destroy(this.gameObject);
            GameManager.Instance.KillPlayer();
        }
    }

    public void SetWeaponDefault()
    {
        this.weapon.gameObject.SetActive(true);
        this.weaponShotgun.gameObject.SetActive(false);
    }

    public void SetWeaponShotgun()
    {
        this.weapon.gameObject.SetActive(false);
        this.weaponShotgun.gameObject.SetActive(true);
    }

    private void Awake()
    {
        this.rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        UIManager.Instance.SetTextPlayerHealth(this.health);
        this.weapon = GetComponentInChildren<Weapon>();
        this.weaponShotgun = GetComponentInChildren<WeaponShotgun>();
        this.SetWeaponDefault();
    }

    private void Update()
    {
        this.GetInput();
    }

    private void FixedUpdate()
    {
        this.HandleVerticalInput();
        this.HandleHorizontalInput();
    }

    private void GetInput()
    {
        this.verticalInput = Input.GetAxisRaw("Vertical");
        this.horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    private void HandleVerticalInput()
    {
        this.rb2d.AddRelativeForce(new Vector2(0, -this.verticalInput) * this.moveSpeed);
    }

    private void HandleHorizontalInput()
    {
        this.rb2d.AddTorque(-this.horizontalInput * this.rotationSpeed);
    }
}
