using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D Player;
    public float Speed = 5.0f;

    private void Awake()
    {
        Player = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Player.velocity = new Vector2(x, y).normalized * Speed;
        transform.rotation = Quaternion.identity;
    }
}
