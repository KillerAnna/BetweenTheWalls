using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
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
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            Debug.Log("W");
            collider.gameObject.SendMessage("Hit", 1);
            Destroy(gameObject);
        }
    }

}
