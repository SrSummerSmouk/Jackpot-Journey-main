using UnityEngine;

public class terrainTile : MonoBehaviour
{
    [SerializeField] private Vector2Int tilePosition;

    public void SetTilePosition(Vector2Int pos)
    {
        tilePosition = pos;
    }

    void Start()
    {
        GetComponentInParent<WorldScrolling>().Add(gameObject, tilePosition);
    }
}
