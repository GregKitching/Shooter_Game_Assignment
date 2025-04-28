using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    protected Player player;

    protected virtual void Start() {
        player = GameManager.getInstance().getPlayer();
    }

    public virtual void OnPickup() {
        Destroy(gameObject);
    }
}
