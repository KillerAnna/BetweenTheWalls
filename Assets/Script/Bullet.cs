using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 Mouse;
    public float z;
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

        foreach (Collider2D col in Physics2D.OverlapCircleAll(transform.position, -0.0001f)) // 총알 위치에 뭐가 있는지 검사
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Wall")) // 총알이 검사한 곳에 Wall 레이어를 가진 collider가 있다면
            {
                col.transform.GetComponent<Wall>().Destroywall(transform.position); // Wall 파괴
                Destroy(gameObject); // 총알 파괴
            }
            else if (col.gameObject.layer == LayerMask.NameToLayer("BrokenWall")) // 총알이 검사한 곳에 BrokenWall 레이어를 가진 collider가 있다면
            {
                col.transform.GetComponent<BrokenWall>().DestroyBrokenwall(transform.position); // BrokenWall 파괴
                Destroy(gameObject); // 총알 파괴
            }
            else if (col.gameObject.layer == LayerMask.NameToLayer("WallOutside")) // 총알이 검사한 곳에 WallOutside 레이어를 가진 collider가 있다면
            {
                Destroy(gameObject); // 총알 파괴
            }
        }

    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
