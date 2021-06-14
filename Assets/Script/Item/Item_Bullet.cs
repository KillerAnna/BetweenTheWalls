using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Bullet : MonoBehaviour // 총알아이템 획득 스크립트
{
    private void OnTriggerEnter2D(Collider2D collision) // 총알아이템이 무언가랑 충돌할 시
    {
        if (collision.tag.Equals("Player")) // 충돌한 오브젝트의 태그가 Player라면
        {
            Destroy(this.gameObject); // 총알아이템 삭제
        }
    }
}
