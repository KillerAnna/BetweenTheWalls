using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Green_Skeleton : MonoBehaviour
{
    Animator anim;
    private Rigidbody2D player;
    SpriteRenderer rend;

    public ImageActivation _imageActivation; // ImageActivation Ŭ���� ������

    public GameObject[] Map_Wall; // Wall Ÿ�� �׸� ���ӿ�����Ʈ
    public Tilemap[]    tilemap;  // tilemap Ÿ�ϸ�
    public Tile         __Wall;

    public ImageActivation imageActivation; // ImageActivation Ŭ���� ������
    private Transform      PlayerTR; // Player Transform
    private GameObject     Player;   // Player ���ӿ�����Ʈ
    public LayerMask       layer;    // �˻� �� �� ���̾�

    private Vector2Int  targetPos_G;        // �̵��� ��ġ (�÷��̾��� �� ��ġ)
    public float        GSkel_Speed = 2.5f; // Green Skeleton ���ǵ�


    private void Start()
    {
        player = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Map_Wall = new GameObject[2];

        Player = GameObject.Find("Player");
        Map_Wall[0] = GameObject.Find("Wall"); // Wall ���ӿ�����Ʈ ã��
        Map_Wall[1] = GameObject.Find("BrokenWall"); // Wall ���ӿ�����Ʈ ã��
        imageActivation = GameObject.Find("ImageManager").GetComponent<ImageActivation>(); // ImageActivation ���ӿ�����Ʈ�� ImageActivation ������Ʈ ��������


        PlayerTR = Player.GetComponent<Transform>();
        tilemap[0] = Map_Wall[0].GetComponent<Tilemap>(); // Wall�� Tilemap ������Ʈ ��������
        tilemap[1] = Map_Wall[1].GetComponent<Tilemap>(); // BrokenWall�� Tilemap ������Ʈ ��������
    }

    private void Update()
    {
        if (player.velocity.normalized.x == 0 && player.velocity.normalized.y == 0)
            anim.SetBool("is_walk", false);
        else
            anim.SetBool("is_walk", true);

        targetPos_G = Vector2Int.RoundToInt(PlayerTR.position);

        transform.position = Vector2.MoveTowards(transform.position, targetPos_G, GSkel_Speed * Time.deltaTime); // targetPos_G �� ��ġ�� Skeleton �̵�      

        if (Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y), 1f, layer)) // Green Skeleton�� �ڽ��� ��ġ�� ���� �ִ��� �˻�
        {
            Vector3Int cellPosition = tilemap[0].WorldToCell(transform.position); // �������� �� ������ ?? ���� �� �𸣰���

            tilemap[0].SetTile(cellPosition, null); // �� �ı� (�ӽ� ����)
            tilemap[1].SetTile(cellPosition, null); // �� �ı� (�ӽ� ����)
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // Skeleton�� ���𰡿� �浹�ߴٸ�
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet")) // Collision = �Ѿ� // �Ѿ˰� �ε����ٸ�
        {
            Vector3Int cellPosition = tilemap[0].WorldToCell(transform.position); // �������� �� ������ ?? ���� �� �𸣰���
            tilemap[0].SetTile(cellPosition, __Wall); // Skeleton ��ġ�� �� ���� // �ӽù���

            Destroy(gameObject); // Green Skeleton ���
            Destroy(collision.gameObject); // �Ѿ� ����
        }

        else if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) // Player�� Skeleton�̶� ������ Gameover
        {
            imageActivation.isActivation = true; // ���ӿ��� ���� Ȱ��ȭ
        }

        else if (collision.gameObject.layer == LayerMask.NameToLayer("Kill_Bullet"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject); // �Ѿ� ����
        }
    }
}
