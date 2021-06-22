using UnityEngine;
using UnityEngine.Tilemaps;

public class Bricks : MonoBehaviour // 벽 파괴하는 스크립트
{
    public Tilemap tilemap; // 이 스크립트가 포함된 타일맵
    public Tile Wall;

    private void Start() // Start 함수
    {
        tilemap = GetComponent<Tilemap>(); // 타일맵 컴포넌트 가져오기
    }

    public void PullWall(Vector3 Pos, float x, float y) // 벽 밀기 함수
    {
        Vector3Int cellPosition = tilemap.WorldToCell(Pos); // 포지션을 셀 포지션 ?? 여긴 잘 모르겠음

        tilemap.SetTile(cellPosition, null); // 벽 파괴

        tilemap.SetTile(new Vector3Int(cellPosition.x + (int)x, cellPosition.y + (int)y, 0), Wall);
    }
}
