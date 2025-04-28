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

    public Player getPlayer() {
        return player;
    }

    public void gameStart() {
        StartCoroutine(GameStart());
    }

    public void gameStop() {
        uiManager.setGameOverText("Game Over");
        scoreManager.setHighScore();
        StartCoroutine(GameStop());
    }

    IEnumerator GameStart() {
        yield return new WaitForSeconds(0.5f);
        player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity).GetComponent<Player>();
        uiManager.setPlayer(player);
        scoreManager.resetScore();
        uiManager.updateScore(0);
        uiManager.updateHighScore(ScoreManager.getInstance().getHighSCore());
        uiManager.setMenuCanvasActive(false);
        enemySpawner.setSpawningEnemies(true);
    }

    IEnumerator GameStop() {
        enemySpawner.setSpawningEnemies(false);
        yield return new WaitForSeconds(2.0f);
        scoreManager.setHighScore();
        uiManager.setMenuCanvasActive(true);
        enemySpawner.deleteAllEnemies();
    }
}
