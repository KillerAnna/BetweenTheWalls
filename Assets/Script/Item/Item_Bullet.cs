using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Bullet : MonoBehaviour // �Ѿ˾����� ȹ�� ��ũ��Ʈ
{
    private void OnTriggerEnter2D(Collider2D collision) // �Ѿ˾������� ���𰡶� �浹�� ��
    {
        if (collision.tag.Equals("Player")) // �浹�� ������Ʈ�� �±װ� Player���
        {
            Destroy(this.gameObject); // �Ѿ˾����� ����
        }
    }
}
