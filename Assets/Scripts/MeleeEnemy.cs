using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField] float damage;

    protected override void Start() {
        base.Start();
        enemyType = EnemyType.Melee;
    }

    public override void fire() {
        GameManager.getInstance().getPlayer().GetDamage(damage);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            fire();
        }
    }
}
