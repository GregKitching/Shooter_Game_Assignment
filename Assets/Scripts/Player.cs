using System;
using UnityEngine;
using UnityEngine.Events;

public class Player : MovingObject
{
    Camera playerCamera;
    [SerializeField] float moveSpeed;
    [SerializeField] float moveBoundX;
    [SerializeField] float moveBoundY;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject nukePrefab;
    [SerializeField] float maxHealth;
    [SerializeField] float regenRate;
    [SerializeField] float attackRate;
    [SerializeField] float nukeCooldown;
    [SerializeField] AudioClip playerShootClip;
    [SerializeField] AudioClip playerHitClip;
    [SerializeField] AudioClip nukeExplodeClip;
    [SerializeField] AudioClip powerupClip;
    float attackCounter = 0.0f;
    float currentAttackRate;
    float attackRateCounter = 0.0f;
    bool attackRateChanged = false;
    int nukes;
    int maxNukes;
    float nukeCounter = 0.0f;
    Rigidbody2D rb;
    AudioSource audioSource;
    public UnityEvent nukeUpdate;
    public Action OnDeath;

    void Awake() {
        playerCamera = Camera.main;
        health = new Health(maxHealth, regenRate, maxHealth);
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        currentAttackRate = attackRate;
    }

    void Start() {
        UIManager.getInstance().updateNukes(nukes, maxNukes);
    }

    void Update() {
        if (attackCounter < currentAttackRate) {
            attackCounter += Time.deltaTime;
        }
        if (nukeCounter > 0.0f) {
            nukeCounter -= Time.deltaTime;
            if (nukeCounter <= 0.0f) {
                nukeCounter = 0.0f;
                UIManager.getInstance().clearNukeCooldownText();
            } else {
                UIManager.getInstance().updateNukeCooldown(nukeCounter);
            }
        }
        if (attackRateCounter > 0.0f) {
            attackRateCounter -= Time.deltaTime;
            if (attackRateCounter <= 0.0f) {
                attackRateCounter = 0.0f;
                currentAttackRate = attackRate;
                attackRateChanged = false;
                UIManager.getInstance().clearPowerupText();
            } else {
                UIManager.getInstance().updatePowerupTime("Firing Speed", attackRateCounter);
            }
        }
        health.regenerateHealth();
    }

    public override void move(Vector2 direction, Vector2 target) {
        rb.linearVelocity = direction * moveSpeed * Time.deltaTime;
        if (transform.position.x > moveBoundX) {
            transform.position = new Vector3(moveBoundX, transform.position.y, 0.0f);
        } else if (transform.position.x < -moveBoundX) {
            transform.position = new Vector3(-moveBoundX, transform.position.y, 0.0f);
        }
        if (transform.position.y > moveBoundY) {
            transform.position = new Vector3(transform.position.x, moveBoundY, 0.0f);
        } else if (transform.position.y < -moveBoundY) {
            transform.position = new Vector3(transform.position.x, -moveBoundY, 0.0f);
        }
        Vector3 playerScreenPos = playerCamera.WorldToScreenPoint(transform.position);
        target.x -= playerScreenPos.x;
        target.y -= playerScreenPos.y;
        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
    }

    public override void fire() {
        if (attackCounter >= currentAttackRate) {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.GetComponent<Bullet>().setInfo(10.0f, 15.0f, 5.0f, "Player");
            attackCounter = 0.0f;
            audioSource.PlayOneShot(playerShootClip);
        }
    }

    public override void attack(float interval) {
        
    }

    public void useNuke() {
        if (nukes > 0 && nukeCounter == 0.0f) {
            Instantiate(nukePrefab, transform.position, Quaternion.identity);
            audioSource.PlayOneShot(nukeExplodeClip);
            nukeCounter = nukeCooldown;
            nukes--;
            UIManager.getInstance().updateNukes(nukes, maxNukes);
        }
    }

    public void addNuke() {
        if (nukes < maxNukes) {
            nukes++;
            UIManager.getInstance().updateNukes(nukes, maxNukes);
        }
    }
    
    public void changeAttackRate(float mult, float timerVal) {
        if (!attackRateChanged) {
            // divide attack timer to multiply attack rate
            currentAttackRate /= mult;
            attackRateChanged = true;
        }
        attackRateCounter = timerVal;
        UIManager.getInstance().updatePowerupTime("Firing Speed", timerVal);
    }

    public override void die() {
        GameManager.getInstance().playPlayerDeathSound();
        EnemySpawner.getInstance().removeLasers();
        GameManager.getInstance().gameStop();
        Destroy(gameObject);
    }

    public bool hasMaxHealth() {
        return health.getHealth() == health.getMaxHealth();
    }

    public bool hasMaxNukes() {
        return nukes == maxNukes;
    }

    public void setNukes(int value) {
        nukes = value;
    }

    public void setMaxNukes(int value) {
        maxNukes = value;
    }

    public override void GetDamage(float damage) {
        base.GetDamage(damage);
        audioSource.PlayOneShot(playerHitClip);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Pickup")) {
            collision.GetComponent<Pickup>().OnPickup();
            audioSource.PlayOneShot(powerupClip);
        }
    }
}
