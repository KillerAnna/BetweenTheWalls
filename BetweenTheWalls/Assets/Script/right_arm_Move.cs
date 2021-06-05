using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class right_arm_Move : MonoBehaviour
{
    public GameObject bullet;
    public Transform pos;
    public float cooltime;
    private float curtime;
    public Vector2 Mouse;
    public float z;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        z = Mathf.Atan2(Mouse.y, Mouse.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, z+90);
        if(curtime<=0)
            {
                if (Input.GetMouseButton(0))
                {
                    Instantiate(bullet, pos.position, transform.localRotation);
                }
            curtime = cooltime;
            }
        curtime -= Time.deltaTime; 
    }
}
