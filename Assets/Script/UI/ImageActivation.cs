using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // �Ⱦ����� UI���� ��ũ��Ʈ���� Ȯ���� �ϱ����� ���ܳ���

public class ImageActivation : MonoBehaviour
{
    public bool         isActivation = false; // ���ӿ��� ���� (�⺻�� false)
    public GameObject   GameOver;             // GameOver �̹��� ���ӿ�����Ʈ
    public GameObject   Exit;                 // Exit ��ư ���ӿ�����Ʈ

    private void Start() // �̹���, ��ư �����ͼ� ��Ȱ��ȭ
    {
        GameOver = GameObject.Find("GameOver"); // GameOver �̹��� ���ӿ�����Ʈ ã��
        Exit = GameObject.Find("Exit");         // Exit ��ư ���ӿ�����Ʈ ã��

        GameOver.gameObject.SetActive(false);   // GameOver �̹��� ������Ʈ ��Ȱ��ȭ
        Exit.gameObject.SetActive(false);       // Exit ��ư ������Ʈ ��Ȱ��ȭ
    }

    private void Update()
    {
        if (isActivation) // ���ӿ��� ������ Ȱ��ȭ�Ǹ�
        {
            Time.timeScale = 0; // ���� �Ͻ�����
            GameOver.gameObject.SetActive(true); // GameOver �̹��� ������Ʈ Ȱ��ȭ
            Exit.gameObject.SetActive(true);     // Exit ��ư ������Ʈ Ȱ��ȭ
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
