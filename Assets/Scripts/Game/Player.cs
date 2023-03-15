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
    private WeaponLaser weaponLaser;
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

    public void SetWeapon(string weapon)
    {
        switch (weapon)
        {
            case "Default":
                this.weapon.gameObject.SetActive(true);
                this.weaponShotgun.gameObject.SetActive(false);
                this.weaponLaser.gameObject.SetActive(false);
                break;
            case "Shotgun":
                this.weapon.gameObject.SetActive(false);
                this.weaponShotgun.gameObject.SetActive(true);
                this.weaponLaser.gameObject.SetActive(false);
                break;
            case "Laser":
                this.weapon.gameObject.SetActive(false);
                this.weaponShotgun.gameObject.SetActive(false);
                this.weaponLaser.gameObject.SetActive(true);
                break;
        }
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
        this.weaponLaser = GetComponentInChildren<WeaponLaser>();
        this.SetWeapon("Default");
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
