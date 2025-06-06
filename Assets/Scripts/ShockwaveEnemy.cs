using UnityEngine;

public class ShockwaveEnemy : Enemy
{
    [SerializeField] float damage;
    [SerializeField] float attackInterval;
    [SerializeField] float attackRange;
    [SerializeField] float projectileSpeed;
    [SerializeField] float projectileLifetime;
    [SerializeField] float growRate;
    [SerializeField] GameObject shockwavePrefab;
    AudioSource audioSource;
    float timer = 0.0f;

    protected override void Start()
    {
        base.Start();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (timer < attackInterval)
        {
            timer += Time.deltaTime;
        }
        if (Vector2.Distance(transform.position, target.position) < attackRange)
        {
            if (timer >= attackInterval)
            {
                fire();
                timer = 0.0f;
            }
        }
    }

    public override void fire() {
        GameObject shockwave = Instantiate(shockwavePrefab, transform.position, transform.rotation);
        shockwave.GetComponent<Shockwave>().setInfo(damage, projectileSpeed, projectileLifetime, "Enemy", growRate);
        audioSource.Play();
    }
    
    public override void GetDamage(float damage) {
        base.GetDamage(damage);
        audioSource.PlayOneShot(enemyDamageClip);
    }
}
