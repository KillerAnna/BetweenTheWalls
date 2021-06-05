using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Get_Item : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
