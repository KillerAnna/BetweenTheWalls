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
        if(bullet > 0)
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
}

