using UnityEngine;

public class Shockwave : Projectile
{
    LineRenderer lineRenderer;
    BoxCollider2D boxCollider;
    float growRate;
    bool hitPlayer = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame

    protected override void Update()
    {
        base.Update();
        lineRenderer.SetPosition(0, lineRenderer.GetPosition(0) + new Vector3(0.0f, growRate / 2 * Time.deltaTime, 0.0f));
        lineRenderer.SetPosition(1, lineRenderer.GetPosition(1) - new Vector3(0.0f, growRate / 2 * Time.deltaTime, 0.0f));
        boxCollider.size += new Vector2(0.0f, growRate * Time.deltaTime);
        
    }

    protected override void addDamage(IDamagable damagable) {
        if (damagable != null && !hitPlayer) {
            base.addDamage(damagable);
        }
    }

    public void setInfo(float d, float m, float dt, string t, float gr) {
        base.setInfo(d, m, dt, t);
        growRate = gr;
    }
}
