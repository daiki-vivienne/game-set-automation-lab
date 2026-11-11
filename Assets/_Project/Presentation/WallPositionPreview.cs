using UnityEngine;

public class WallPositionPreview: MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string[] mapRows = new string[] { "#####", "#...#", "#####" };
        FixedMap map = new FixedMap( mapRows );
        bool CanWalk = map.IsWalkable(4,1);
        if (CanWalk == false)
        {
            transform.position = new Vector3(4, 1, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
