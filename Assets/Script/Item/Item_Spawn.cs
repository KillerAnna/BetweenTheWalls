using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Spawn : MonoBehaviour // �Ѿ� ������ ����
{
    [SerializeField] // ���� ����Ƽ�� ���̱�
    private GameObject  Item_BulletPrefab;      // �Ѿ� ������ ������
    private bool        continue_spawn = false; // �ٽ� ���� ����
    private int         SpawnCount = 5;         // ���� �Ѿ� ������ ���� ����    
    private float       Spawndelaytime = 10f;   // �Ѿ� ������ ����� �ð�   
    public bool         isSpawn = false;        // ���� �ð� �� ����� ����

    private void Update() // ���� ������ �� �ѹ� ����
    {
        while (SpawnCount > 0) // 5�� ����
        {            
            continue_spawn = false; // �ٽ� ���� ���� �ʱ�ȭ
            int x = Random.Range(-13, 13); // ���� x ��ǥ
            int y = Random.Range(-13, 13); // ���� y ��ǥ

            foreach (Collider2D col in Physics2D.OverlapCircleAll(new Vector2(x, y), 0.4f)) // �Ѿ� ������ ������ġ�� Collider ��������
            {
                if (col.gameObject.layer == LayerMask.NameToLayer("Wall") || col.gameObject.layer == LayerMask.NameToLayer("BrokenWall")) // �Ѿ� ������ ���� ��ġ�� ���̶��
                    continue_spawn = true; // �ٽ� ����

                else if (col.gameObject.layer == LayerMask.NameToLayer("Floor_Spawn")) // �Ѿ� ������ ���� ��ġ�� ���ͽ��� �̶��
                    continue_spawn = true; // �ٽ� ����

                else if (col.tag.Equals("Item_Bullet")) // �Ѿ� ������ ���� ��ġ�� �Ѿ� �������� �̹� �ִٸ�
                    continue_spawn = true; // �ٽ� ����
            }
            if (continue_spawn) // �ٽ� �����ؾ� �Ѵٸ�
                continue; // �ݺ��� ���ư���

            Vector2 position = new Vector2(x, y); // ���� ��ǥ

            Instantiate(Item_BulletPrefab, position, Quaternion.identity); // �Ѿ� ������ ����
            SpawnCount--; // ������ ���� �Ѿ� ������ ���� Ƚ�� 1 ����
        }

        if (isSpawn) // �Ѿ� ������ ����� ���ΰ� true ���
        {
            isSpawn = false; // �Ѿ� ������ ����� ���� false�� ����

            StartCoroutine(BulletSpawnDelay()); // �ڷ�ƾ ����
        }
    }

    IEnumerator BulletSpawnDelay() // �Ѿ� ������ ����� �ð� ������ �ڷ�ƾ
    {
        yield return new WaitForSeconds(Spawndelaytime); // �Ѿ� ������� �ɸ��� �ð���ŭ ����
        SpawnCount++; // ���� �Ѿ� ������ ���� Ƚ�� 1 ����
    }
}


