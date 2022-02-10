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
            Debug.Log("チュートリアルガチャへ");
        }
        yield return 0;
    }

    // 文章を表示させるコルーチン
    IEnumerator Cotest()
    {
        uitext.DrawText("プロローグ");
        yield return StartCoroutine("Skip");

        uitext.DrawText("夏の暑さが和らぎ、木の葉の青々とした山はだんたんと色鮮やかになった。");
        yield return StartCoroutine("Skip");

        uitext.DrawText("その山を背景に、人はそれほど多くはないが、少しの平たい土地を奪い合うかのように乱立する建物がせめぎあう宿場町があった。");
        yield return StartCoroutine("Skip");


        uitext.DrawText("依頼主", "実はですねぇ、今年の夏頃に町の北の方に新しく魚屋ができたんですが・・・");
        yield return StartCoroutine("Skip");

        uitext.DrawText("探偵", "・・・");
        yield return StartCoroutine("Skip");

        uitext.DrawText("宿場の一室で依頼主と探偵は、机を挟み淹れたての緑茶を飲みながら会話をしている。");
        yield return StartCoroutine("Skip");

        uitext.DrawText("依頼主", "住人の話しによると、その店では新鮮な海の魚も売っているとのことなんですが、どうにも抽選機を回して出た色で買える魚が変わるという話で。");
        yield return StartCoroutine("Skip");

        uitext.DrawText("探偵", "ほうほう、それで");
        yield return StartCoroutine("Skip");

        uitext.DrawText("依頼主", "はい、それで私のほうでその店に聞くと、店員曰く「ガチャ？」という方式の売り方をしているみたいなんです。");
        yield return StartCoroutine("Skip");

        uitext.DrawText("探偵", "確かに変な売り方ですね。とても儲けが出る売り方とは思えないです。");
        yield return StartCoroutine("Skip");

        uitext.DrawText("依頼主", "私もそう思うのですが、何回抽選しても新鮮な海の魚が出てきたところは見たことがないと住人から陳情が来ていて、本当に海の魚を売っているか怪しいのです。");
        yield return StartCoroutine("Skip");

        uitext.DrawText("依頼主", "そこで探偵さんには、本当に新鮮な海の魚を仕入れているのかそのお店を調査してもらいたくて・・・");
        yield return StartCoroutine("Skip");

        uitext.DrawText("探偵", "・・・");
        yield return StartCoroutine("Skip");

        uitext.DrawText("部屋に一瞬の静寂が訪れる。探偵は少し考える素振りをし、口を開いた。");
        yield return StartCoroutine("Skip");

        uitext.DrawText("探偵", "分かりました。しかし、かなりの費用はかかると思います。");
        yield return StartCoroutine("Skip");

        uitext.DrawText("この宿場町は海からかなり離れており、新鮮な魚を運んでくるには相応の費用が掛かることは想像がつく。");
        yield return StartCoroutine("Skip");

        uitext.DrawText("しかし、話題に挙がった魚屋で本当に海の魚を仕入れているのかは分からない。");
        yield return StartCoroutine("Skip");

        uitext.DrawText("探偵はどことなく深い闇を感じた。");
        yield return StartCoroutine("Skip");

        uitext.DrawText("依頼主と探偵は、かき集めた軍資金を頼りに怪しい魚屋の闇を暴くことにした。");
        yield return StartCoroutine("Skip");
        yield return StartCoroutine("TutorialGacha");
    }
    // Update is called once per frame
}
