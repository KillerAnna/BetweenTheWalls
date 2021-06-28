using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Point : MonoBehaviour
{
    public float spawn_time = 3f;
    public Transform[] spawnpoint;
    public GameObject enemy;
    public GameObject enemy1;
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
            yield return new WaitForSeconds(10);
            int x = Random.Range(0, spawnpoint.Length);
            Instantiate(enemy, spawnpoint[x]);
            Instantiate(enemy1, spawnpoint[x]);
        }
        
    }
}
