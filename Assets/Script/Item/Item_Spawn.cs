using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Spawn : MonoBehaviour // 총알 아이템 생성
{
    [SerializeField] // 변수 유니티에 보이기
    private GameObject  Item_BulletPrefab;      // 총알 아이템 프리팹
    private bool        continue_spawn = false; // 다시 생성 조건
    private int         SpawnCount = 5;         // 현재 총알 아이템 스폰 개수    
    private float       Spawndelaytime = 10f;   // 총알 아이템 재생성 시간   
    public bool         isSpawn = false;        // 일정 시간 후 재생성 여부

    private void Update() // 게임 시작할 때 한번 실행
    {
        while (SpawnCount > 0) // 5번 생성
        {            
            continue_spawn = false; // 다시 생성 조건 초기화
            int x = Random.Range(-13, 13); // 생성 x 좌표
            int y = Random.Range(-13, 13); // 생성 y 좌표

            foreach (Collider2D col in Physics2D.OverlapCircleAll(new Vector2(x, y), 0.4f)) // 총알 아이템 생성위치의 Collider 가져오기
            {
                if (col.gameObject.layer == LayerMask.NameToLayer("Wall") || col.gameObject.layer == LayerMask.NameToLayer("BrokenWall")) // 총알 아이템 생성 위치가 벽이라면
                    continue_spawn = true; // 다시 생성

                else if (col.gameObject.layer == LayerMask.NameToLayer("Floor_Spawn")) // 총알 아이템 생성 위치가 몬스터스폰 이라면
                    continue_spawn = true; // 다시 생성

                else if (col.tag.Equals("Item_Bullet")) // 총알 아이템 생성 위치에 총알 아이템이 이미 있다면
                    continue_spawn = true; // 다시 생성
            }
            if (continue_spawn) // 다시 생성해야 한다면
                continue; // 반복문 돌아가기

            Vector2 position = new Vector2(x, y); // 생성 좌표

            Instantiate(Item_BulletPrefab, position, Quaternion.identity); // 총알 아이템 생성
            SpawnCount--; // 앞으로 남은 총알 아이템 생성 횟수 1 차감
        }

        if (isSpawn) // 총알 아이템 재생성 여부가 true 라면
        {
            isSpawn = false; // 총알 아이템 재생성 여부 false로 변경

            StartCoroutine(BulletSpawnDelay()); // 코루틴 실행
        }
    }

    IEnumerator BulletSpawnDelay() // 총알 아이템 재생성 시간 딜레이 코루틴
    {
        yield return new WaitForSeconds(Spawndelaytime); // 총알 재생성에 걸리는 시간만큼 리턴
        SpawnCount++; // 남은 총알 아이템 생성 횟수 1 증가
    }
}


