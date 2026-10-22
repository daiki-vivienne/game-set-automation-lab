// 固定マップを保持し、サイズ・座標の範囲・歩ける地形を確認する。
public class FixedMap
{
    // 1つの文字列が地図の1行。上の行から順番に保持する。
    private string[] rows ;

    // 作る側が渡した地図を、このFixedMapで使えるようにする。
    public FixedMap(string[] mapRows)
    {
        // mapRowsは受け取り用の名前。あとからも地図を読めるよう、フィールドrowsで保持する。
        rows = mapRows;
    }

    // 高さを整数で読む窓口。1つの文字列が1行なので、配列の個数が高さになる。
    // Heightが読まれるとgetが動き、rows.Lengthの値を読み手へ渡す。
    public int Height
    {
        get { return rows.Length; }
    }
    //Widthが読まれるとgetが動き、rows[0].Length（1行目の文字数）を読み手へ渡す。
    public int Width
    {
        get { return rows[0].Length; }
    }

    // 移動先の地形を読む前に、その座標が地図の範囲内かを確認するメソッド。
    // boolは答えの型。範囲内ならtrue、範囲外ならfalseを返す。壁のマスも範囲内に含める。
    public bool IsInside(int x, int y)
    {
        // returnで、4つの条件を確認した答え（trueかfalse）を呼び出した側へ返す。
        return x >= 0 && x < Width && y >= 0 && y < Height;
    }
    
    // 指定したマスが床ならtrue、壁や地図外ならfalseを返す。
    public bool IsWalkable(int x, int y)
    {
        // 地図外には読むマスがないので、文字を読む前に判定を終える。
        if (IsInside(x, y) == false)
        {
            return false;
        }
        // Yは下から増えるため、上から保存した行番号に変換する。
        int rowIndex = Height - 1 - y;
        // 地図から1行を選び、その行のX番目の1文字を読む。
        string row = rows[rowIndex];
        char cell = row[x];
        // 床の文字「.」と等しいかを比較し、その答えを返す。
        return cell == '.';
    }


}
