using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 안쓰지만 UI관련 스크립트란건 확실히 하기위해 남겨놓음

public class ImageActivation : MonoBehaviour
{
    public bool         isActivation = false; // 게임오버 조건 (기본값 false)
    public GameObject   GameOver;             // GameOver 이미지 게임오브젝트
    public GameObject   Exit;                 // Exit 버튼 게임오브젝트

    private void Start() // 이미지, 버튼 가져와서 비활성화
    {
        GameOver = GameObject.Find("GameOver"); // GameOver 이미지 게임오브젝트 찾기
        Exit = GameObject.Find("Exit");         // Exit 버튼 게임오브젝트 찾기

        GameOver.gameObject.SetActive(false);   // GameOver 이미지 오브젝트 비활성화
        Exit.gameObject.SetActive(false);       // Exit 버튼 오브젝트 비활성화
    }

    private void Update()
    {
        if (isActivation) // 게임오버 조건이 활성화되면
        {
            Time.timeScale = 0; // 게임 일시정지
            GameOver.gameObject.SetActive(true); // GameOver 이미지 오브젝트 활성화
            Exit.gameObject.SetActive(true);     // Exit 버튼 오브젝트 활성화
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
