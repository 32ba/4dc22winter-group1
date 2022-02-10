using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextWriterUraEnd : MonoBehaviour
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

    IEnumerator Home()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("�z�[����");
        }
        yield return 0;
    }
    // ���͂�\��������R���[�`��
    IEnumerator Cotest()
    {
        uitext.DrawText("���G���f�B���O");
        yield return StartCoroutine("Skip");

        uitext.DrawText("�ӂ�ɂ͉��Ƃ������Ȃ���C���Y���Ă����B");
        yield return StartCoroutine("Skip");

        uitext.DrawText("�˗���", "���[��E�E�E��������}�O���o���Ⴂ�܂����˂�");
        yield return StartCoroutine("Skip");

        uitext.DrawText("�T��", "�܂����S�ȏ��������Ă��Ă悩�����̂ł͂Ȃ��ł���?");
        yield return StartCoroutine("Skip");

        uitext.DrawText("�˗���", "�����ł��ˁB���̋����������𓭂��Ă��Ȃ��Ă悩�����ł��B");
        yield return StartCoroutine("Skip");

        uitext.DrawText("�T��", "����ł͈˗����Ȃ�ł����ǂ��E�E�E");
        yield return StartCoroutine("Skip");

        uitext.DrawText("�˗���", "�͂��B������ɂȂ�܂����H");
        yield return StartCoroutine("Skip");

        uitext.DrawText("�T��", "15���~�ɂȂ�܂��B");
        yield return StartCoroutine("Skip");

        uitext.DrawText("�˗���", "�͂��H�������ɍ������₵�܂���?");
        yield return StartCoroutine("Skip");

        uitext.DrawText("�T��", "�����˗����͈˗����Ȃ�ŕ����Ă����Ȃ��ƍ���܂��B");
        yield return StartCoroutine("Skip");

        uitext.DrawText("�˗���", "���񂽋��ނ�����C�����Ȃ���񂩁B");
        yield return StartCoroutine("Skip");

        uitext.DrawText("�˗���", "������l��ȁI������l��ŁI�z���}�I");
        yield return StartCoroutine("Skip");


        yield return StartCoroutine("Home");
    }
}
