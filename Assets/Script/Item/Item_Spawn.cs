using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Spawn : MonoBehaviour // 총알 아이템 생성
{
    [SerializeField]
    private GameObject Item_BulletPrefab; // 총알 아이템 프리팹
    private bool continue_spawn = false; // 다시 생성 조건

    private void Awake()
    {
        for (int i = 0; i < 5; i++) // 5번 생성
        {
            continue_spawn = false;
            int x = Random.Range(-13, 13); // 생성 x 좌표
            int y = Random.Range(-13, 13); // 생성 y 좌표

            foreach (Collider2D col in Physics2D.OverlapCircleAll(new Vector2(x, y), 0.4f))
            {
                if (col.gameObject.layer == LayerMask.NameToLayer("Wall")) // 총알 아이템 생성 위치가 벽이라면
                    continue_spawn = true; // 다시 생성
                else if (col.gameObject.layer == LayerMask.NameToLayer("Floor_Spawn")) // 총알 아이템 생성 위치가 몬스터스폰 이라면
                    continue_spawn = true; // 다시 생성
                else if (col.tag.Equals("Item_Bullet")) // 총알 아이템 생성 위치에 총알 아이템이 이미 있다면
                    continue_spawn = true; // 다시 생성
            }
            if (continue_spawn) // 다시 생성해야 한다면
            {
                i--; // 생성 횟수 차감
                continue; // 반복문 돌아가기
            }

            Vector2 position = new Vector2(x, y); // 생성 좌표
            
            Instantiate(Item_BulletPrefab, position, Quaternion.identity); // 총알 아이템 생성
        }
    }


}
