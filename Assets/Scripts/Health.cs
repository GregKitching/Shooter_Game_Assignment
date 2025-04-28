using UnityEngine;
using System;

public class Health
{
    float currentHealth;
    float maxHealth;
    float regenRate;
    public Action<float> onHealthUpdate;
    public Health(float maxHealth, float regenRate) {
        this.maxHealth = maxHealth;
        this.regenRate = regenRate;
        currentHealth = maxHealth;
        onHealthUpdate?.Invoke(currentHealth);
    }
    public Health(float maxHealth, float regenRate, float health) {
        this.maxHealth = maxHealth;
        this.regenRate = regenRate;
        currentHealth  = health;
        onHealthUpdate?.Invoke(currentHealth);
    }

    public void addHealth(float amount) {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        onHealthUpdate?.Invoke(currentHealth);
    }

    public void removeHealth(float amount) {
        currentHealth = Mathf.Max(currentHealth - amount, 0.0f);
        onHealthUpdate?.Invoke(currentHealth);
    }

    public float getHealth() {
        return currentHealth;
    }

    public void regenerateHealth() {
        addHealth(regenRate * Time.deltaTime);
        onHealthUpdate?.Invoke(currentHealth);
    }
}
