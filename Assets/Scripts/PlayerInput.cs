using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Player player;
    float horizontal;
    float vertical;
    Vector2 lookTarget;
    bool paused = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        lookTarget = Input.mousePosition;
        if (Input.GetMouseButton(0)) {
            player.fire();
        }
        if (Input.GetMouseButtonDown(1)) {
            player.useNuke();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused) {
                Time.timeScale = 1.0f;
                UIManager.getInstance().setPauseTextActive(false);
                paused = false;
            } else {
                Time.timeScale = 0.0f;
                UIManager.getInstance().setPauseTextActive(true);
                paused = true;
            }
        }
    }

    void FixedUpdate() {
        player.move(new Vector2(horizontal, vertical), lookTarget);
    }
}
