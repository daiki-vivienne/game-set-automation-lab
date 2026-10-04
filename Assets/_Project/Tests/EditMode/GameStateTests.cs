using NUnit.Framework;
//ゲームの状態をテストする処理をまとめる
public class GameStateTests
{
    // 指定した位置と向きで始まり、ターン数が0であることを確認する
    [Test]
    public void InitialStateIsSet()
    {
        GameState game = new GameState(1, 1, Direction.Up);
        Assert.That(game.X, Is.EqualTo(1));
        Assert.That(game.Y, Is.EqualTo(1));
        Assert.That(game.Facing, Is.EqualTo(Direction.Up));
        Assert.That(game.TurnCount, Is.EqualTo(0));
    }
}