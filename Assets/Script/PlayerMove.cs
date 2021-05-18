using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove Instance;

    private Rigidbody2D Player;
    public float Speed = 5.0f;

    private void Awake()
    {
        Player = GetComponent<Rigidbody2D>();

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Player.velocity = new Vector2(x, y).normalized * Speed;
        transform.rotation = Quaternion.identity;
    }
}
