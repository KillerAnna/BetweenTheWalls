using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageActivation : MonoBehaviour
{
    public GameObject Obj_Exit; // Exit 버튼 오브젝트

    public Image Bullet;      // Bullet 이미지
    public Image GameOver;    // GameOver 이미지
    public Image Exit;        // Exit 이미지

    public GameObject ElapsedTime;  // ElapsedTime 텍스트

    public bool  isActivation = false; // 게임오버 조건 (기본값 false)

    private void Start()
    {
        Obj_Exit = GameObject.Find("Exit"); // Exit 버튼 오브젝트 찾기
        Obj_Exit.SetActive(false); // Exit 버튼 비활성화

        Bullet.fillAmount = 1.0f; // 화면상에 보이게 함
    }

    private void Update()
    {
        if (isActivation) // 게임오버 조건이 활성화되면
        {
            Time.timeScale = 0; // 게임 일시정지

            Obj_Exit.SetActive(true); // Exit 버튼 활성화

            Exit.fillAmount = 1.0f;        // 화면상에 보이게 함
            GameOver.fillAmount = 1.0f;    // 화면상에 보이게 함

            ElapsedTime.transform.position = new Vector2(1920, 560);
        }
    }
}
