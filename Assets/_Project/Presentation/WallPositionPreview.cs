using UnityEngine;

public class WallPositionPreview: MonoBehaviour
{
    private FixedMap map;

    // 壁と床の表示に使った地図を、プレイヤーの移動判定にも渡せるようにする。
    public FixedMap LevelMap
    {
        get { return map; }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 地図は上の行から書く。'#' は壁、'.' は歩ける床。
        string[] mapRows = new string[] { "#####", "#...#", "#####" };
        map = new FixedMap( mapRows );
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
                else
                {
                    GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    // 壁と床は Z=0 に並べ、プレイヤーの箱はその手前に表示する。
                    floor.transform.position = new Vector3(x, y , 0);
                    // 箱を描画する MeshRenderer を取得し、壁と見分けられるように床を青くする。
                    MeshRenderer floorRenderer = floor.GetComponent<MeshRenderer>();
                    floorRenderer.material.color = Color.blue;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
