using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    static ScoreManager instance;
    int score;
    int highScore;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetScore() {
        score = 0;
    }

    public int getScore() {
        return score;
    }

    public int getHighSCore() {
        return highScore;
    }

    public void increaseScore(int s) {
        score += s;
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
