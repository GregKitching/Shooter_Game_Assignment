using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    static PickupSpawner instance;
    [Range(0, 1)] [SerializeField] float pickupProbability;
    [SerializeField] GameObject healthPickupPrefab;
    [SerializeField] GameObject firingSpeedPickupPrefab;
    [SerializeField] GameObject nukePickupPrefab;
    [SerializeField] float healthProbability;
    [SerializeField] float firingSpeedProbability;
    [SerializeField] float nukeProbability;

    public static PickupSpawner getInstance() {
        return instance;
    }
    
    void createSingleton() {
        if (instance != null && instance != this) {
            Destroy(this);
        }
        instance = this;
    }

    void Awake() {
        createSingleton();
    }

    public void spawnPickup(Vector2 spawnPosition) {
        if (UnityEngine.Random.Range(0.0f, 1.0f) < pickupProbability) {
            float selection = UnityEngine.Random.Range(0.0f, 1.0f);
            if (selection < healthProbability) {
                if (!GameManager.getInstance().getPlayer().hasMaxHealth()) {
                    Instantiate(healthPickupPrefab, spawnPosition, Quaternion.identity);
                }
            } else if (selection < firingSpeedProbability + healthProbability) {
                Instantiate(firingSpeedPickupPrefab, spawnPosition, Quaternion.identity);
            } else {
                if (!GameManager.getInstance().getPlayer().hasMaxNukes()) {
                    Instantiate(nukePickupPrefab, spawnPosition, Quaternion.identity);
                }
            }
        }
    }
}
