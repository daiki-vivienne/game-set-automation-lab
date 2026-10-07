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
}    