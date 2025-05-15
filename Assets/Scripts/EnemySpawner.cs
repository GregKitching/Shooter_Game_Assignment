using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    static EnemySpawner instance;
    [SerializeField] GameObject meleeEnemyPrefab;
    [SerializeField] GameObject explodingEnemyPrefab;
    [SerializeField] GameObject gunEnemyPrefab;
    [SerializeField] GameObject laserEnemyPrefab;
    [SerializeField] GameObject shockwaveEnemyPrefab;
    [Range(0, 1)][SerializeField] float meleeEnemyStartingSpawnChance;
    [Range(0, 1)][SerializeField] float gunEnemyStartingSpawnChance;
    [Range(0, 1)][SerializeField] float laserEnemyStartingSpawnChance;
    [Range(0, 1)][SerializeField] float explodingEnemyStartingSpawnChance;
    [Range(0, 1)][SerializeField] float shockwaveEnemyStartingSpawnChance;
    List<GameObject> spawnedEnemies = new List<GameObject>();
    bool spawningEnemies = false;
    float spawnTimer = 0.0f;
    float delTime = 5.0f;
    float delTimer = 0.0f;
    float meleeEnemySpawnChance;
    float gunEnemySpawnChance;
    float laserEnemySpawnChance;
    float explodingEnemySpawnChance;
    float shockwaveEnemySpawnChance;
    float spawnTime;

    public static EnemySpawner getInstance() {
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

    // Update is called once per frame
    void Update()
    {
        if (spawningEnemies) {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnTime) {
                float enemyType = Random.Range(0.0f, 1.0f);
                if (enemyType < meleeEnemySpawnChance) {
                    spawnedEnemies.Add(Instantiate(meleeEnemyPrefab, getRandomPositionOnCircle(10.0f), Quaternion.identity));
                } else if (enemyType < gunEnemySpawnChance + meleeEnemySpawnChance) {
                    spawnedEnemies.Add(Instantiate(gunEnemyPrefab, getRandomPositionOnCircle(10.0f), Quaternion.identity));
                } else if (enemyType < laserEnemySpawnChance + gunEnemySpawnChance + meleeEnemySpawnChance) {
                    spawnedEnemies.Add(Instantiate(laserEnemyPrefab, getRandomPositionOnCircle(10.0f), Quaternion.identity));
                } else if (enemyType < explodingEnemySpawnChance + laserEnemySpawnChance + gunEnemySpawnChance + meleeEnemySpawnChance) {
                    spawnedEnemies.Add(Instantiate(explodingEnemyPrefab, getRandomPositionOnCircle(10.0f), Quaternion.identity));
                } else {
                    spawnedEnemies.Add(Instantiate(shockwaveEnemyPrefab, getRandomPositionOnCircle(10.0f), Quaternion.identity));
                }
                spawnTimer -= spawnTime;
                if (spawnTime > 0.2) {
                    spawnTime -= 0.01f;
                }
            }
            delTimer += Time.deltaTime;
            if (delTimer >= delTime) {
                garbageCollect();
                delTimer -= delTime;
            }
        }
    }

    public void setEnemySpawnRatios(float melee, float gun, float laser, float exploding, float shockwave) {
        meleeEnemySpawnChance = melee;
        gunEnemySpawnChance = gun;
        laserEnemySpawnChance = laser;
        explodingEnemySpawnChance = exploding;
        shockwaveEnemySpawnChance = shockwave;
    }

    public void resetEnemySpawnRatios() {
        meleeEnemySpawnChance = meleeEnemyStartingSpawnChance;
        gunEnemySpawnChance = gunEnemyStartingSpawnChance;
        laserEnemySpawnChance = laserEnemyStartingSpawnChance;
        explodingEnemySpawnChance = explodingEnemyStartingSpawnChance;
        shockwaveEnemySpawnChance = shockwaveEnemyStartingSpawnChance;
    }

    public void setSpawningEnemies(bool a) {
        spawningEnemies = a;
    }

    Vector2 getRandomPositionOnCircle(float c) {
        float x = UnityEngine.Random.Range(0.0f, c);
        float x2 = x * x;
        int quadrant = UnityEngine.Random.Range(0, 4);
        float y = Mathf.Sqrt((c * c) - x2);
        switch (quadrant) {
            case 0:
                return new Vector2(x, y);
            case 1:
                return new Vector2(-x, y);
            case 2:
                return new Vector2(x, -y);
            default:
                return new Vector2(-x, -y);
        }

    }

    void garbageCollect() {
        int i = 0;
        while (i < spawnedEnemies.Count) {
            if (spawnedEnemies[i] == null) {
                spawnedEnemies.RemoveAt(i);
            } else {
                i++;
            }
        }
    }

    public void deleteAllEnemies() {
        while (spawnedEnemies.Count > 0) {
            Destroy(spawnedEnemies[0]);
            spawnedEnemies.RemoveAt(0);
        }
    }

    public void killAllEnemies() {
        while (spawnedEnemies.Count > 0) {
            if (spawnedEnemies[0] != null) {
                spawnedEnemies[0].GetComponent<Enemy>().die();
            }
            spawnedEnemies.RemoveAt(0);
        }
    }

    public void removeLasers() {
        foreach (GameObject enemy in spawnedEnemies) {
            if (enemy != null) {
                LaserEnemy laserEnemy = enemy.GetComponent<LaserEnemy>();
                if (laserEnemy != null) {
                    laserEnemy.removeLaser();
                }
            }
        }
    }

    public void setInitialSpawnTime(float t) {
        spawnTime = t;
    }
}
