using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextWriterSanmaIf : MonoBehaviour
{
    public Uitext uitext;
    public ImageChange change;
    public LoadingScreenManager load;
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
            load.LoadLoadingScreen();
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("Home");
        }
        yield return 0;
    }
    // ���͂�\��������R���[�`��
    IEnumerator Cotest()
    {
        uitext.DrawText("�˗���", "�T���}����������Ȃ������ł��ˁE�E�E");
        yield return StartCoroutine("Skip");

        uitext.DrawText("�T��", "�܂��܂�10�񂵂������ĂȂ�����˂��B");
        yield return StartCoroutine("Skip");

        uitext.DrawText("�˗���", "�����ł��ˁB�C���ɃK�`�����񂵑����܂����B");
        yield return StartCoroutine("Skip");

        change.ChangeSmile();
        uitext.DrawText("�T��", "�����B�������܂��傤�B");
        yield return StartCoroutine("Skip");

        yield return StartCoroutine("Home");
    }

}
