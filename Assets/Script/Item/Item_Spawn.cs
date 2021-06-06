using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Spawn : MonoBehaviour // �Ѿ� ������ ����
{
    [SerializeField]
    private GameObject Item_BulletPrefab; // �Ѿ� ������ ������
    private bool continue_spawn = false; // �ٽ� ���� ����

    private void Awake()
    {
        for (int i = 0; i < 5; i++) // 5�� ����
        {
            continue_spawn = false;
            int x = Random.Range(-13, 13); // ���� x ��ǥ
            int y = Random.Range(-13, 13); // ���� y ��ǥ

            foreach (Collider2D col in Physics2D.OverlapCircleAll(new Vector2(x, y), 0.4f))
            {
                if (col.gameObject.layer == LayerMask.NameToLayer("Wall")) // �Ѿ� ������ ���� ��ġ�� ���̶��
                    continue_spawn = true; // �ٽ� ����
                else if (col.gameObject.layer == LayerMask.NameToLayer("Floor_Spawn")) // �Ѿ� ������ ���� ��ġ�� ���ͽ��� �̶��
                    continue_spawn = true; // �ٽ� ����
                else if (col.tag.Equals("Item_Bullet")) // �Ѿ� ������ ���� ��ġ�� �Ѿ� �������� �̹� �ִٸ�
                    continue_spawn = true; // �ٽ� ����
            }
            if (continue_spawn) // �ٽ� �����ؾ� �Ѵٸ�
            {
                i--; // ���� Ƚ�� ����
                continue; // �ݺ��� ���ư���
            }

            Vector2 position = new Vector2(x, y); // ���� ��ǥ
            
            Instantiate(Item_BulletPrefab, position, Quaternion.identity); // �Ѿ� ������ ����
        }
    }


}
