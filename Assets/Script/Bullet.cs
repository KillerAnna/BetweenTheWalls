using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bullet;
    public float speed;

    // Start is called before the first frame update
    private void Start()
    {
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

    private void OnTriggerEeter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Wall")
        {
            Debug.Log("Hit!");
            Destroy(gameObject);
        }
    }

}
