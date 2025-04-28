using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    static PickupSpawner instance;
    [SerializeField] PickupSpawn[] pickups;
    [Range(0, 1)] [SerializeField] float pickupProbability;
    List<Pickup> pickupPool = new List<Pickup>();
    Pickup chosenPickup;


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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach(PickupSpawn spawn in pickups) {
            for (int i = 0; i < spawn.spawnAmount; i++) {
                pickupPool.Add(spawn.pickup);
            }
        }
    }

    public void spawnPickup(Vector2 spawnPosition) {
        if (pickupPool.Count == 0) {
            return;
        }
        if (UnityEngine.Random.Range(0.0f, 1.0f) < pickupProbability) {
            chosenPickup = pickupPool[UnityEngine.Random.Range(0, pickupPool.Count)];
            Instantiate(chosenPickup, spawnPosition, Quaternion.identity);
        }
    }
}

[System.Serializable]
public struct PickupSpawn {
    public Pickup pickup;
    public int spawnAmount;
}
