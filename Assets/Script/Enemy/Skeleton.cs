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

    // G : 시작으로부터 이동했던 거리, H : |가로|+|세로| 장애물 무시하여 목표까지의 거리, F : G + H
    public int x, y, G, H;
    public int F { get { return G + H; } }
}


public class Skeleton : MonoBehaviour // Skeleton 관련 스크립트
{
    public Vector2Int       bottomLeft, topRight, startPos, targetPos; // 맵 왼아래 좌표, 맵 오른위 좌표, 현재 좌표, 타겟의 좌표
    public List<Node>       FinalNodeList;                             // 마지막 노드 리스트
    public bool             allowDiagonal, dontCrossCorner;            // 대각선 허용, 코너를 뚫을지 말지
    private Transform       SkeletonTR;                                // Skeleton Transform
    private Transform       PlayerTR;                                  // Player Transform
    public GameObject       Player;                                    // Player 게임오브젝트

    public ImageActivation  imageActivation; // ImageActivation 클래스 참조용
    public LayerMask        layer;           // Skeleton이 이동하면서 확인할 레이어

    public GameObject       Map_Wall; // Wall 타일 그릴 게임오브젝트
    public Tilemap          tilemap;  // tilemap 타일맵
    public Tile             _wall;    // Wall 타일

    private int             sizeX, sizeY;                   // 맵 사이즈
    private Node[,]         NodeArray;                      // 노드배열 (기억안남..)
    private Node            StartNode, TargetNode, CurNode; // 시작노드, 타겟노드, 현재노드
    private List<Node>      OpenList, ClosedList;           // 오픈리스트 클로즈리스트 (기억안남..)

    public float SKel_Speed = 8f; // Skeleton 속도

    private void Awake()
    {
        Player = GameObject.Find("Player"); // Player 게임오브젝트 찾기
        Map_Wall = GameObject.Find("Wall"); // Wall 게임오브젝트 찾기
        imageActivation = GameObject.Find("ImageActivation").GetComponent<ImageActivation>(); // ImageActivation 게임오브젝트의 ImageActivation 컴포넌트 가져오기

        PlayerTR = Player.GetComponent<Transform>(); // Player의 Transform 컴포넌트 가져오기
        SkeletonTR = GetComponent<Transform>(); // Skeleton의 Transform 컴포넌트 가져오기
        tilemap = Map_Wall.GetComponent<Tilemap>(); // Wall의 Tilemap 컴포넌트 가져오기
    }

    private void Update()
    {
        if (targetPos != Vector2Int.RoundToInt(PlayerTR.position)) // 타겟 좌표가 Player 좌표랑 같지 않으면
        {
            startPos = Vector2Int.RoundToInt(SkeletonTR.position); // 현재 좌표로 Skeleton 좌표 설정
            targetPos = Vector2Int.RoundToInt(PlayerTR.position);  // 현재 좌표로 Palyer 좌표 설정

            PathFinding(); // A* 알고리즘 함수 호출
        }

        if (FinalNodeList.Count != 0) // 길찾기가 진행중이라면
        {
            //길찾기 성공시
            if (FinalNodeList.Count == 1) return;

            Vector2 FinalNodePos = new Vector2(FinalNodeList[1].x, FinalNodeList[1].y); // FinalNodePos의 좌표를 가져온다
            if(Physics2D.OverlapCircle(new Vector2(FinalNodePos.x, FinalNodePos.y), 0.4f, layer))
                PathFinding(); // A* 알고리즘 함수 호출
            else
                SkeletonTR.position = Vector2.MoveTowards(SkeletonTR.position, FinalNodePos, SKel_Speed * Time.deltaTime); // FinalNodePos의 위치로 Skeleton 이동            

            if ((Vector2)SkeletonTR.position == FinalNodePos) FinalNodeList.RemoveAt(0); // Skeleton이 정했던 위치로 이동했으면 그 좌표 삭제
        }       
    }

    private void OnTriggerEnter2D(Collider2D collision) // Skeleton이 무언가와 충돌했다면
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet")) // Collision = 총알 // 총알과 부딪혔다면
        {
            Vector3Int cellPosition = tilemap.WorldToCell(transform.position); // 포지션을 셀 포지션 ?? 여긴 잘 모르겠음
            tilemap.SetTile(cellPosition, _wall); // Skeleton 위치에 벽 생성 // 임시방편

            Destroy(gameObject); // Skeleton 사망
            Destroy(collision.gameObject); // 총알 삭제
        }

        else if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) // Player랑 Skeleton이랑 닿으면 Gameover
        {
            imageActivation.isActivation = true; // 게임오버 조건 활성화
        }
    }


    public void PathFinding() // A* 알고리즘 작동부분
    {
        // NodeArray의 크기 정해주고, isWall, x, y 대입
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


        // 시작과 끝 노드, 열린리스트와 닫힌리스트, 마지막리스트 초기화
        StartNode = NodeArray[startPos.x - bottomLeft.x, startPos.y - bottomLeft.y];
        TargetNode = NodeArray[targetPos.x - bottomLeft.x, targetPos.y - bottomLeft.y];

        OpenList = new List<Node>() { StartNode };
        ClosedList = new List<Node>();
        FinalNodeList = new List<Node>();


        while (OpenList.Count > 0)
        {
            // 열린리스트 중 가장 F가 작고 F가 같다면 H가 작은 걸 현재노드로 하고 열린리스트에서 닫힌리스트로 옮기기
            CurNode = OpenList[0];
            for (int i = 1; i < OpenList.Count; i++)
                if (OpenList[i].F <= CurNode.F && OpenList[i].H < CurNode.H) CurNode = OpenList[i];

            OpenList.Remove(CurNode);
            ClosedList.Add(CurNode);


            // 마지막
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


            // ↗↖↙↘
            if (allowDiagonal)
            {
                OpenListAdd(CurNode.x + 1, CurNode.y + 1);
                OpenListAdd(CurNode.x - 1, CurNode.y + 1);
                OpenListAdd(CurNode.x - 1, CurNode.y - 1);
                OpenListAdd(CurNode.x + 1, CurNode.y - 1);
            }

            // ↑ → ↓ ←
            OpenListAdd(CurNode.x, CurNode.y + 1);
            OpenListAdd(CurNode.x + 1, CurNode.y);
            OpenListAdd(CurNode.x, CurNode.y - 1);
            OpenListAdd(CurNode.x - 1, CurNode.y);
        }
    }

    void OpenListAdd(int checkX, int checkY)
    {
        // 상하좌우 범위를 벗어나지 않고, 벽이 아니면서, 닫힌리스트에 없다면
        if (checkX >= bottomLeft.x && checkX < topRight.x + 1 && checkY >= bottomLeft.x && checkY < topRight.y + 1 && !NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y].isWall && !ClosedList.Contains(NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y]))
        {
            // 대각선 허용시, 벽 사이로 통과 안됨
            if (allowDiagonal) if (NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall && NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) return;

            // 코너를 가로질러 가지 않을시, 이동 중에 수직수평 장애물이 있으면 안됨
            if (dontCrossCorner) if (NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall || NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) return;


            // 이웃노드에 넣고, 직선은 10, 대각선은 14비용
            Node NeighborNode = NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y];
            int MoveCost = CurNode.G + (CurNode.x - checkX == 0 || CurNode.y - checkY == 0 ? 10 : 14);


            // 이동비용이 이웃노드G보다 작거나 또는 열린리스트에 이웃노드가 없다면 G, H, ParentNode를 설정 후 열린리스트에 추가
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