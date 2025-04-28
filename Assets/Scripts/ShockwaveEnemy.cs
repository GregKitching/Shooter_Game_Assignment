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
    float timer = 0.0f;
    float attackTimer = 0.0f;

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (timer < attackInterval) {
            timer += Time.deltaTime;
        }
        if (Vector2.Distance(transform.position, target.position) < attackRange) {
            if (timer >= attackInterval) {
                fire();
                timer = 0.0f;
            }
        }
    }

    public override void fire() {
        GameObject shockwave = Instantiate(shockwavePrefab, transform.position, transform.rotation);
        shockwave.GetComponent<Shockwave>().setInfo(damage, projectileSpeed, projectileLifetime, "Enemy", growRate);
    }
}
