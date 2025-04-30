using UnityEngine;
using UnityEngine.Events;

public class NukePickup : Pickup//, IDamagable
{
    public override void OnPickup()
    {
        player.addNuke();
        base.OnPickup();
    }

    /*public void GetDamage(float damage) {
        OnPickup();
    }*/
}
