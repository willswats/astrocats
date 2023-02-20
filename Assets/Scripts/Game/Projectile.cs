using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed = 500f;
    private float lifeTimeSeconds = 10f;
    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Destroy(gameObject, lifeTimeSeconds);
    }

    private void OnCollisionEnter2D(Collision2D collison)
    {
        Destroy(gameObject);
    }

    public void SetForce(Vector2 direction)
    {
        rb2d.AddForce(direction * speed);
    }
}
