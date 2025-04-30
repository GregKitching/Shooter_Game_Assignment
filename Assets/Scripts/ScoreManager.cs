using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    static ScoreManager instance;
    int score;
    int highScore;
    int currentEnemyLevel;
    EnemyLevel[] enemyLevels = new EnemyLevel[5];

    public static ScoreManager getInstance() {
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
    void Start() {
        score = 0;
        highScore = PlayerPrefs.GetInt("HighScore");
        UIManager.getInstance().updateScore(0);
        UIManager.getInstance().updateHighScore(highScore);
        // TODO: don't have this hard coded
        enemyLevels[0] = new EnemyLevel(10, 1.0f, 0.0f, 0.0f, 0.0f, 0.0f);
        enemyLevels[1] = new EnemyLevel(50, 0.9f, 0.1f, 0.0f, 0.0f, 0.0f);
        enemyLevels[2] = new EnemyLevel(180, 0.7f, 0.2f, 0.1f, 0.0f, 0.0f);
        enemyLevels[3] = new EnemyLevel(300, 0.4f, 0.3f, 0.2f, 0.1f, 0.0f);
        enemyLevels[4] = new EnemyLevel(500, 0.1f, 0.4f, 0.2f, 0.2f, 0.1f);
    }

    public void resetScore() {
        score = 0;
        currentEnemyLevel = 0;
    }

    public int getScore() {
        return score;
    }

    public int getHighSCore() {
        return highScore;
    }

    public void increaseScore(int s) {
        score += s;
        if (currentEnemyLevel < enemyLevels.Length - 1 && score >= enemyLevels[currentEnemyLevel].nextLevelScore) {
            currentEnemyLevel++;
            EnemyLevel e = enemyLevels[currentEnemyLevel];
            EnemySpawner.getInstance().setEnemySpawnRatios(e.meleeEnemySpawnChance, e.gunEnemySpawnChance, e.laserEnemySpawnChance, e.explodingEnemySpawnChance, e.shockwaveEnemySpawnChance);
        }
        if (score > highScore) {
            highScore = score;
            UIManager.getInstance().updateHighScore(highScore);
        }
        UIManager.getInstance().updateScore(score);
    }

    public void setHighScore() {
        PlayerPrefs.SetInt("HighScore", highScore);
    }
}

public class EnemyLevel {
    public int nextLevelScore;
    public float meleeEnemySpawnChance;
    public float gunEnemySpawnChance;
    public float laserEnemySpawnChance;
    public float explodingEnemySpawnChance;
    public float shockwaveEnemySpawnChance;
    public EnemyLevel(int n, float m, float g, float l, float e, float s) {
        nextLevelScore = n;
        meleeEnemySpawnChance = m;
        gunEnemySpawnChance = g;
        laserEnemySpawnChance = l;
        explodingEnemySpawnChance = e;
        shockwaveEnemySpawnChance = s;
    }
}
