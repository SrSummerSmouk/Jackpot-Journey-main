using UnityEngine;

public class WorldScrolling : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject terrainChunkPrefab;
    [SerializeField] private int chunkWidth = 44;
    [SerializeField] private int chunkHeight = 30;
    [SerializeField] private int terrainTileHorizontalCount = 2;
    [SerializeField] private int terrainTileVerticalCount = 2;

    private Vector2Int currentTilePosition;
    private GameObject[,] terrainTiles;

    private void Awake()
    {
        terrainTiles = new GameObject[terrainTileHorizontalCount, terrainTileVerticalCount];
    }

    private void Update()
    {
        Vector2 playerPos = playerTransform.position;
        Vector2Int playerChunkCoord = new Vector2Int(
            Mathf.FloorToInt(playerPos.x / chunkWidth),
            Mathf.FloorToInt(playerPos.y / chunkHeight)
        );

        if (playerChunkCoord != currentTilePosition)
        {
            currentTilePosition = playerChunkCoord;
            LoadChunksAround(playerChunkCoord);
        }
    }

    private void LoadChunksAround(Vector2Int center)
    {
        for (int y = -1; y <= 1; y++)
        {
            for (int x = -1; x <= 1; x++)
            {
                Vector2Int tilePos = new Vector2Int(center.x + x, center.y + y);

                int xi = tilePos.x + terrainTileHorizontalCount / 2;
                int yi = tilePos.y + terrainTileVerticalCount / 2;

                if (xi < 0 || xi >= terrainTileHorizontalCount || yi < 0 || yi >= terrainTileVerticalCount)
                    continue;

                if (terrainTiles[xi, yi] == null)
                {
                    Vector3 spawnPos = new Vector3(tilePos.x * chunkWidth, tilePos.y * chunkHeight, 0);
                    GameObject tileGO = Instantiate(terrainChunkPrefab, spawnPos, Quaternion.identity, transform);
                    tileGO.GetComponent<terrainTile>().SetTilePosition(tilePos);
                    terrainTiles[xi, yi] = tileGO;
                }
            }
        }
    }

    public void Add(GameObject tileGameObject, Vector2Int tilePosition)
    {
        int xi = tilePosition.x + terrainTileHorizontalCount / 2;
        int yi = tilePosition.y + terrainTileVerticalCount / 2;

        if (xi >= 0 && xi < terrainTileHorizontalCount && yi >= 0 && yi < terrainTileVerticalCount)
        {
            terrainTiles[xi, yi] = tileGameObject;
        }
    }
}
