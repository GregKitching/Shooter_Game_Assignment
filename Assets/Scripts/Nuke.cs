using NUnit.Framework;
using UnityEngine;

public class Nuke : MonoBehaviour
{
    [SerializeField] float existTime;
    [SerializeField] float killDelay;
    [SerializeField] float growRate;
    [SerializeField] float alphaChangeRate;
    SpriteRenderer spriteRenderer;
    float timer = 0.0f;
    bool activated = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        transform.localScale += new Vector3(growRate, growRate, 0) * Time.deltaTime;
        Color c = Color.white;
        c.a = spriteRenderer.color.a - alphaChangeRate * Time.deltaTime;
        spriteRenderer.color = c;
        if (timer >= killDelay && !activated) {
            EnemySpawner.getInstance().killAllEnemies();
            activated = true;
        } else if (timer >= existTime) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile")) {
            Projectile projectile = collision.GetComponent<Projectile>();
            if (!projectile.isPlayerBullet()) {
                Destroy(collision.gameObject);
            }
        }
    }
}
