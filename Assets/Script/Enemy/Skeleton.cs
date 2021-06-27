using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class Node
{
    public Node(bool _isWall, int _x, int _y) { isWall = _isWall; x = _x; y = _y; }

    public bool isWall;
    public Node ParentNode;

    // G : �������κ��� �̵��ߴ� �Ÿ�, H : |����|+|����| ��ֹ� �����Ͽ� ��ǥ������ �Ÿ�, F : G + H
    public int x, y, G, H;
    public int F { get { return G + H; } }
}


public class Skeleton : MonoBehaviour // Skeleton ���� ��ũ��Ʈ
{
    public Vector2Int       bottomLeft, topRight, startPos, targetPos; // �� �޾Ʒ� ��ǥ, �� ������ ��ǥ, ���� ��ǥ, Ÿ���� ��ǥ
    public List<Node>       FinalNodeList;                             // ������ ��� ����Ʈ
    public bool             allowDiagonal, dontCrossCorner;            // �밢�� ���, �ڳʸ� ������ ����
    private Transform       SkeletonTR;                                // Skeleton Transform
    private Transform       PlayerTR;                                  // Player Transform
    public GameObject       Player;                                    // Player ���ӿ�����Ʈ

    public ImageActivation  imageActivation; // ImageActivation Ŭ���� ������
    public LayerMask        layer;           // Skeleton�� �̵��ϸ鼭 Ȯ���� ���̾�

    public GameObject       Map_Wall; // Wall Ÿ�� �׸� ���ӿ�����Ʈ
    public Tilemap          tilemap;  // tilemap Ÿ�ϸ�
    public Tile             _wall;    // Wall Ÿ��

    private int             sizeX, sizeY;                   // �� ������
    private Node[,]         NodeArray;                      // ���迭 (���ȳ�..)
    private Node            StartNode, TargetNode, CurNode; // ���۳��, Ÿ�ٳ��, ������
    private List<Node>      OpenList, ClosedList;           // ���¸���Ʈ Ŭ�����Ʈ (���ȳ�..)

    public float SKel_Speed = 8f; // Skeleton �ӵ�

    private void Awake()
    {
        Player = GameObject.Find("Player"); // Player ���ӿ�����Ʈ ã��
        Map_Wall = GameObject.Find("Wall"); // Wall ���ӿ�����Ʈ ã��
        imageActivation = GameObject.Find("ImageActivation").GetComponent<ImageActivation>(); // ImageActivation ���ӿ�����Ʈ�� ImageActivation ������Ʈ ��������

        PlayerTR = Player.GetComponent<Transform>(); // Player�� Transform ������Ʈ ��������
        SkeletonTR = GetComponent<Transform>(); // Skeleton�� Transform ������Ʈ ��������
        tilemap = Map_Wall.GetComponent<Tilemap>(); // Wall�� Tilemap ������Ʈ ��������
    }

    private void Update()
    {
        if (targetPos != Vector2Int.RoundToInt(PlayerTR.position)) // Ÿ�� ��ǥ�� Player ��ǥ�� ���� ������
        {
            startPos = Vector2Int.RoundToInt(SkeletonTR.position); // ���� ��ǥ�� Skeleton ��ǥ ����
            targetPos = Vector2Int.RoundToInt(PlayerTR.position);  // ���� ��ǥ�� Palyer ��ǥ ����

            PathFinding(); // A* �˰��� �Լ� ȣ��
        }

        if (FinalNodeList.Count != 0) // ��ã�Ⱑ �������̶��
        {
            //��ã�� ������
            if (FinalNodeList.Count == 1) return;

            Vector2 FinalNodePos = new Vector2(FinalNodeList[1].x, FinalNodeList[1].y); // FinalNodePos�� ��ǥ�� �����´�
            if(Physics2D.OverlapCircle(new Vector2(FinalNodePos.x, FinalNodePos.y), 0.4f, layer))
                PathFinding(); // A* �˰��� �Լ� ȣ��
            else
                SkeletonTR.position = Vector2.MoveTowards(SkeletonTR.position, FinalNodePos, SKel_Speed * Time.deltaTime); // FinalNodePos�� ��ġ�� Skeleton �̵�            

            if ((Vector2)SkeletonTR.position == FinalNodePos) FinalNodeList.RemoveAt(0); // Skeleton�� ���ߴ� ��ġ�� �̵������� �� ��ǥ ����
        }       
    }

    private void OnTriggerEnter2D(Collider2D collision) // Skeleton�� ���𰡿� �浹�ߴٸ�
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet")) // Collision = �Ѿ� // �Ѿ˰� �ε����ٸ�
        {
            Vector3Int cellPosition = tilemap.WorldToCell(transform.position); // �������� �� ������ ?? ���� �� �𸣰���
            tilemap.SetTile(cellPosition, _wall); // Skeleton ��ġ�� �� ���� // �ӽù���

            Destroy(gameObject); // Skeleton ���
            Destroy(collision.gameObject); // �Ѿ� ����
        }

        else if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) // Player�� Skeleton�̶� ������ Gameover
        {
            imageActivation.isActivation = true; // ���ӿ��� ���� Ȱ��ȭ
        }
    }


    public void PathFinding() // A* �˰��� �۵��κ�
    {
        // NodeArray�� ũ�� �����ְ�, isWall, x, y ����
        sizeX = topRight.x - bottomLeft.x + 1;
        sizeY = topRight.y - bottomLeft.y + 1;
        NodeArray = new Node[sizeX, sizeY];

        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                bool isWall = false;
                foreach (Collider2D col in Physics2D.OverlapCircleAll(new Vector2(i + bottomLeft.x, j + bottomLeft.y), 0.4f))
                    if (col.gameObject.layer == LayerMask.NameToLayer("Wall") || col.gameObject.layer == LayerMask.NameToLayer("WallOutside") || col.gameObject.layer == LayerMask.NameToLayer("BrokenWall")) isWall = true;

                NodeArray[i, j] = new Node(isWall, i + bottomLeft.x, j + bottomLeft.y);
            }
        }


        // ���۰� �� ���, ��������Ʈ�� ��������Ʈ, ����������Ʈ �ʱ�ȭ
        StartNode = NodeArray[startPos.x - bottomLeft.x, startPos.y - bottomLeft.y];
        TargetNode = NodeArray[targetPos.x - bottomLeft.x, targetPos.y - bottomLeft.y];

        OpenList = new List<Node>() { StartNode };
        ClosedList = new List<Node>();
        FinalNodeList = new List<Node>();


        while (OpenList.Count > 0)
        {
            // ��������Ʈ �� ���� F�� �۰� F�� ���ٸ� H�� ���� �� ������� �ϰ� ��������Ʈ���� ��������Ʈ�� �ű��
            CurNode = OpenList[0];
            for (int i = 1; i < OpenList.Count; i++)
                if (OpenList[i].F <= CurNode.F && OpenList[i].H < CurNode.H) CurNode = OpenList[i];

            OpenList.Remove(CurNode);
            ClosedList.Add(CurNode);


            // ������
            if (CurNode == TargetNode)
            {
                Node TargetCurNode = TargetNode;
                while (TargetCurNode != StartNode)
                {
                    FinalNodeList.Add(TargetCurNode);
                    TargetCurNode = TargetCurNode.ParentNode;
                }
                FinalNodeList.Add(StartNode);
                FinalNodeList.Reverse();

                return;
            }


            // �֢آע�
            if (allowDiagonal)
            {
                OpenListAdd(CurNode.x + 1, CurNode.y + 1);
                OpenListAdd(CurNode.x - 1, CurNode.y + 1);
                OpenListAdd(CurNode.x - 1, CurNode.y - 1);
                OpenListAdd(CurNode.x + 1, CurNode.y - 1);
            }

            // �� �� �� ��
            OpenListAdd(CurNode.x, CurNode.y + 1);
            OpenListAdd(CurNode.x + 1, CurNode.y);
            OpenListAdd(CurNode.x, CurNode.y - 1);
            OpenListAdd(CurNode.x - 1, CurNode.y);
        }
    }

    void OpenListAdd(int checkX, int checkY)
    {
        // �����¿� ������ ����� �ʰ�, ���� �ƴϸ鼭, ��������Ʈ�� ���ٸ�
        if (checkX >= bottomLeft.x && checkX < topRight.x + 1 && checkY >= bottomLeft.x && checkY < topRight.y + 1 && !NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y].isWall && !ClosedList.Contains(NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y]))
        {
            // �밢�� ����, �� ���̷� ��� �ȵ�
            if (allowDiagonal) if (NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall && NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) return;

            // �ڳʸ� �������� ���� ������, �̵� �߿� �������� ��ֹ��� ������ �ȵ�
            if (dontCrossCorner) if (NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall || NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) return;


            // �̿���忡 �ְ�, ������ 10, �밢���� 14���
            Node NeighborNode = NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y];
            int MoveCost = CurNode.G + (CurNode.x - checkX == 0 || CurNode.y - checkY == 0 ? 10 : 14);


            // �̵������ �̿����G���� �۰ų� �Ǵ� ��������Ʈ�� �̿���尡 ���ٸ� G, H, ParentNode�� ���� �� ��������Ʈ�� �߰�
            if (MoveCost < NeighborNode.G || !OpenList.Contains(NeighborNode))
            {
                NeighborNode.G = MoveCost;
                NeighborNode.H = (Mathf.Abs(NeighborNode.x - TargetNode.x) + Mathf.Abs(NeighborNode.y - TargetNode.y)) * 10;
                NeighborNode.ParentNode = CurNode;

                OpenList.Add(NeighborNode);
            }
        }
    }

    void OnDrawGizmos()
    {
        if (FinalNodeList.Count != 0) for (int i = 0; i < FinalNodeList.Count - 1; i++)
                Gizmos.DrawLine(new Vector2(FinalNodeList[i].x, FinalNodeList[i].y), new Vector2(FinalNodeList[i + 1].x, FinalNodeList[i + 1].y));
    }
}