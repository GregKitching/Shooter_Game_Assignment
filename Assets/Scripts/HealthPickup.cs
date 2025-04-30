using UnityEngine;

public class HealthPickup : Pickup//, IDamagable
{
    //[SerializeField] float healthMin;
    //[SerializeField] float healthMax;
    [SerializeField] float restoredHealth;

    public override void OnPickup() {
        //float health = UnityEngine.Random.Range(healthMin, healthMax);
        player.health.addHealth(restoredHealth);
        base.OnPickup();
    }

    /*public void GetDamage(float damage) {
        OnPickup();
    }*/
}
