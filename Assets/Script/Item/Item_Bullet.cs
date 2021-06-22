using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Bullet : MonoBehaviour // �Ѿ˾����� ȹ�� ��ũ��Ʈ
{
    private Item_Spawn Item_Spawn; // Item_Spawn Ŭ���� ����ϱ� ���� �غ�

    private void Start()
    {
        Item_Spawn = GameObject.Find("Item_Spawn").GetComponent<Item_Spawn>(); // Item_Spawn Ŭ���� ���� ��������
    }

    private void OnTriggerEnter2D(Collider2D collision) // �Ѿ˾������� ���𰡶� �浹�� ��
    {
        if (collision.tag.Equals("Player")) // �浹�� ������Ʈ�� �±װ� Player���
        {
            Item_Spawn.isSpawn = true; // �Ѿ� ����� ���� true�� ����
            Destroy(this.gameObject); // �Ѿ� ������ ����
        }
    }
}
