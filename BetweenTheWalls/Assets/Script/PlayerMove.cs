using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
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
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Player.velocity = new Vector2(x, y).normalized * Speed;
        transform.rotation = Quaternion.identity;

        if (Mouse.x < 0)
            rend.flipX = true;
        else
            rend.flipX = false;
    }


}
