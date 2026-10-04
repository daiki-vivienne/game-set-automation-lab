// ゲームの現在の状態と、それを変更する処理をまとめる
public class GameState
{
    private Direction facing;
    private int x = 0;
    private int y = 0;
    private int turnCount = 0;
    
    // テストごとに開始位置と向きを指定できるようにする。
    public GameState(int startX, int startY, Direction startFacing)
    {
        x = startX;
        y = startY;
        facing = startFacing;
    }
    
    // テストから位置を確認できるようにし、外からの書き換えは許可しない。
    public int X
    {
        get { return x; }
    }
    public int Y
    {
        get { return y; }
    }
    public Direction Facing
    {
        get { return facing; }
    }

    public int TurnCount
    {
        get { return turnCount; }
    }
}    