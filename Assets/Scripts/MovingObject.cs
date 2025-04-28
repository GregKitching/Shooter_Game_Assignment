using Unity.Mathematics;
using UnityEditor.Callbacks;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour, IDamagable
{
    public Health health;
    public virtual void move(Vector2 direction) {

    }
    public virtual void move(float speed) {

    }
    /// <summary>
    /// AAAAAAAAAAAAAAAAAAAAAAAAAAA
    /// </summary>
    /// <param name="direction">Vector2 direction</param>
    public abstract void move(Vector2 direction, Vector2 target);
    public abstract void fire();
    public abstract void attack(float interval);
    public abstract void die();
    public virtual void GetDamage(float damage) {
        health.removeHealth(damage);
        if (health.getHealth() <= 0.0f) {
            die();
        }
    }
}
