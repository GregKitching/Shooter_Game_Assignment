using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    static UIManager instance;
    [SerializeField] Canvas mainCanvas;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI nukesText;
    [SerializeField] TextMeshProUGUI nukeCooldownText;
    [SerializeField] TextMeshProUGUI powerupText;

    [SerializeField] Canvas menuCanvas;
    [SerializeField] GameObject mainGroup;
    [SerializeField] TextMeshProUGUI mainMenuHighScoreText;

    [SerializeField] GameObject instructionsGroup;
    [SerializeField] GameObject optionsGroup;

    [SerializeField] TextMeshProUGUI mainMenuText;
    Player player;

    public enum MenuGroup {
        Main,
        Instructions,
        Options
    }

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

    public void setMainCanvasActive(bool active) {
        mainCanvas.gameObject.SetActive(active);
    }

    public void setMenuCanvasActive(bool active) {
        menuCanvas.gameObject.SetActive(active);
    }

    public void setActiveGroup(MenuGroup group) {
        mainGroup.SetActive(group == MenuGroup.Main);
        instructionsGroup.SetActive(group == MenuGroup.Instructions);
        optionsGroup.SetActive(group == MenuGroup.Options);
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

    public void updateNukes(int nukes, int maxNukes) {
        nukesText.SetText("Nukes: " + nukes.ToString() + "/" + maxNukes.ToString());
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

    public void setMainMenuText(string text) {
        mainMenuText.SetText(text);
    }

    public void setMainMenuHighScore(int score) {
        mainMenuHighScoreText.SetText("High Score: " + score.ToString());
    }
}
