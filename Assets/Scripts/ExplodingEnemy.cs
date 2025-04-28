using UnityEngine;

public class ExplodingEnemy : Enemy
{
    [SerializeField] float damage;
    [SerializeField] float attackRange;
    [SerializeField] float explosionWaitTime;
    [SerializeField] GameObject explosionPrefab;
    SpriteRenderer spriteRenderer;
    float timer = 0.0f;
    bool charging = false;
    float colourChangeValue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        enemyType = EnemyType.Explode;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        colourChangeValue = 0.5f / explosionWaitTime;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (charging) {
            charge();
        } else {
            base.Update();
            if (target == null) {
                return;
            }
            if (Vector2.Distance(transform.position, target.position) < attackRange) {
                charging = true;
            }
        }
    }

    void charge() {
        timer += Time.deltaTime;
        float rChange = colourChangeValue * Time.deltaTime;
        float gbChange = rChange / 2;
        spriteRenderer.color = new Color(spriteRenderer.color.r + rChange, spriteRenderer.color.g - gbChange, spriteRenderer.color.b - gbChange);
        if (timer >= explosionWaitTime) {
            timer -= explosionWaitTime;
            charging = false;
            explode();
        }
    }

    void explode() {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        explosion.GetComponent<Explosion>().setDamage(damage);
        Destroy(gameObject);
    }
}
