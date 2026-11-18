using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerPositionPreview: MonoBehaviour
{
    private GameState game;

    // Inspectorで、WallPreviewに付いている地図表示用のコンポーネントを指定する。
    [SerializeField]
    private WallPositionPreview wallpreview;
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
            // ゲーム内の位置を更新したあと、その座標をUnity上のプレイヤーに反映する。
            game.Move(Direction.Right, wallpreview.LevelMap);
            transform.position = new Vector3(game.X, game.Y, 0);
            Debug.Log("Dキーを押した");
        }
    }
}
