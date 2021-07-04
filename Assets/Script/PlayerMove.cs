using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameObject G_bullet;
    public GameObject K_bullet;
    public float cooltime;
    private float curtime;
    public float z;
    public Transform pos_right;
    public Transform pos_left;
    public int bullet = 5;
    public Vector2 Mouse;
    SpriteRenderer rend;
    private Rigidbody2D Player;
    public float Speed = 5.0f;
    Animator anim;
    public LayerMask whatisGround; // üũ�� ���̾� ���

    private void Awake()
    {
        Player = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        z = Mathf.Atan2(Mouse.y, Mouse.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, z + 90);
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (Player.velocity.normalized.x == 0 && Player.velocity.normalized.y == 0)
            anim.SetBool("is_walk", false);
        else
            anim.SetBool("is_walk", true);
        


        if (bullet > 0)
        {
            if (curtime >= cooltime)
            {
                if (Mouse.x <= 0)
                {
                    if (Input.GetMouseButtonDown(1))
                    {
                        GameObject obj = Instantiate(G_bullet);
                        obj.transform.position = pos_left.transform.position;
                        bullet -= 1;
                        curtime = 0;
                    }
                    else if (Input.GetMouseButtonDown(0))
                    {
                        GameObject obj = Instantiate(K_bullet);
                        obj.transform.position = pos_left.transform.position;
                        bullet -= 1;
                        curtime = 0;
                    }
                }
                else
                {
                    if (Input.GetMouseButton(1))
                    {
                        GameObject obj = Instantiate(G_bullet);
                        obj.transform.position = pos_right.transform.position;
                        bullet -= 1;
                        curtime = 0;
                    }
                    else if (Input.GetMouseButtonDown(0))
                    {
                        GameObject obj = Instantiate(K_bullet);
                        obj.transform.position = pos_left.transform.position;
                        bullet -= 1;
                        curtime = 0;
                    }
                }
            }
            curtime += Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift)) // Shift Ű�� �Է��ߴٸ�
        {
            Push(x, y); // �б� �Լ� ȣ��
        }

        Player.velocity = new Vector2(x, y).normalized * Speed;
        transform.rotation = Quaternion.identity;

        if (Mouse.x < 0)
            rend.flipX = true;
        else
            rend.flipX = false;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Item_Bullet")
        {
            bullet += 1;
        }
    }

    public void Push(float x, float y) // �� �ı� �Լ�
    {
        if (x == 0) // ���� �̵�
        {
            Vector2 Push_Position = new Vector2(transform.position.x, transform.position.y + (y * 0.5f)); // ���� �з����ϴ� ���� ��ĭ ���� ��ǥ

            Collider2D overCollider2d = Physics2D.OverlapCircle(Push_Position, 0.01f, whatisGround); // �� ��ǥ�� ���� �ִ��� Ȯ��
            Collider2D overCollider2d_front = Physics2D.OverlapCircle(new Vector2(Push_Position.x, Push_Position.y + y), 0.01f, whatisGround); // �� ��ǥ ��ĭ �տ� ���� �ִ��� Ȯ��

            if (overCollider2d != null && overCollider2d_front == null && overCollider2d.gameObject.layer != LayerMask.NameToLayer("WallOutside")) // ���� �����ʰ�, ��ĭ �� �տ� ���� ����, �з��� ���� ���̵� ���� �ƴ϶��
            {
                if (overCollider2d.gameObject.layer == LayerMask.NameToLayer("Wall")) // �з��� ���� �Ϲݺ� �̶��
                    overCollider2d.transform.GetComponent<Wall>().Pushwall(Push_Position, x, y); // Wall Ŭ������ �б��Լ� ȣ��

                else if (overCollider2d.gameObject.layer == LayerMask.NameToLayer("BrokenWall")) // �з��� ���� �������� �̶��
                    overCollider2d.transform.GetComponent<BrokenWall>().PushBrokenwall(Push_Position, x, y); // BrokenWall Ŭ������ �б��Լ� ȣ��
            }

        }
        else if (y == 0) // �¿� �̵�
        {
            Vector2 Push_Position = new Vector2(transform.position.x + (x * 0.5f), transform.position.y); // ���� �з����ϴ� ���� ��ĭ ���� ��ǥ

            Collider2D overCollider2d = Physics2D.OverlapCircle(Push_Position, 0.01f, whatisGround); // �� ��ǥ�� ���� �ִ��� Ȯ��
            Collider2D overCollider2d_front = Physics2D.OverlapCircle(new Vector2(Push_Position.x + x, Push_Position.y), 0.01f, whatisGround); // �� ��ǥ ��ĭ �տ� ���� �ִ��� Ȯ��

            if (overCollider2d != null && overCollider2d_front == null && overCollider2d.gameObject.layer != LayerMask.NameToLayer("WallOutside")) // ���� �����ʰ�, ��ĭ �� �տ� ���� ����, �з��� ���� ���̵� ���� �ƴ϶��
            {
                if (overCollider2d.gameObject.layer == LayerMask.NameToLayer("Wall")) // �з��� ���� �Ϲݺ� �̶��
                    overCollider2d.transform.GetComponent<Wall>().Pushwall(Push_Position, x, y); // Wall Ŭ������ �б��Լ� ȣ��

                else if (overCollider2d.gameObject.layer == LayerMask.NameToLayer("BrokenWall")) // �з��� ���� �������� �̶��
                    overCollider2d.transform.GetComponent<BrokenWall>().PushBrokenwall(Push_Position, x, y); // BrokenWall Ŭ������ �б��Լ� ȣ��
            }
        }
    }
}

