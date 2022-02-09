using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaResultUI : MonoBehaviour
{
    public GameObject UIItem;
    private List<GameObject> UIItemList;
    // Start is called before the first frame update
    void Start()
    {
        UIItemList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowResult(List<GachaItem> items)
    {
        foreach(GameObject UIItem in UIItemList)
        {
            Destroy(UIItem);
        }

        UIItemList.Clear();

        foreach(GachaItem item in items)
        {
            GameObject instance = Instantiate(UIItem, transform);
            UIItemList.Add(instance);

            // TODO: 追加したUIアイテムにガチャ結果を表示する
            instance.GetComponentInChildren<Image>().sprite = item.itemImage;
        }
    }
}
