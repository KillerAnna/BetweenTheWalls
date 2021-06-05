using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Item_Bullet : MonoBehaviour
{
    [SerializeField]
    private GameObject Bullet_Prefab;

    private void Awake()
    {
        for (int i = 0; i < 5; i++)
        {
            int x = Random.Range(-11, 11);
            int y = Random.Range(-13, 13);
            Vector2 position = new Vector2(x, y);

            Instantiate(Bullet_Prefab, position, Quaternion.identity);
        }
    }
}
