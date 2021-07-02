using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text_Count : MonoBehaviour
{
    public GameObject remainingTimeText_Obj; // ù ���� ������ ���� �ð� �ؽ�Ʈ ������Ʈ

    public Text BulletText;        // ���� �Ѿ� �� ����� �ؽ�Ʈ
    public Text ElapsedTimeText;   // ��� �ð� �ؽ�Ʈ
    public Text remainingTimeText; // ���� �ð� �ؽ�Ʈ
    public PlayerMove Playermove;  // ���� �Ѿ� ���� �ִ� Ŭ���� 

    private string BulletCount;        // �������� ���� �Ѿ� ���� ���ڿ��� �ٲٱ� ���� ����
    private float elapsedTime = 0;     // ����ð�
    private float remainingTime = 20f; // ù ���� ������ ���� �ð�

    void Start()
    {
        Playermove = GameObject.Find("Player").GetComponent<PlayerMove>(); // Ŭ���� ��������
    }

    void Update()
    {
        BulletCount = Playermove.bullet.ToString(); // ���� �Ѿ� �� ���ڿ��� ����ȯ
        BulletText.text = BulletCount; // ���� �Ѿ� �� �ؽ�Ʈ�� ���

        // ��� �ð�
        elapsedTime += Time.deltaTime;
        ElapsedTimeText.text = elapsedTime.ToString("0");

        // ù ���� ������ ���� �ð�
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            remainingTimeText.text = remainingTime.ToString("0");
        }
        else
            remainingTimeText_Obj.SetActive(false);
    }
}
