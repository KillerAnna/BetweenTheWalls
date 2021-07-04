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
    public LayerMask whatisGround; // 체크할 레이어 목록

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
        if (Input.GetKeyDown(KeyCode.LeftShift)) // Shift 키를 입력했다면
        {
            Push(x, y); // 밀기 함수 호출
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

    public void Push(float x, float y) // 벽 파괴 함수
    {
        if (x == 0) // 상하 이동
        {
            Vector2 Push_Position = new Vector2(transform.position.x, transform.position.y + (y * 0.5f)); // 현재 밀려고하는 방향 한칸 앞의 좌표

            Collider2D overCollider2d = Physics2D.OverlapCircle(Push_Position, 0.01f, whatisGround); // 그 좌표에 벽이 있는지 확인
            Collider2D overCollider2d_front = Physics2D.OverlapCircle(new Vector2(Push_Position.x, Push_Position.y + y), 0.01f, whatisGround); // 그 좌표 한칸 앞에 벽이 있는지 확인

            if (overCollider2d != null && overCollider2d_front == null && overCollider2d.gameObject.layer != LayerMask.NameToLayer("WallOutside")) // 벽이 없지않고, 한칸 더 앞에 벽이 없고, 밀려는 벽이 사이드 벽이 아니라면
            {
                if (overCollider2d.gameObject.layer == LayerMask.NameToLayer("Wall")) // 밀려는 벽이 일반벽 이라면
                    overCollider2d.transform.GetComponent<Wall>().Pushwall(Push_Position, x, y); // Wall 클래스의 밀기함수 호출

                else if (overCollider2d.gameObject.layer == LayerMask.NameToLayer("BrokenWall")) // 밀려는 벽이 갈라진벽 이라면
                    overCollider2d.transform.GetComponent<BrokenWall>().PushBrokenwall(Push_Position, x, y); // BrokenWall 클래스의 밀기함수 호출
            }

        }
        else if (y == 0) // 좌우 이동
        {
            Vector2 Push_Position = new Vector2(transform.position.x + (x * 0.5f), transform.position.y); // 현재 밀려고하는 방향 한칸 앞의 좌표

            Collider2D overCollider2d = Physics2D.OverlapCircle(Push_Position, 0.01f, whatisGround); // 그 좌표에 벽이 있는지 확인
            Collider2D overCollider2d_front = Physics2D.OverlapCircle(new Vector2(Push_Position.x + x, Push_Position.y), 0.01f, whatisGround); // 그 좌표 한칸 앞에 벽이 있는지 확인

            if (overCollider2d != null && overCollider2d_front == null && overCollider2d.gameObject.layer != LayerMask.NameToLayer("WallOutside")) // 벽이 없지않고, 한칸 더 앞에 벽이 없고, 밀려는 벽이 사이드 벽이 아니라면
            {
                if (overCollider2d.gameObject.layer == LayerMask.NameToLayer("Wall")) // 밀려는 벽이 일반벽 이라면
                    overCollider2d.transform.GetComponent<Wall>().Pushwall(Push_Position, x, y); // Wall 클래스의 밀기함수 호출

                else if (overCollider2d.gameObject.layer == LayerMask.NameToLayer("BrokenWall")) // 밀려는 벽이 갈라진벽 이라면
                    overCollider2d.transform.GetComponent<BrokenWall>().PushBrokenwall(Push_Position, x, y); // BrokenWall 클래스의 밀기함수 호출
            }
        }
    }
}

