// 1回の移動で、どこからどこへ進んだかを記録する。
public class ActorMoved
{
   private int fromX;
   private int fromY;
   private int toX;
   private int toY;

   // 受け取った座標をフィールドに入れ、作成後も読めるようにする。
   public ActorMoved(int beforeX, int beforeY, int afterX, int afterY)
   {
       fromX = beforeX;
       fromY = beforeY;
       toX = afterX;
       toY = afterY;
   }

   // テストなどから、保存した移動元と移動先の座標を読み取れるようにする。
   public int FromX
   {
      get { return fromX; }
   }
   public int FromY
   {
      get { return fromY; }
   }
   public int ToX
   {
      get { return toX; }
   }
   public int ToY
   {
      get { return toY; }
   }
}
