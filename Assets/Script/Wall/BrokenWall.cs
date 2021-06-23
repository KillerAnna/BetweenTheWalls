using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BrokenWall : MonoBehaviour
{
    public Tilemap tilemap; // 이 스크립트가 포함된 타일맵
    public Tile Brokenwall; // 일반 벽

    private void Start() // Start 함수
    {
        tilemap = GetComponent<Tilemap>(); // 타일맵 컴포넌트 가져오기
    }

    public void PullBrokenwall(Vector3 Pos, float x, float y) // 벽 밀기 함수
    {
        Vector3Int cellPosition = tilemap.WorldToCell(Pos); // 포지션을 셀 포지션 ?? 여긴 잘 모르겠음

        tilemap.SetTile(cellPosition, null); // 플레이어 앞에 벽 파괴

        tilemap.SetTile(new Vector3Int(cellPosition.x + (int)x, cellPosition.y + (int)y, 0), Brokenwall); // 플레이어 앞앞에 벽 생성
    }

    public void DestroyBrokenwall(Vector3 Pos2) // 총알 닿으면 벽 파괴하는 함수
    {
        Vector3Int cellPosition = tilemap.WorldToCell(Pos2); // 포지션을 셀 포지션 ?? 여긴 잘 모르겠음

        tilemap.SetTile(cellPosition, null); // 총알이 닿은벽 파괴
    }
}
