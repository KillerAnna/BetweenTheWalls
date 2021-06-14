using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameObject G_bullet;
    public float cooltime;
    private float curtime;
    public float z;
    public Transform pos_right;
    public Transform pos_left;
    public int bullet = 50;
    public Vector2 Mouse;
    SpriteRenderer rend;
    private Rigidbody2D Player;
    public float Speed = 5.0f;
    public LayerMask whatisGround;

    private void Awake()
    {
        Player = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        z = Mathf.Atan2(Mouse.y, Mouse.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, z + 90);
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        if (bullet > 0)
        {
            if (curtime >= cooltime)
            {
                if (Mouse.x <= 0)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        GameObject obj = Instantiate(G_bullet);
                        obj.transform.position = pos_left.transform.position;
                        bullet -= 1;
                        curtime = 0;
                    }
                }
                else
                {
                    if (Input.GetMouseButton(0))
                    {
                        GameObject obj = Instantiate(G_bullet);
                        obj.transform.position = pos_right.transform.position;
                        bullet -= 1;
                        curtime = 0;
                    }
                }
            }
            curtime += Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift)) // 대쉬키를 입력했다면
        {
            Destroy_Wall(x, y); // 벽 파괴 함수 호출
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
            bullet += 5;
        }
    }

    public void Destroy_Wall(float x, float y) // 벽 파괴 함수
    {
        Vector2 Destroy_Position = new Vector2(transform.position.x + x, transform.position.y + y); // 현재 대쉬하려는 방향 한칸 앞의 좌표
        Collider2D overCollider2d = Physics2D.OverlapCircle(Destroy_Position, 0.01f, whatisGround); // 그 좌표에 벽이 있는지 확인
        if (overCollider2d != null) // 
        {
            overCollider2d.transform.GetComponent<Bricks>().MakeDot(Destroy_Position); // 벽이 있으면 파괴
        }
    }
}

