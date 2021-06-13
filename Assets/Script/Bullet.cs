using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 Mouse;
    public float z;
    public int bullet;
    public float speed;

    // Start is called before the first frame update
    private void Start()
    {
        Mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        z = Mathf.Atan2(Mouse.y, Mouse.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, z + 90);
        StartCoroutine(DestroyBullet());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        
        foreach(Collider2D col in Physics2D.OverlapCircleAll(transform.position, 0.0001f))
            if (col.gameObject.layer == LayerMask.NameToLayer("Wall"))
                Destroy(gameObject);
    }

   IEnumerator DestroyBullet()
   {
       yield return new WaitForSeconds(3);
       Destroy(gameObject);
   }



}
