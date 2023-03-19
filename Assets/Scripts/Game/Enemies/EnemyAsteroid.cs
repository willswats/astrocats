using UnityEngine;

public class EnemyAsteroid : Enemy
{
    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;

    public override void Awake()
    {
        base.Awake();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void Start()
    {
        base.Start();
        this.RandomiseSprite();
    }

    private void RandomiseSprite()
    {
        this.spriteRenderer.sprite = this.sprites[Random.Range(0, this.sprites.Length)];
    }
}
