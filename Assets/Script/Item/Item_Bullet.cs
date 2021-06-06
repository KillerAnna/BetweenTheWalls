using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Bullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
