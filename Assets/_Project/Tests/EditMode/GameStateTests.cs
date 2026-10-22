// AssertやTestなど、NUnitのテスト用の名前を短く使えるようにする。
using NUnit.Framework;
// GameStateを作って状態を読み、期待値と一致するか確かめるテストをまとめる。
public class GameStateTests
{
    // 作成直後に指定した位置・向きと、ターン数0を読み取れることを確認する。
    // [Test]は実行対象の目印。テストの成功・失敗はNUnitが記録する。
    [Test]
    public void InitialStateIsSet()
    {
        // (1, 1, Up)をコンストラクターへ渡して状態を作り、変数gameで扱う。
        GameState game = new GameState(1, 1, Direction.Up);
        // 各プロパティから実際の値を読み、Is.EqualToに書いた期待値と比較する。
        Assert.That(game.X, Is.EqualTo(1));
        Assert.That(game.Y, Is.EqualTo(1));
        Assert.That(game.Facing, Is.EqualTo(Direction.Up));
        // 開始ターン数は渡していないが、GameStateが0で用意していることも確認する。
        Assert.That(game.TurnCount, Is.EqualTo(0));
    }

    // 右の床へ移動すると、位置・向き・ターン数がそろって更新されることを確認する。
    [Test]
    public void MoveRightOnFloorUpdatesState()
    {
        // 開始位置(1, 1)と、その右隣(2, 1)が床になる地図を用意する。
        string[] mapRows = new string[] { "#####", "#...#", "#####" };
        FixedMap map = new FixedMap(mapRows);
        // 上向きで開始し、右への入力によって向きも変わることを確かめる。
        GameState game = new GameState(1, 1, Direction.Up);
        // gameのMoveへ方向と地図を渡す。Moveの中では、この地図を引数mapとして使う。
        game.Move(Direction.Right, map);
        // 移動後のgameを読み、横位置2・縦位置1・右向き・1ターンを確認する。
        Assert.That(game.X, Is.EqualTo(2));
        Assert.That(game.Y, Is.EqualTo(1));
        Assert.That(game.Facing, Is.EqualTo(Direction.Right));
        Assert.That(game.TurnCount, Is.EqualTo(1));
    }

    // 左の床への移動で、位置・向き・ターン数が更新されることを確認する。
    [Test]
    public void MoveLeftOnFloorUpdatesState()
    {
        // 開始位置(2, 1)と左隣(1, 1)が床になる地図を用意する。
        string[] mapRows = new string[] { "#####", "#...#", "#####" };
        FixedMap map = new FixedMap(mapRows);
        GameState game = new GameState(2, 1, Direction.Up);
        game.Move(Direction.Left, map);
        // 移動後の位置(1, 1)・左向き・1ターンを、同じgameから読み取る。
        Assert.That(game.X, Is.EqualTo(1));
        Assert.That(game.Y, Is.EqualTo(1));
        Assert.That(game.Facing, Is.EqualTo(Direction.Left));
        Assert.That(game.TurnCount, Is.EqualTo(1));
    }

    // 上の床への移動で、Xは変わらずYが増え、向きとターン数も変わることを確認する。
    [Test]
    public void MoveUpOnFloorUpdatesState()
    {
        // 左右用の地図より1行増やし、(2, 1)と(2, 2)を縦に隣り合う床にする。
        string[] mapRows = new string[] { "#####", "#...#", "#...#", "#####" };
        FixedMap map = new FixedMap(mapRows);
        GameState game = new GameState(2, 1, Direction.Right);
        game.Move(Direction.Up, map);
        // 移動後の位置(2, 2)・上向き・1ターンを確認する。
        Assert.That(game.X, Is.EqualTo(2));
        Assert.That(game.Y, Is.EqualTo(2));
        Assert.That(game.Facing, Is.EqualTo(Direction.Up));
        Assert.That(game.TurnCount, Is.EqualTo(1));
    }

    // 下の床への移動で、Xは変わらずYが減り、向きとターン数も変わることを確認する。
    [Test]
    public void MoveDownOnFloorUpdatesState()
    {
        // 上移動と同じ配置を使い、今度は(2, 2)から(2, 1)へ移動させる。
        string[] mapRows = new string[] { "#####", "#...#", "#...#", "#####" };
        FixedMap map = new FixedMap(mapRows);
        GameState game = new GameState(2, 2, Direction.Up);
        game.Move(Direction.Down, map);
        // 移動後の位置(2, 1)・下向き・1ターンを確認する。
        Assert.That(game.X, Is.EqualTo(2));
        Assert.That(game.Y, Is.EqualTo(1));
        Assert.That(game.Facing, Is.EqualTo(Direction.Down));
        Assert.That(game.TurnCount, Is.EqualTo(1));
    }

    // 壁への移動入力でも向きは変わり、位置とターン数は変わらないことを確認する。
    [Test]
    public void MoveIntoWallOnlyChangesFacing()
    {
        // (3, 1)は床、右隣の(4, 1)は壁となる配置で移動を試す。
        string[] mapRows = new string[] { "#####", "#...#", "#...#", "#####" };
        FixedMap map = new FixedMap(mapRows);
        GameState game = new GameState(3, 1, Direction.Up);
        game.Move(Direction.Right, map);
        Assert.That(game.X, Is.EqualTo(3));
        Assert.That(game.Y, Is.EqualTo(1));
        Assert.That(game.Facing, Is.EqualTo(Direction.Right));
        Assert.That(game.TurnCount, Is.EqualTo(0));
    }

    // 地図外への移動入力でも向きだけ変わり、位置とターン数が維持されることを確認する。
    [Test]
    public void MoveOutsideMapOnlyChangesFacing()
    {
        // 地図外の判定を直接確認するため、Y=1の行の右端(4, 1)を床にする。
        string[] mapRows = new string[] { "#####", "#...#", "#....", "#####" };
        FixedMap map = new FixedMap(mapRows);
        GameState game = new GameState(4, 1, Direction.Up);
        game.Move(Direction.Right, map);
        Assert.That(game.X, Is.EqualTo(4));
        Assert.That(game.Y, Is.EqualTo(1));
        Assert.That(game.Facing, Is.EqualTo(Direction.Right));
        Assert.That(game.TurnCount, Is.EqualTo(0));
    }

    // 向きだけ変える命令は、地図や移動を必要とせず、位置とターン数を維持する。
    [Test]
    public void ChangeFacingKeepsPositionAndTurnCount()
    {
        GameState game = new GameState(2, 1, Direction.Up);
        game.ChangeFacing(Direction.Right);
        // 向きはUpからRightへ変わり、開始位置(2, 1)と0ターンはそのままである。
        Assert.That(game.X, Is.EqualTo(2));
        Assert.That(game.Y, Is.EqualTo(1));
        Assert.That(game.Facing, Is.EqualTo(Direction.Right));
        Assert.That(game.TurnCount, Is.EqualTo(0));

    }


}