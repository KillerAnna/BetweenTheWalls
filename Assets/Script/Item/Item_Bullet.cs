using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Bullet : MonoBehaviour // 총알아이템 획득 스크립트
{
    private Item_Spawn Item_Spawn; // Item_Spawn 클래스 사용하기 위한 준비

    private void Start()
    {
        Item_Spawn = GameObject.Find("Item_Spawn").GetComponent<Item_Spawn>(); // Item_Spawn 클래스 정보 가져오기
    }

    private void OnTriggerEnter2D(Collider2D collision) // 총알아이템이 무언가랑 충돌할 시
    {
        if (collision.tag.Equals("Player")) // 충돌한 오브젝트의 태그가 Player라면
        {
            Item_Spawn.isSpawn = true; // 총알 재생성 여부 true로 변경
            Destroy(this.gameObject); // 총알 아이템 삭제
        }
    }
}
