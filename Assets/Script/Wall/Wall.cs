using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Wall : MonoBehaviour
{
    public Tilemap[] tilemap; // �� ��ũ��Ʈ�� ���Ե� Ÿ�ϸ�
    public Tile[] wall; // �Ϲ� ��

    private void Start() // Start �Լ�
    {
        tilemap[0] = GetComponent<Tilemap>(); // Ÿ�ϸ� ������Ʈ ��������
    }

    public void Pullwall(Vector3 Pos, float x, float y) // �� �б� �Լ�
    {
        Vector3Int cellPosition = tilemap[0].WorldToCell(Pos); // �������� �� ������ ?? ���� �� �𸣰���

        tilemap[0].SetTile(cellPosition, null); // �÷��̾� �տ� �� �ı�

        tilemap[0].SetTile(new Vector3Int(cellPosition.x + (int)x, cellPosition.y + (int)y, 0), wall[0]); // �÷��̾� �վտ� �� ����
    }

    public void Destroywall(Vector3 Pos2) // �Ѿ� ������ �� �ı��ϴ� �Լ�
    {
        Vector3Int cellPosition = tilemap[0].WorldToCell(Pos2); // �������� �� ������ ?? ���� �� �𸣰���

        tilemap[0].SetTile(cellPosition, null); // �Ѿ��� ���� �� �ı�
        tilemap[1].SetTile(cellPosition, wall[1]);
    }
}
