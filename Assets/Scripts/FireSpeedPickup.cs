using UnityEngine;

public class FireSpeedPickup : Pickup//, IDamagable
{
    [SerializeField] float multiplier;
    [SerializeField] float effectTime;

    public override void OnPickup() {
        player.changeAttackRate(multiplier, effectTime);
        base.OnPickup();
    }

    /*public void GetDamage(float damage) {
        OnPickup();
    }*/
}
