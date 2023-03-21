using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 100;
    public float moveSpeed = 4f;
    public float rotationSpeed = 0.5f;
    private float verticalInput;
    private float horizontalInput;
    private GameObject weaponDefault;
    private GameObject weaponShotgun;
    private GameObject weaponLaser;
    private GameObject weaponCannon;
    private Rigidbody2D rb2d;
    private AudioSource audioSource;
    private Animator anim;

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
                this.weaponDefault.SetActive(true);
                this.weaponShotgun.SetActive(false);
                this.weaponLaser.SetActive(false);
                this.weaponCannon.SetActive(false);
                break;
            case "Shotgun":
                this.weaponDefault.SetActive(false);
                this.weaponShotgun.SetActive(true);
                this.weaponLaser.SetActive(false);
                this.weaponCannon.SetActive(false);
                break;
            case "Laser":
                this.weaponDefault.SetActive(false);
                this.weaponShotgun.SetActive(false);
                this.weaponLaser.SetActive(true);
                this.weaponCannon.SetActive(false);
                break;
            case "Cannon":
                this.weaponDefault.SetActive(false);
                this.weaponShotgun.SetActive(false);
                this.weaponLaser.SetActive(false);
                this.weaponCannon.SetActive(true);
                break;
        }
    }

    private void GetInput()
    {
        this.verticalInput = Input.GetAxisRaw("Vertical");
        this.horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    private void Move()
    {
        this.rb2d.AddRelativeForce(new Vector2(0, -this.verticalInput) * this.moveSpeed);
        this.rb2d.AddTorque(-this.horizontalInput * this.rotationSpeed);
    }

    private void PlayThrusterAudio()
    {
        if (horizontalInput != 0)
        {
            anim.SetTrigger("Thruster");
            audioSource.Play();
        }
        if (horizontalInput == 0)
        {
            audioSource.Stop();
            anim.ResetTrigger("Thruster");
        }
        if (verticalInput != 0)
        {
            anim.SetTrigger("Thruster");
            audioSource.Play();
        }
        if (verticalInput == 0)
        {
            audioSource.Stop();
            anim.ResetTrigger("Thruster");
        }
    }

    private void Awake()
    {
        this.rb2d = GetComponent<Rigidbody2D>();
        this.anim = GetComponent<Animator>();
        this.audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        UIManager.Instance.SetTextPlayerHealth(this.health);
        this.weaponDefault = GameManager.Instance.GetGameObjectWithTag(this.gameObject, "WeaponDefault");
        this.weaponShotgun = GameManager.Instance.GetGameObjectWithTag(this.gameObject, "WeaponShotgun");
        this.weaponLaser = GameManager.Instance.GetGameObjectWithTag(this.gameObject, "WeaponLaser");
        this.weaponCannon = GameManager.Instance.GetGameObjectWithTag(this.gameObject, "WeaponCannon");
        this.SetWeapon("Default");
    }

    private void Update()
    {
        this.GetInput();
    }

    private void FixedUpdate()
    {
        this.Move();
        this.PlayThrusterAudio();
    }
}
