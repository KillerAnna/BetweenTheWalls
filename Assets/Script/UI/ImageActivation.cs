using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageActivation : MonoBehaviour
{
    public GameObject Obj_Exit; // Exit ��ư ������Ʈ

    public Image Bullet;      // Bullet �̹���
    public Image GameOver;    // GameOver �̹���
    public Image Exit;        // Exit �̹���

    public GameObject ElapsedTime;  // ElapsedTime �ؽ�Ʈ

    public bool  isActivation = false; // ���ӿ��� ���� (�⺻�� false)

    private void Start()
    {
        Obj_Exit = GameObject.Find("Exit"); // Exit ��ư ������Ʈ ã��
        Obj_Exit.SetActive(false); // Exit ��ư ��Ȱ��ȭ

        Bullet.fillAmount = 1.0f; // ȭ��� ���̰� ��
    }

    private void Update()
    {
        if (isActivation) // ���ӿ��� ������ Ȱ��ȭ�Ǹ�
        {
            Time.timeScale = 0; // ���� �Ͻ�����

            Obj_Exit.SetActive(true); // Exit ��ư Ȱ��ȭ

            Exit.fillAmount = 1.0f;        // ȭ��� ���̰� ��
            GameOver.fillAmount = 1.0f;    // ȭ��� ���̰� ��

            ElapsedTime.transform.position = new Vector2(1920, 560);
        }
    }
}
