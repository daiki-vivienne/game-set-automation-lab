using NUnit.Framework;

// 地図を渡したあと、読み取れる情報が期待どおりか確認するテストをまとめる。
public class FixedMapTest
{
    // 渡した地図の高さと横幅を、期待どおりに読み取れることを確認する。
    [Test]
    public void HeightAndWidthAreCorrect()

    {
        // 高さが3横幅が5だと分かるテスト用の地図を、1行ずつ文字列にして配列へまとめる。
        string[] mapRows = new string[] { "#####", "#...#", "#####" };
        // 用意した地図を渡し、読み取りを確かめる対象のFixedMapを作る。
        FixedMap map = new FixedMap( mapRows );
        Assert.That( map.Height, Is.EqualTo( 3 ) );
        Assert.That( map.Width, Is.EqualTo( 5 ) );
    }
    //端の壁マスも、地図の中に含まれることを確認する。
    [Test]
    public void EdgePositionsAreInsideMap()
    {
        // 高さが3横幅が5だと分かるテスト用の地図を、1行ずつ文字列にして配列へまとめる。
        string[] mapRows = new string[] { "#####", "#...#", "#####" };
        // 用意した地図を渡し、読み取りを確かめる対象のFixedMapを作る。
        FixedMap map = new FixedMap( mapRows );
        
        // 左右上下の端がtrue（範囲内）かAssertで確認する。
        Assert.That( map.IsInside( 0, 0 ), Is.True );
        Assert.That( map.IsInside( 0, 1 ), Is.True );
        Assert.That( map.IsInside( 4, 1 ), Is.True );
        Assert.That( map.IsInside( 2, 2 ), Is.True );
        Assert.That( map.IsInside( 2, 0 ), Is.True );
        
        
    }

    [Test]
    // 地図外への移動を止めるため、各方向の範囲外を確認する。
    public void OutsidePositionsAreNotInsideMap()
    {
        // 高さが3横幅が5だと分かるテスト用の地図を、1行ずつ文字列にして配列へまとめる。
        string[] mapRows = new string[] { "#####", "#...#", "#####" };
        // 用意した地図を渡し、読み取りを確かめる対象のFixedMapを作る。
        FixedMap map = new FixedMap( mapRows );
        
        //左右上下の一つ外が範囲外と判定されることを確認する
        Assert.That( map.IsInside( 5, 1 ), Is.False );
        Assert.That( map.IsInside( -1, 1 ), Is.False );
        Assert.That( map.IsInside( 2, 3 ), Is.False );
        Assert.That( map.IsInside( 2, -1 ), Is.False );
    }

    [Test]
    // 床のマスが、歩けると判定されることを確認する。
    public void IsWalkableIsTrue()
    {
        // 高さが3横幅が5だと分かるテスト用の地図を、1行ずつ文字列にして配列へまとめる。
        string[] mapRows = new string[] { "#####", "#...#", "#####" };
        // 用意した地図を渡し、読み取りを確かめる対象のFixedMapを作る。
        FixedMap map = new FixedMap( mapRows );
        // 中段の床(1, 1)を調べる。
        Assert.That( map.IsWalkable( 1, 1 ), Is.True );
    }

    [Test]
    // 上下で地形を変え、Yから正しい行を選んでいることを確認する。
    public void FloorCellsWalkable()
    {
        // 上段中央は壁、下段中央は床。上下を逆に読む誤りを検出できる配置にする。
        string[] mapRows = new string[] { "#####", "#...#", "##.##" };
        // 用意した地図を渡し、読み取りを確かめる対象のFixedMapを作る。
        FixedMap map = new FixedMap( mapRows );
        
        // 上の壁ではfalse、下の床ではtrueになる。
        Assert.That( map.IsWalkable( 2, 2 ), Is.False );
        Assert.That( map.IsWalkable( 2, 0 ), Is.True );
    }
    
}   
