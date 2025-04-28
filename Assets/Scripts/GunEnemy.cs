using UnityEngine;

public class GunEnemy : Enemy
{
    [SerializeField] float bulletDamage;
    [SerializeField] float bulletSpeed;
    [SerializeField] float attackRange;
    [SerializeField] float attackInterval;
    [SerializeField] GameObject bulletPrefab;
    float timer = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        enemyType = EnemyType.Gunner;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (timer < attackInterval) {
            timer += Time.deltaTime;
        }
        if (target != null) {
            if (Vector2.Distance(transform.position, target.position) < attackRange) {
                if (timer >= attackInterval) {
                    fire();
                    timer = 0.0f;
                }
            }
        }
    }

    public override void fire() {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.GetComponent<Bullet>().setInfo(bulletDamage, bulletSpeed, 5.0f, "Enemy", new Color(0.0f, 0.75f, 1.0f));
    }
}
