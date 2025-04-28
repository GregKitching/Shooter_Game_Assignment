using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected float damage;
    protected float moveSpeed;
    protected string ignoredTag;
    protected float destroyTime;
    protected float counter = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        move();
        counter += Time.deltaTime;
        if (counter >= destroyTime) {
            Destroy(gameObject);
        }
    }

    protected virtual void move() {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }

    protected virtual void addDamage(IDamagable damagable) {
        if (damagable != null) {
            damagable.GetDamage(damage);
        }
    }

    public void setInfo(float d, float m, float dt, string t) {
        damage = d;
        moveSpeed = m;
        destroyTime = dt;
        ignoredTag = t;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag(ignoredTag)) {
            IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();
            addDamage(damagable);
        }
    }
}
