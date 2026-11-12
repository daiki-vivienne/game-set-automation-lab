using UnityEngine;

public class WallPositionPreview: MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string[] mapRows = new string[] { "#####", "#...#", "#####" };
        FixedMap map = new FixedMap( mapRows );
        // Yが1つ決まるたびに、Xを左端から右端まで調べる。
        for (int y = 0; y < map.Height; y++)
        {
            Debug.Log("Y = " + y);
            for (int x = 0; x < map.Width; x++)
            {
                // 地図内の座標をCoreに渡すと、床ならtrue、壁ならfalseが返る。
                bool IsFloor = map.IsWalkable(x, y);
                Debug.Log( IsFloor );
                if (IsFloor == false)
                {
                    // 壁1マスにつき箱を1個作り、地図と同じX・Yに置く。
                    GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    wall.transform.position = new Vector3(x, y , 0);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
