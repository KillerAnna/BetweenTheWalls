using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameQuit : MonoBehaviour
{
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
