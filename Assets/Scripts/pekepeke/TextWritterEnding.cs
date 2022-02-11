using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextWritterEnding : MonoBehaviour
{
    public Uitext uitext;
    public ImageChange change;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Cotest");
    }

    IEnumerator Skip()
    {
        while (uitext.playing) yield return 0;
        while (!uitext.IsClicked()) yield return 0;
    }

    IEnumerator Title()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Title");
        }
        yield return 0;
    }

    // ���͂�\��������R���[�`��
    IEnumerator Cotest()
    {
        uitext.DrawText("�G���f�B���O");
        yield return StartCoroutine("Skip");

        uitext.DrawText("�����̑O�ɍ�������l�̎p���������B");
        yield return StartCoroutine("Skip");

        uitext.DrawText("�˗���", "�T���}�����o�Ȃ��ł͂Ȃ��ł����B");
        yield return StartCoroutine("Skip");

        uitext.DrawText("�T��", "�V�䂵�ď��߂ă}�O�����o�Ă���Ȃ�āE�E�E");
        yield return StartCoroutine("Skip");

        uitext.DrawText("�˗���", "�����Ίm���������Ă܂���ˁB");
        yield return StartCoroutine("Skip");

        uitext.DrawText("�T��", "�������ˁB100�p�[�������Ă�ˁB");
        yield return StartCoroutine("Skip");

        uitext.DrawText("�˗���", "���̋����\�ʂ�ق�ƂɈŐ[���ł���");
        yield return StartCoroutine("Skip");

        change.ChangeAngry();
        uitext.DrawText("�T��", "�������ނ���Ƃ�C�����Ȃ�����ȁI");
        yield return StartCoroutine("Skip");

        uitext.DrawText("�˗���", "�ق�Ƃ��̒ʂ�ł����");
        yield return StartCoroutine("Skip");

        uitext.DrawText("�T��", "������l��ȁI������l��ŁI�z���}�I");
        yield return StartCoroutine("Skip");
        yield return StartCoroutine("Title");
    }
}
