using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arm_Move : MonoBehaviour
{
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
        Mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        z = Mathf.Atan2(Mouse.y, Mouse.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, z+90);
        if (Mouse.x < 0)
        {
            rend.flipX = true;
        }
        else
        {
            rend.flipX = false;

        }
    }
}
