using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Green_Skeleton : MonoBehaviour
{
    public ImageActivation _imageActivation; // ImageActivation 클래스 참조용

    public GameObject[] Map_Wall; // Wall 타일 그릴 게임오브젝트
    public Tilemap[]    tilemap;  // tilemap 타일맵
    public Tile         __Wall;

    public ImageActivation imageActivation; // ImageActivation 클래스 참조용
    private Transform      PlayerTR; // Player Transform
    private GameObject     Player;   // Player 게임오브젝트
    public LayerMask       layer;    // 검사 할 벽 레이어

    private Vector2Int  targetPos_G;        // 이동할 위치 (플레이어의 현 위치)
    public float        GSkel_Speed = 2.5f; // Green Skeleton 스피드


    private void Start()
    {
        Map_Wall = new GameObject[2];

        Player = GameObject.Find("Player");
        Map_Wall[0] = GameObject.Find("Wall"); // Wall 게임오브젝트 찾기
        Map_Wall[1] = GameObject.Find("BrokenWall"); // Wall 게임오브젝트 찾기
        imageActivation = GameObject.Find("ImageActivation").GetComponent<ImageActivation>(); // ImageActivation 게임오브젝트의 ImageActivation 컴포넌트 가져오기


        PlayerTR = Player.GetComponent<Transform>();
        tilemap[0] = Map_Wall[0].GetComponent<Tilemap>(); // Wall의 Tilemap 컴포넌트 가져오기
        tilemap[1] = Map_Wall[1].GetComponent<Tilemap>(); // BrokenWall의 Tilemap 컴포넌트 가져오기
    }

    private void Update()
    {
        targetPos_G = Vector2Int.RoundToInt(PlayerTR.position);

        transform.position = Vector2.MoveTowards(transform.position, targetPos_G, GSkel_Speed * Time.deltaTime); // FinalNodePos의 위치로 Skeleton 이동      

        if (Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y), 1f, layer)) // Green Skeleton이 자신의 위치에 벽이 있는지 검사
        {
            Vector3Int cellPosition = tilemap[0].WorldToCell(transform.position); // 포지션을 셀 포지션 ?? 여긴 잘 모르겠음

            tilemap[0].SetTile(cellPosition, null); // 벽 파괴 (임시 방편)
            tilemap[1].SetTile(cellPosition, null); // 벽 파괴 (임시 방편)
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // Skeleton이 무언가와 충돌했다면
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet")) // Collision = 총알 // 총알과 부딪혔다면
        {
            Vector3Int cellPosition = tilemap[0].WorldToCell(transform.position); // 포지션을 셀 포지션 ?? 여긴 잘 모르겠음
            tilemap[0].SetTile(cellPosition, __Wall); // Skeleton 위치에 벽 생성 // 임시방편

            Destroy(gameObject); // Green Skeleton 사망
            Destroy(collision.gameObject); // 총알 삭제
        }

        else if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) // Player랑 Skeleton이랑 닿으면 Gameover
        {
            imageActivation.isActivation = true; // 게임오버 조건 활성화
        }
    }
}
