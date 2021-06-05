using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class left_arm_Move : MonoBehaviour
{
    public GameObject bullet;
    public Transform pos;
    public float cooltime;
    private float curtime;
    SpriteRenderer rend;
    public Vector2 Mouse;
    public float z;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Color c = rend.material.color;
        Mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        z = Mathf.Atan2(Mouse.y, Mouse.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, z+90);
        if (Mouse.x <= 0)
        {
            rend.flipX = true;
            c.a = 1;
            rend.material.color = c;
            if (curtime <= 0)
            {
                if (Input.GetMouseButton(0))
                {
                    Instantiate(bullet, pos.position, transform.localRotation);
                }
                curtime = cooltime;
            }
            curtime -= Time.deltaTime;
        }
        else
        {
            rend.flipX = false;
            c.a = 0;
            rend.material.color = c;
        }
    }
}
