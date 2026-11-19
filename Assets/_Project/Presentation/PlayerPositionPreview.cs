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
        // 箱の大きさはシーンのScaleで設定し、表示のZは箱が床より手前に見えるようにずらす。
        // -0.5f の末尾の f は、Vector3 に渡す数値を float 型として扱うための印。
        transform.position = new Vector3(game.X, game.Y, -0.5f);
        
    }

    // Update is called once per frame
    void Update()
    {
        Keyboard keyboard = Keyboard.current;
        // 押しっぱなしで毎フレーム進まないよう、押した瞬間だけ移動を試す。
        // 行き先が壁なら、GameStateが保持している元の座標をそのまま表示する。
        if (keyboard != null && keyboard.dKey.wasPressedThisFrame)
        {
            // ゲーム内の位置を更新したあと、その座標をUnity上のプレイヤーに反映する。
            game.Move(Direction.Right, wallpreview.LevelMap);
            transform.position = new Vector3(game.X, game.Y, -0.5f);
            Debug.Log("Dキーを押した");
        }
        if (keyboard != null && keyboard.aKey.wasPressedThisFrame)
        {
            // ゲーム内の位置を更新したあと、その座標をUnity上のプレイヤーに反映する。
            game.Move(Direction.Left, wallpreview.LevelMap);
            transform.position = new Vector3(game.X, game.Y, -0.5f);
            Debug.Log("Aキーを押した");
        }
        if (keyboard != null && keyboard.wKey.wasPressedThisFrame)
        {
            // ゲーム内の位置を更新したあと、その座標をUnity上のプレイヤーに反映する。
            game.Move(Direction.Up, wallpreview.LevelMap);
            transform.position = new Vector3(game.X, game.Y, -0.5f);
            Debug.Log("Wキーを押した");
        }
        if (keyboard != null && keyboard.sKey.wasPressedThisFrame)
        {
            // ゲーム内の位置を更新したあと、その座標をUnity上のプレイヤーに反映する。
            game.Move(Direction.Down, wallpreview.LevelMap);
            transform.position = new Vector3(game.X, game.Y, -0.5f);
            Debug.Log("Sキーを押した");
        }
    }
}
