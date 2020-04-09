using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapEditor : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile tile;

    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            var pos = cam.ScreenToWorldPoint(Input.mousePosition);
            tilemap.SetTile(tilemap.WorldToCell(pos), tile);
        }
    }
}
