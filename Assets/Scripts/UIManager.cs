using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    static UIManager instance;

    [Header("Gameplay Canvas")]
    [SerializeField] Canvas mainCanvas;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI nukesText;
    [SerializeField] TextMeshProUGUI nukeCooldownText;
    [SerializeField] TextMeshProUGUI powerupText;
    [SerializeField] TextMeshProUGUI pauseText;

    [Header("Main Menu Canvas")]
    [SerializeField] Canvas menuCanvas;
    [SerializeField] TextMeshProUGUI mainMenuText;
    [SerializeField] GameObject mainGroup;
    [SerializeField] TextMeshProUGUI mainMenuHighScoreText;

    [Header("Instructions Canvas")]
    [SerializeField] GameObject instructionsGroup;
    [SerializeField] GameObject optionsGroup;

    [Header("Options Canvas")]
    [SerializeField] Slider startingNukesSlider;
    [SerializeField] TextMeshProUGUI startingNukesSliderText;
    [SerializeField] Slider maxNukesSlider;
    [SerializeField] TextMeshProUGUI maxNukesSliderText;
    [SerializeField] Slider initialEnemySpawnRateSlider;
    [SerializeField] TextMeshProUGUI initialEnemySpawnRateSliderText;

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

    public void setPauseTextActive(bool active) {
        pauseText.gameObject.SetActive(active);
    }

    public void setMainMenuText(string text) {
        mainMenuText.SetText(text);
    }

    public void setMainMenuHighScore(int score) {
        mainMenuHighScoreText.SetText("High Score: " + score.ToString());
    }

    public void updateStartingNukes() {
        int nukes = (int)startingNukesSlider.value;
        startingNukesSliderText.text = nukes.ToString();
        PlayerPrefs.SetInt("StartingNukes", nukes);
    }

    public void setStartingNukesSlider(int nukes) {
        startingNukesSliderText.text = nukes.ToString();
        startingNukesSlider.value = nukes;
    }

    public int getStartingNukes() {
        return (int)startingNukesSlider.value;
    }

    public void updateMaxNukes() {
        int nukes = (int)maxNukesSlider.value;
        maxNukesSliderText.text = nukes.ToString();
        PlayerPrefs.SetInt("MaxNukes", nukes);
        updateStartingNukesFromMaxNukes(nukes);
        
    }

    public void setMaxNukesSlider(int nukes) {
        maxNukesSliderText.text = nukes.ToString();
        maxNukesSlider.value = nukes;
        updateStartingNukesFromMaxNukes(nukes);
    }

    void updateStartingNukesFromMaxNukes(int nukes) {
        if (nukes < (int)startingNukesSlider.maxValue) {
            startingNukesSlider.maxValue = nukes;
            if (nukes < (int)startingNukesSlider.value) {
                startingNukesSlider.value = nukes;
                PlayerPrefs.SetInt("StartingNukes", nukes);
            }
        } else if (nukes > startingNukesSlider.maxValue) {
            startingNukesSlider.maxValue = nukes;
        }
    }

    public int getMaxNukes() {
        return (int)maxNukesSlider.value;
    }

    public void updateInitialEnemySpawnRate() {
        float rate = initialEnemySpawnRateSlider.value;
        initialEnemySpawnRateSliderText.text = rate.ToString();
        PlayerPrefs.SetFloat("InitialEnemySpawnRate", rate);
    }

    public void setInitialEnemySpawnRateSlider(float rate) {
        initialEnemySpawnRateSliderText.text = rate.ToString();
        initialEnemySpawnRateSlider.value = rate;
    }

    public float getInitialEnemySpawnRate() {
        return initialEnemySpawnRateSlider.value;
    }
}
