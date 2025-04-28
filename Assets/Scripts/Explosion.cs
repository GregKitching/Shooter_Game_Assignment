using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float stayTime;
    float damage;
    float timer = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= stayTime) {
            Destroy(gameObject);
        }
    }

    public void setDamage(float d) {
        damage = d;
    }

    void addDamage(IDamagable damagable) {
        if (damagable != null) {
            damagable.GetDamage(damage);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.gameObject.CompareTag("Player")) {
            IDamagable damagable = collision.collider.gameObject.GetComponent<IDamagable>();
            addDamage(damagable);
        }
    }
}
