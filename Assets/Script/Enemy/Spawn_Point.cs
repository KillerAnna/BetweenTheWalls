using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Point : MonoBehaviour
{
    public float spawn_time = 3f;
    public Transform[] spawnpoint;
    public GameObject enemy;
    public GameObject enemy1;
    public float a = 10f;
    public float b = 5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn_enemy());
    }

    // Update is called once per frame
    void Update()
    {

    }
   IEnumerator Spawn_enemy()
    {
        while(true)
        {
            yield return new WaitForSeconds(a);
            for(int i = 0; i < 8;i++)
            {
                int x = Random.Range(0, spawnpoint.Length);
                Instantiate(enemy, spawnpoint[x]);
            }
            yield return new WaitForSeconds(b);
            for(int j=0;j<1; j++)
            {
                int y = Random.Range(0, spawnpoint.Length);
                Instantiate(enemy1, spawnpoint[y]);
            }
            a = 3f;
            b = 24f;
        }
        
    }
}
