using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageActivation : MonoBehaviour
{
    public bool  isActivation = false; // 게임오버 조건 (기본값 false)
    public Image Bullet;               // Bullet UI
    public Image GameOver;             // GameOver UI
    public Image Exit;                 // Exit UI

    private void Start()
    {
        Bullet.fillAmount = 1.0f; // 화면상에 보이게 함
    }

    private void Update()
    {
        if (isActivation) // 게임오버 조건이 활성화되면
        {
            Time.timeScale = 0; // 게임 일시정지

            GameOver.fillAmount = 1.0f; // 화면상에 보이게 함
            Exit.fillAmount = 1.0f; // 화면상에 보이게 함
        }
    }

    public void Quit() // Exit 버튼이 눌리면 실행할 함수 (게임 종료)
    {
        // 게임 종료 스크립트
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
}
