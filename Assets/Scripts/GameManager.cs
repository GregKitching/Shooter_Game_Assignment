using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] UIManager uiManager;
    [SerializeField] GameObject playerPrefab;
    Player player;
    public static GameManager getInstance() {
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

    void Start() {
        uiManager.setMainMenuHighScore(scoreManager.getHighSCore());
    }

    public Player getPlayer() {
        return player;
    }

    public void gameStart() {
        StartCoroutine(GameStart());
    }

    public void gameStop() {
        uiManager.setMainMenuText("Game Over");
        scoreManager.setHighScore();
        StartCoroutine(GameStop());
    }

    public void instructionsButtonPressed() {
        uiManager.setActiveGroup(UIManager.MenuGroup.Instructions);
    }

    public void optionsButtonPressed() {
        uiManager.setActiveGroup(UIManager.MenuGroup.Options);
    }

    public void quitButtonPressed() {
        Application.Quit();
    }

    public void backButtonPressed() {
        uiManager.setMainMenuText("Shooter Game");
        uiManager.setActiveGroup(UIManager.MenuGroup.Main);
    }

    IEnumerator GameStart() {
        yield return new WaitForSeconds(0.5f);
        player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity).GetComponent<Player>();
        uiManager.setPlayer(player);
        scoreManager.resetScore();
        uiManager.updateScore(0);
        uiManager.updateHighScore(ScoreManager.getInstance().getHighSCore());
        uiManager.setMenuCanvasActive(false);
        uiManager.setMainCanvasActive(true);
        enemySpawner.setSpawningEnemies(true);
    }

    IEnumerator GameStop() {
        enemySpawner.setSpawningEnemies(false);
        yield return new WaitForSeconds(2.0f);
        scoreManager.setHighScore();
        uiManager.setMainCanvasActive(false);
        uiManager.setMenuCanvasActive(true);
        uiManager.setMainMenuHighScore(scoreManager.getHighSCore());
        enemySpawner.deleteAllEnemies();
    }
}
