using UnityEngine;

public class Bullet : Projectile
{
    SpriteRenderer spriteRenderer;
    Color colour = Color.white;

    protected override void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.color = colour;
    }

    protected override void addDamage(IDamagable damagable) {
        if (damagable != null) {
            base.addDamage(damagable);
            Destroy(gameObject);
        }
    }

    public void setInfo(float d, float m, float dt, string t, Color c) {
        base.setInfo(d, m, dt, t);
        colour = c;
    }
}
