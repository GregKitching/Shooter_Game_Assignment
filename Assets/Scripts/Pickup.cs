using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] protected float deleteTime;
    protected Player player;
    float deleteTimer;

    protected virtual void Start() {
        deleteTimer = deleteTime;
        player = GameManager.getInstance().getPlayer();
    }

    void Update() {
        deleteTimer -= Time.deltaTime;
        if (deleteTimer <= 0.0f) {
            Destroy(gameObject);
        }
    }

    public virtual void OnPickup() {
        Destroy(gameObject);
    }
}
