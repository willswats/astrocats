using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 100;
    public float moveSpeed = 4f;
    public float rotationSpeed = 0.5f;
    private float verticalInput;
    private float horizontalInput;

    public float hitProtection = 1f;
    private bool availableForAttack;
    private float lastAttackedAt = -9999f;

    public GameObject weaponDefault;
    public GameObject weaponShotgun;
    public GameObject weaponLaser;
    public GameObject weaponCannon;

    private PlayerWeapon playerWeaponShotgun;
    private PlayerWeapon playerWeaponLaser;
    private PlayerWeapon playerWeaponCannon;

    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;

    private AudioSource audioSourceExplosion;
    private AudioSource audioSourceThruster;

    private Animator anim;

    public void DamagePlayer(int damage)
    {
        if (this.availableForAttack == true)
        {
            this.availableForAttack = false;
            this.lastAttackedAt = Time.time;

            this.health -= damage;
            UIManager.Instance.SetTextPlayerHealth(this.health);

            this.audioSourceExplosion.Play();

            if (this.health <= 0)
            {
                this.audioSourceThruster.volume = 0;
                this.rb2d.simulated = false;
                GetComponent<Collider2D>().enabled = false;
                this.spriteRenderer.enabled = false;

                int childCount = this.transform.childCount;
                for (int i = 0; i < childCount; i++)
                {
                    Destroy(transform.GetChild(i).gameObject);
                }

                Destroy(this.gameObject, 2f);
                GameManager.Instance.KillPlayer();
            }
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
                GameManager.Instance.SetCurrentWeapon("Default");
                break;
            case "Shotgun":
                this.weaponDefault.SetActive(false);
                this.weaponShotgun.SetActive(true);
                this.weaponLaser.SetActive(false);
                this.weaponCannon.SetActive(false);
                GameManager.Instance.SetCurrentWeapon("Shotgun");
                break;
            case "Laser":
                this.weaponDefault.SetActive(false);
                this.weaponShotgun.SetActive(false);
                this.weaponLaser.SetActive(true);
                this.weaponCannon.SetActive(false);
                GameManager.Instance.SetCurrentWeapon("Laser");
                break;
            case "Cannon":
                this.weaponDefault.SetActive(false);
                this.weaponShotgun.SetActive(false);
                this.weaponLaser.SetActive(false);
                this.weaponCannon.SetActive(true);
                GameManager.Instance.SetCurrentWeapon("Cannon");
                break;
        }
    }

    public void UpgradeWeapon(string weapon)
    {
        switch (weapon)
        {
            case "Shotgun":
                playerWeaponShotgun.IncreaseFireRate();
                break;
            case "Laser":
                playerWeaponLaser.IncreaseFireRate();
                break;
            case "Cannon":
                playerWeaponCannon.IncreaseFireRate();
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
        if (verticalInput != 0)
        {
            if (!audioSourceThruster.isPlaying)
            {
                audioSourceThruster.Play();
            }
        }
        else
        {
            if (audioSourceThruster.isPlaying)
            {
                audioSourceThruster.Stop();
            }
        }
    }

    private void PlayThrusterAnimation()
    {
        if (verticalInput != 0)
        {
            anim.SetTrigger("Thruster");
        }
        else
        {
            this.anim.speed = 1;
        }
    }

    private void AnimationEventPause()
    {
        this.anim.speed = 0;
    }

    private void Awake()
    {
        this.rb2d = GetComponent<Rigidbody2D>();
        this.anim = GetComponent<Animator>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();

        AudioSource[] audioSources = GetComponents<AudioSource>();
        this.audioSourceThruster = audioSources[0];
        this.audioSourceExplosion = audioSources[1];
    }

    private void Start()
    {
        UIManager.Instance.SetTextPlayerHealth(this.health);
        this.SetWeapon(GameManager.Instance.GetCurrentWeapon());

        playerWeaponShotgun = this.weaponShotgun.GetComponent<PlayerWeapon>();
        playerWeaponLaser = this.weaponLaser.GetComponent<PlayerWeapon>();
        playerWeaponCannon = this.weaponCannon.GetComponent<PlayerWeapon>();
        playerWeaponShotgun.fireRate = 0.4f;
        playerWeaponLaser.fireRate = 0.8f;
        playerWeaponCannon.fireRate = 0.6f;

    }

    private void Update()
    {
        if (!GameManager.Instance.gamePaused)
        {
            this.GetInput();
            this.PlayThrusterAudio();
            this.PlayThrusterAnimation();
        }

        float availableForAttackTime = this.lastAttackedAt + this.hitProtection;
        if (Time.time > availableForAttackTime)
        {
            availableForAttack = true;
        }
    }

    private void FixedUpdate()
    {
        this.Move();
    }
}
