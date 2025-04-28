using UnityEngine;

public class LaserEnemy : Enemy
{
    [SerializeField] float damageScale;
    [SerializeField] float chargeTime;
    Player player;
    LineRenderer laser;
    bool charging = false;
    bool firing = false;
    float chargeCounter = 0.0f;
    Color chargeColour;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        enemyType = EnemyType.Laser;
        player = GameManager.getInstance().getPlayer();
        laser = GetComponentInChildren<LineRenderer>();
        chargeColour = new Color(0.0f, 0.5f, 0.0f);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (target != null && player != null) {
            if (Vector2.Distance(transform.position, target.position) < distance + 0.2f) {
                laser.SetPosition(1, transform.InverseTransformPoint(target.position));
                if (!charging && firing) {
                    player.GetDamage(damageScale * Time.deltaTime);
                } else if (charging && !firing) {
                    chargeCounter += Time.deltaTime;
                    if (chargeCounter >= chargeTime) {
                        startFiring();
                    }
                } else if (!charging && !firing) {
                    startCharging();
                }
            } else {
                if (charging || firing) {
                    stopChargingOrFiring();
                }
            }
        }
    }

    void startCharging() {
        charging = true;
        firing = false;
        laser.SetPosition(1, transform.InverseTransformPoint(target.position));
        laser.startColor = chargeColour;
        laser.endColor = chargeColour;
    }

    void startFiring() {
        firing = true;
        charging = false;
        chargeCounter = 0.0f;
        laser.startColor = Color.green;
        laser.endColor = Color.green;
    }

    void stopChargingOrFiring() {
        charging = false;
        firing = false;
        chargeCounter = 0.0f;
        laser.SetPosition(1, Vector3.zero);
        laser.startColor = Color.green;
        laser.endColor = Color.green;
    }

    public void removeLaser() {
        stopChargingOrFiring();
    }
}
