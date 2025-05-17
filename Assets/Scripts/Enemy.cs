using UnityEngine;

public class Enemy : MovingObject
{
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float speed;
    [SerializeField] protected bool maintainDistanceFromPlayer;
    [SerializeField] protected float distance;
    [SerializeField] protected int scoreValue;
    [SerializeField] protected AudioClip enemyDamageClip;
    protected Transform target;
    protected EnemyType enemyType;

    protected virtual void Start()
    {
        health = new Health(maxHealth, 0);
        target = GameManager.getInstance().getPlayer().transform;
    }

    protected virtual void Update()
    {
        if (target != null)
        {
            move(target.position);
        }
        else
        {
            move(speed);
        }
    }

    public override void move(Vector2 direction, Vector2 target)
    {

    }

    public override void move(Vector2 direction)
    {
        direction.x -= transform.position.x;
        direction.y -= transform.position.y;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
        if (maintainDistanceFromPlayer)
        {
            transform.Translate((target.position - transform.position + getClosestFiringPosition()).normalized * speed * Time.deltaTime, Space.World);
        }
        else
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    Vector3 getClosestFiringPosition()
    {
        return (transform.position - target.position).normalized * distance;
    }

    // Use if there is no player in the scene
    public override void move(float speed)
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    public override void attack(float interval)
    {

    }

    public override void fire()
    {

    }

    public override void die()
    {
        ScoreManager.getInstance().increaseScore(scoreValue);
        PickupSpawner.getInstance().spawnPickup(transform.position);
        GameManager.getInstance().playEnemyDeathSound();
        Destroy(gameObject);
    }

    public EnemyType GetEnemyType()
    {
        return enemyType;
    }
}
