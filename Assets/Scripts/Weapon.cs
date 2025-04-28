using UnityEngine;

public class Weapon
{
    string name;
    float damage;
    float moveSpeed;
    public Weapon(string n, float d, float s) {
        name = n;
        damage = d;
        moveSpeed = s;
    }
    public void fire(Bullet bullet, MovingObject player) {
        
    }

    public float GetDamage() {
        return damage;
    }
}
