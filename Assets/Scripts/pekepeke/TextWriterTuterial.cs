using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextWriterTuterial : MonoBehaviour
{
    public Uitext uitext;
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

    IEnumerator TutorialGacha()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("�`���[�g���A���K�`����");
        }
        yield return 0;
    }

    // ���͂�\��������R���[�`��
    IEnumerator Cotest()
    {
        uitext.DrawText("�v�����[�O");
        yield return StartCoroutine("Skip");

        uitext.DrawText("�Ă̏������a�炬�A�؂̗t�̐X�Ƃ����R�͂��񂽂�ƐF�N�₩�ɂȂ����B");
        yield return StartCoroutine("Skip");

        uitext.DrawText("���̎R��w�i�ɁA�l�͂���قǑ����͂Ȃ����A�����̕������y�n��D���������̂悤�ɗ������錚�������߂������h�꒬���������B");
        yield return StartCoroutine("Skip");


        uitext.DrawText("�˗���", "���͂ł��˂��A���N�̉č��ɒ��̖k�̕��ɐV�����������ł�����ł����E�E�E");
        yield return StartCoroutine("Skip");

        uitext.DrawText("�T��", "�E�E�E");
        yield return StartCoroutine("Skip");

        uitext.DrawText("�h��̈ꎺ�ň˗���ƒT��́A�������ݟ��ꂽ�Ă̗Β������݂Ȃ����b�����Ă���B");
        yield return StartCoroutine("Skip");

        uitext.DrawText("�˗���", "�Z�l�̘b���ɂ��ƁA���̓X�ł͐V�N�ȊC�̋��������Ă���Ƃ̂��ƂȂ�ł����A�ǂ��ɂ����I�@���񂵂ďo���F�Ŕ����鋛���ς��Ƃ����b�ŁB");
        yield return StartCoroutine("Skip");

        uitext.DrawText("�T��", "�ق��ق��A�����");
        yield return StartCoroutine("Skip");

        uitext.DrawText("�˗���", "�͂��A����Ŏ��̂ق��ł��̓X�ɕ����ƁA�X���H���u�K�`���H�v�Ƃ��������̔���������Ă���݂����Ȃ�ł��B");
        yield return StartCoroutine("Skip");

        uitext.DrawText("�T��", "�m���ɕςȔ�����ł��ˁB�ƂĂ��ׂ����o�锄����Ƃ͎v���Ȃ��ł��B");
        yield return StartCoroutine("Skip");

        uitext.DrawText("�˗���", "���������v���̂ł����A���񒊑I���Ă��V�N�ȊC�̋����o�Ă����Ƃ���͌������Ƃ��Ȃ��ƏZ�l�������Ă��āA�{���ɊC�̋��𔄂��Ă��邩�������̂ł��B");
        yield return StartCoroutine("Skip");

        uitext.DrawText("�˗���", "�����ŒT�コ��ɂ́A�{���ɐV�N�ȊC�̋����d����Ă���̂����̂��X�𒲍����Ă��炢�����āE�E�E");
        yield return StartCoroutine("Skip");

        uitext.DrawText("�T��", "�E�E�E");
        yield return StartCoroutine("Skip");

        uitext.DrawText("�����Ɉ�u�̐Î₪�K���B�T��͏����l����f�U������A�����J�����B");
        yield return StartCoroutine("Skip");

        uitext.DrawText("�T��", "������܂����B�������A���Ȃ�̔�p�͂�����Ǝv���܂��B");
        yield return StartCoroutine("Skip");

        uitext.DrawText("���̏h�꒬�͊C���炩�Ȃ藣��Ă���A�V�N�ȋ����^��ł���ɂ͑����̔�p���|���邱�Ƃ͑z�������B");
        yield return StartCoroutine("Skip");

        uitext.DrawText("�������A�b��ɋ������������Ŗ{���ɊC�̋����d����Ă���̂��͕�����Ȃ��B");
        yield return StartCoroutine("Skip");

        uitext.DrawText("�T��͂ǂ��ƂȂ��[���ł��������B");
        yield return StartCoroutine("Skip");

        uitext.DrawText("�˗���ƒT��́A�����W�߂��R�����𗊂�ɉ����������̈ł�\�����Ƃɂ����B");
        yield return StartCoroutine("Skip");
        yield return StartCoroutine("TutorialGacha");
    }
    // Update is called once per frame
}
