// ゲームの位置・向き・ターン数を保持し、外から現在の状態を読めるようにする型。
public class GameState
{
    // このGameStateの状態を保存するフィールド。外から直接書き換えさせないためprivateにする。
    private Direction facing;
    // ここで用意した0は、コンストラクターに渡された開始位置に置き換わる。
    private int x = 0;
    private int y = 0;
    // 作った直後のターン数は0。今のコンストラクターでは開始ターン数を受け取らない。
    private int turnCount = 0;
    
    // new GameState(...)で状態を作るときに動くコンストラクター。
    // テストなど作る側が、開始位置と向きを指定できるようにする。
    public GameState(int startX, int startY, Direction startFacing)
    {
        // startXなどは受け取った値の名前。あとからも使えるよう、xなどのフィールドに保存する。
        x = startX;
        y = startY;
        facing = startFacing;
    }
    
    // テストや将来の画面表示が状態を読めるようにする。setを設けず、外からの代入は許可しない。
    // Xを読むとgetが動き、横位置を保存しているxの値をreturnで読み手へ渡す。
    public int X
    {
        get { return x; }
    }
    // 縦位置を読む窓口。同じGameStateが保持しているyの現在の値を返す。
    public int Y
    {
        get { return y; }
    }
    // 位置とは別に、現在向いている方向をDirectionの値として読む窓口。
    public Direction Facing
    {
        get { return facing; }
    }

    // 経過したターン数を読む窓口。作成直後の0もここから確認できる。
    public int TurnCount
    {
        get { return turnCount; }
    }

    // 渡された方向へ移動を試みる。結果はX・Y・Facing・TurnCountから読む。
    // 上下左右に対応。mapは呼び出す側が用意した地図を受け取る。
    public void Move( Direction direction, FixedMap map )
    {
        // 歩けるかどうかに関係なく、入力された方向を向く。
        facing = direction;
        // 地形を確認するまでは現在位置を変えず、別の変数で移動先候補を計算する。
        int nextX = x;
        int nextY = y;
        // Rightという値を受け取った場合、候補のXを1増やすことで右移動を表す。
        if (direction == Direction.Right)
        {
            nextX = nextX + 1;
        }

        // 左はXを減らす。現在位置ではなく、移動先候補だけを変える。
        if (direction == Direction.Left)
        {
            nextX = nextX - 1;
        }

        // 上でYが増える座標方針に合わせ、候補のYを1増やす。
        if (direction == Direction.Up)
        {
            nextY = nextY + 1;
        }

        // 下では候補のYを1減らし、歩けるかの確認は後の共通処理に任せる。
        if (direction == Direction.Down)
        {
            nextY = nextY - 1;
        }
        // 地形の判定は地図に任せる。壁や地図外なら、この中の状態更新を行わない。
        if (map.IsWalkable( nextX, nextY ))
        {
            // 歩けると分かった候補を現在位置に反映し、移動分の1ターンを加える。
            x = nextX;
            y = nextY;
            turnCount = turnCount + 1;
        }
    }

    // 移動せず、指定された方向を向く命令。地形を調べないため地図は受け取らない。
    // 位置とターン数は変更せず、変更後の向きはFacingから読み取る。
    public void ChangeFacing(Direction direction)
    {
        facing = direction;
    }
        
}    