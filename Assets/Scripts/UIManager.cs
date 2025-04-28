using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    static UIManager instance;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI nukesText;
    [SerializeField] TextMeshProUGUI nukeCooldownText;
    [SerializeField] TextMeshProUGUI powerupText;
    [SerializeField] Canvas menuCanvas;
    [SerializeField] TextMeshProUGUI gameOverText;
    Player player;

    public static UIManager getInstance() {
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

    public void setPlayer(Player p) {
        player = p;
        player.health.onHealthUpdate += updateHealth;
    }

    public void setMenuCanvasActive(bool active) {
        menuCanvas.gameObject.SetActive(active);
    }

    void OnDisable() {
        if (player != null) {
            player.health.onHealthUpdate -= updateHealth;
        }
    }

    public void updateHealth(float health) {
        healthText.SetText("HP: " + health.ToString());
    }

    public void updateScore(int score) {
        scoreText.SetText("Score: " + score.ToString());
    }

    public void updateHighScore(int score) {
        highScoreText.SetText("High Score: " + score.ToString());
    }

    public void updateNukes(int nukes) {
        nukesText.SetText("Nukes: " + nukes.ToString());
    }

    public void updateNukeCooldown(float time) {
        nukeCooldownText.SetText("Cooldown: " + time.ToString());
    }

    public void clearNukeCooldownText() {
        nukeCooldownText.SetText("");
    }

    public void updatePowerupTime(string powerup, float time) {
        powerupText.SetText(powerup + ": " + time.ToString());
    }

    public void clearPowerupText() {
        powerupText.SetText("");
    }

    public void setGameOverText(string text) {
        gameOverText.SetText(text);
    }
}
