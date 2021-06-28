using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageActivation : MonoBehaviour
{
    public bool  isActivation = false; // ���ӿ��� ���� (�⺻�� false)
    public Image Bullet;               // Bullet UI
    public Image GameOver;             // GameOver UI
    public Image Exit;                 // Exit UI

    private void Start()
    {
        Bullet.fillAmount = 1.0f; // ȭ��� ���̰� ��
    }

    private void Update()
    {
        if (isActivation) // ���ӿ��� ������ Ȱ��ȭ�Ǹ�
        {
            Time.timeScale = 0; // ���� �Ͻ�����

            GameOver.fillAmount = 1.0f; // ȭ��� ���̰� ��
            Exit.fillAmount = 1.0f; // ȭ��� ���̰� ��
        }
    }

    public void Quit() // Exit ��ư�� ������ ������ �Լ� (���� ����)
    {
        // ���� ���� ��ũ��Ʈ
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // ���ø����̼� ����
#endif
    }
}
