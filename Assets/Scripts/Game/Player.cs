using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 100;
    public float moveSpeed = 2f;
    public float rotationSpeed = 0.3f;
    private float verticalInput;
    private float horizontalInput;

    public float hitProtection = 1f;
    private bool availableForAttack;
    private float lastAttackedAt = -9999f;

    public GameObject weaponDefault;
    public GameObject weaponShotgun;
    public GameObject weaponLaser;
    public GameObject weaponCannon;

    // TODO: remove need for these variables
    private PlayerWeapon playerWeaponShotgun;
    private PlayerWeapon playerWeaponLaser;
    private PlayerWeapon playerWeaponCannon;
    private float playerWeaponShotgunInitialWaitFireAmount = 0.6f;
    private float playerWeaponLaserInitialWaitFireAmount = 1f;
    private float playerWeaponCannonInitialWaitFireAmount = 0.8f;

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
            UIManager.Instance.SetTextPlayerHealth(this.health, GameManager.Instance.PlayerLives);

            this.audioSourceExplosion.Play();

            if (this.health <= 0)
            {
                // TODO: refactor
                this.audioSourceThruster.volume = 0;
                this.rb2d.simulated = false;
                this.GetComponent<Collider2D>().enabled = false;
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

    public void GainHealth(int health)
    {
        if ((this.health + health) <= 100)
        {
            this.health += health;
            UIManager.Instance.SetTextPlayerHealth(this.health, GameManager.Instance.PlayerLives);
        }
    }

    public void LevelUp()
    {
        int playerLevel = GameManager.Instance.PlayerLevel;
        int experiencePoints = GameManager.Instance.ExperiencePoints;

        if (experiencePoints >= GameManager.Instance.GetExperiencePointsForLevel(4) && playerLevel == 4)
        {
            UIManager.Instance.ToggleWinMenu();
        }
        else if (experiencePoints >= GameManager.Instance.GetExperiencePointsForLevel(3) && playerLevel == 3)
        {
            GameManager.Instance.PlayerLevel += 1;
            GameManager.Instance.ExperiencePoints = 0;
            SetLevel();
        }
        else if (experiencePoints >= GameManager.Instance.GetExperiencePointsForLevel(2) && playerLevel == 2)
        {
            GameManager.Instance.PlayerLevel += 1;
            GameManager.Instance.ExperiencePoints = 0;
            SetLevel();
        }
        else if (experiencePoints >= GameManager.Instance.GetExperiencePointsForLevel(1) && playerLevel == 1)
        {
            GameManager.Instance.PlayerLevel += 1;
            GameManager.Instance.ExperiencePoints = 0;
            SetLevel();
        }
        else if (experiencePoints >= GameManager.Instance.GetExperiencePointsForLevel(0) && playerLevel == 0)
        {
            GameManager.Instance.PlayerLevel += 1;
            GameManager.Instance.ExperiencePoints = 0;
            SetLevel();
        }
    }

    public void SetLevel()
    {
        int playerLevel = GameManager.Instance.PlayerLevel;

        if (playerLevel == 4)
        {
            this.moveSpeed = 4f;
            this.rotationSpeed = 0.5f;
        }
        else if (playerLevel == 3)
        {
            this.moveSpeed = 3.5f;
            this.rotationSpeed = 0.45f;
        }
        else if (playerLevel == 2)
        {
            this.moveSpeed = 3f;
            this.rotationSpeed = 0.4f;
        }
        else if (playerLevel == 1)
        {
            this.moveSpeed = 2.5f;
            this.rotationSpeed = 0.35f;
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

    public void UpgradeWeapon(string weapon, int amount = 1)
    {
        // Don't upgrade on first pickup and only upgrade to maximum
        int maximumWeaponCount = GameManager.Instance.GetMaximumWeaponCount();
        int weapontCount = GameManager.Instance.GetWeaponCount(weapon);
        if (weapontCount > 1)
        {
            switch (weapon)
            {
                case "Shotgun":
                    float shotgunDecreaseAmount = this.playerWeaponShotgunInitialWaitFireAmount / maximumWeaponCount;
                    playerWeaponShotgun.DecreaseWaitAmount(shotgunDecreaseAmount, amount);
                    break;
                case "Laser":
                    float laserDecreaseAmount = this.playerWeaponLaserInitialWaitFireAmount / maximumWeaponCount;
                    playerWeaponLaser.DecreaseWaitAmount(laserDecreaseAmount, amount);
                    break;
                case "Cannon":
                    float cannonDecreaseAmount = this.playerWeaponCannonInitialWaitFireAmount / maximumWeaponCount;
                    playerWeaponCannon.DecreaseWaitAmount(cannonDecreaseAmount, amount);
                    break;
            }
        }
    }

    private void UpgradeWeaponsToCurrentCount()
    {
        // -1 as we do not upgrade on the first pickup
        this.UpgradeWeapon("Shotgun", GameManager.Instance.GetWeaponCount("Shotgun") - 1);
        this.UpgradeWeapon("Laser", GameManager.Instance.GetWeaponCount("Laser") - 1);
        this.UpgradeWeapon("Cannon", GameManager.Instance.GetWeaponCount("Cannon") - 1);
    }

    private void SetInitialWeaponWaitFireAmount()
    {
        playerWeaponShotgun = this.weaponShotgun.GetComponent<PlayerWeapon>();
        playerWeaponLaser = this.weaponLaser.GetComponent<PlayerWeapon>();
        playerWeaponCannon = this.weaponCannon.GetComponent<PlayerWeapon>();
        playerWeaponShotgun.waitFireAmount = this.playerWeaponShotgunInitialWaitFireAmount;
        playerWeaponLaser.waitFireAmount = this.playerWeaponLaserInitialWaitFireAmount;
        playerWeaponCannon.waitFireAmount = this.playerWeaponCannonInitialWaitFireAmount;
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
        UIManager.Instance.SetTextPlayerHealth(this.health, GameManager.Instance.PlayerLives);
        this.SetWeapon(GameManager.Instance.GetCurrentWeapon());
        this.SetInitialWeaponWaitFireAmount();

        this.UpgradeWeaponsToCurrentCount();
        this.SetLevel();
    }

    private void Update()
    {
        if (!GameManager.Instance.GamePaused)
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
