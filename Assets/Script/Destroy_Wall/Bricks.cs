using UnityEngine;
using UnityEngine.Tilemaps;

public class Bricks : MonoBehaviour // �� �ı��ϴ� ��ũ��Ʈ
{
    public Tilemap tilemap; // �� ��ũ��Ʈ�� ���Ե� Ÿ�ϸ�

    private void Start() // Start �Լ�
    {
        tilemap = GetComponent<Tilemap>(); // Ÿ�ϸ� ������Ʈ ��������
    }

    public void MakeDot(Vector3 Pos) // �� �ı� �Լ� (�Ƹ�?)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(Pos); // �������� �� ������ ?? ���� �� �𸣰���

        tilemap.SetTile(cellPosition, null); // �� �ı�
    }
}
