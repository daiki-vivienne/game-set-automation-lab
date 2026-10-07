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
}