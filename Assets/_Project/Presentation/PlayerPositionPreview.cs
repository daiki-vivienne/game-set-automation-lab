using UnityEngine;

public class PlayerPositionPreview: MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameState game = new GameState(2, 1, Direction.Up);
        transform.position = new Vector3(game.X, game.Y, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
