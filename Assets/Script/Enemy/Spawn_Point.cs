using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Point : MonoBehaviour
{
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

    private void SpawnSkeleton(int x, int YPos)
    {
        Instantiate(enemy, new Vector2(spawnpoint[x].position.x, spawnpoint[x].position.y + YPos), Quaternion.identity);
    }

    IEnumerator Spawn_enemy()
    {
        int L_Up, L_Down, R_Up, R_Down;

        while (true)
        {
            L_Up = 0;
            L_Down = 0;
            R_Up = 0;
            R_Down = 0;

            yield return new WaitForSeconds(a);
            for (int i = 0; i < 8; i++)
            {
                int x = Random.Range(0, spawnpoint.Length);
                switch (x)
                {
                    case 0:
                        SpawnSkeleton(x, L_Up);
                        L_Up--;
                        break;
                    case 1:
                        SpawnSkeleton(x, R_Up);
                        R_Up--;
                        break;
                    case 2:
                        SpawnSkeleton(x, R_Down);
                        R_Down++;
                        break;
                    case 3:
                        SpawnSkeleton(x, L_Down);
                        L_Down++;
                        break;
                    default:
                        break;
                }

            }
            yield return new WaitForSeconds(b);
            for (int j = 0; j < 1; j++)
            {
                int y = Random.Range(0, spawnpoint.Length);
                Instantiate(enemy1, spawnpoint[y]);
            }
            a = 3f;
            b = 24f;
        }

    }
}
/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Point : MonoBehaviour
{
    public float spawn_time = 3f;
    public Transform[] spawnpoint;
    public GameObject enemy;
    public GameObject enemy1;
    public float a = 20f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn_enemy());
    }



    IEnumerator Spawn_enemy()
    {
    }
}

for (int j = 0; j < 1; j++)
{
    int y = Random.Range(0, spawnpoint.Length);
    Instantiate(enemy1, spawnpoint[y]);
}
a = 10f;
        }
    }
}
*/