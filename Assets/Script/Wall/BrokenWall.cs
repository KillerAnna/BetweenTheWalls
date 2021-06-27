using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BrokenWall : MonoBehaviour
{
    public Tilemap tilemap; // �� ��ũ��Ʈ�� ���Ե� Ÿ�ϸ�
    public Tile Brokenwall; // ������ ��

    private void Start()
    {
        tilemap = GetComponent<Tilemap>(); // Ÿ�ϸ� ������Ʈ ��������
    }

    public void PushBrokenwall(Vector3 Pos, float x, float y) // �� �б� �Լ�
    {
        Vector3Int cellPosition = tilemap.WorldToCell(Pos); // �������� �� ������ ?? ���� �� �𸣰���

        tilemap.SetTile(cellPosition, null); // �÷��̾� �տ� �� �ı�

        tilemap.SetTile(new Vector3Int(cellPosition.x + (int)x, cellPosition.y + (int)y, 0), Brokenwall); // �÷��̾� �վտ� �� ����
    }

    public void DestroyBrokenwall(Vector3 Pos2) // �Ѿ� ������ �� �ı��ϴ� �Լ�
    {
        Vector3Int cellPosition = tilemap.WorldToCell(Pos2); // �������� �� ������ ?? ���� �� �𸣰���

        tilemap.SetTile(cellPosition, null); // �Ѿ��� ������ �ı�
    }
}
