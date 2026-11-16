using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerPositionPreview: MonoBehaviour
{
    private GameState game;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        game = new GameState(2, 1, Direction.Up);
        transform.position = new Vector3(game.X, game.Y, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard != null && keyboard.dKey.wasPressedThisFrame)
        {
            Debug.Log("Dキーを押した");
        }
    }
}
