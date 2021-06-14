using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Left_Fire : MonoBehaviour
{
    public Transform Gun;
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
        transform.rotation = Quaternion.Euler(0, 0, z + 90);
        transform.position = Gun.position + Gun.right * -0.04f + Gun.up * -0.43f;
    }
}
