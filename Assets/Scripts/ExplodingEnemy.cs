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
    AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        enemyType = EnemyType.Explode;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        colourChangeValue = 0.5f / explosionWaitTime;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (charging)
        {
            charge();
        }
        else
        {
            base.Update();
            if (target == null)
            {
                return;
            }
            if (Vector2.Distance(transform.position, target.position) < attackRange)
            {
                charging = true;
                audioSource.Play();
            }
        }
    }

    void charge()
    {
        timer += Time.deltaTime;
        float rChange = colourChangeValue * Time.deltaTime;
        float gbChange = rChange / 2;
        spriteRenderer.color = new Color(spriteRenderer.color.r + rChange, spriteRenderer.color.g - gbChange, spriteRenderer.color.b - gbChange);
        if (timer >= explosionWaitTime)
        {
            timer -= explosionWaitTime;
            charging = false;
            explode();
        }
    }

    void explode() {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        explosion.GetComponent<Explosion>().setDamage(damage);
        audioSource.Stop();
        Destroy(gameObject);
    }
    
    public override void GetDamage(float damage) {
        base.GetDamage(damage);
        audioSource.PlayOneShot(enemyDamageClip);
    }
}
