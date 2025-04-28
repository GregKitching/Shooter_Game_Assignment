using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Player player;
    float horizontal;
    float vertical;
    Vector2 lookTarget;
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
    }

    void FixedUpdate() {
        player.move(new Vector2(horizontal, vertical), lookTarget);
    }
}
