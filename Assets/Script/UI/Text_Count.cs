using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text_Count : MonoBehaviour
{
    public GameObject remainingTimeText_Obj; // 첫 몬스터 젠까지 남은 시간 텍스트 오브젝트

    public Text BulletText;        // 남은 총알 수 출력할 텍스트
    public Text ElapsedTimeText;   // 경과 시간 텍스트
    public Text remainingTimeText; // 남은 시간 텍스트
    public PlayerMove Playermove;  // 남은 총알 수가 있는 클래스 

    private string BulletCount;        // 정수형인 남은 총알 수를 문자열로 바꾸기 위한 변수
    private float elapsedTime = 0;     // 경과시간
    private float remainingTime = 20f; // 첫 몬스터 젠까지 남은 시간

    void Start()
    {
        Playermove = GameObject.Find("Player").GetComponent<PlayerMove>(); // 클래스 가져오기
    }

    void Update()
    {
        BulletCount = Playermove.bullet.ToString(); // 남은 총알 수 문자열로 형변환
        BulletText.text = BulletCount; // 남은 총알 수 텍스트로 출력

        // 경과 시간
        elapsedTime += Time.deltaTime;
        ElapsedTimeText.text = elapsedTime.ToString("0");

        // 첫 몬스터 젠까지 남은 시간
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            remainingTimeText.text = remainingTime.ToString("0");
        }
        else
            remainingTimeText_Obj.SetActive(false);
    }
}
