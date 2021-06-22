using UnityEngine;
using UnityEngine.Tilemaps;

public class Bricks : MonoBehaviour // �� �ı��ϴ� ��ũ��Ʈ
{
    public Tilemap tilemap; // �� ��ũ��Ʈ�� ���Ե� Ÿ�ϸ�
    public Tile Wall;

    private void Start() // Start �Լ�
    {
        tilemap = GetComponent<Tilemap>(); // Ÿ�ϸ� ������Ʈ ��������
    }

    public void PullWall(Vector3 Pos, float x, float y) // �� �б� �Լ�
    {
        Vector3Int cellPosition = tilemap.WorldToCell(Pos); // �������� �� ������ ?? ���� �� �𸣰���

        tilemap.SetTile(cellPosition, null); // �� �ı�

        tilemap.SetTile(new Vector3Int(cellPosition.x + (int)x, cellPosition.y + (int)y, 0), Wall);
    }
}
