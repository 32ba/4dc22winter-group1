using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GachaWeight
{
    public GachaItem item;
    public int weight;
}

public class GachaManager : MonoBehaviour
{
    public List<GachaWeight> gachaList;

    public GachaResultUI resultUI;

    private Dictionary<GachaItem, int> gachaWeights;
    private int gachaAllWeight;
    private List<GachaItem> results;

    // Start is called before the first frame update
    void Start()
    {
        results = new List<GachaItem>();
        gachaWeights = new Dictionary<GachaItem, int>();
        gachaAllWeight = 0;

        foreach (GachaWeight weightData in gachaList)
        {
            gachaWeights[weightData.item] = weightData.weight;
            gachaAllWeight += weightData.weight;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGacha(int count)
    {
        if(DataManager.UsePoint(1000 * count) == false)
        {
            Debug.Log("ポイントが足りません！");
            return;
        }
        Debug.Log(DataManager.GetPoint());

        results.Clear();

        for(int i = 0; i < count; i++)
        {
            GachaItem result = GetGachaResult();
            results.Add(result);
        }

        resultUI.ShowResult(results);

        foreach(GachaItem item in results)
        {
            Debug.Log(item.name);
        }
    }

    public GachaItem GetGachaResult()
    {
        int randomResult = Random.Range(0, gachaAllWeight);

        foreach(KeyValuePair<GachaItem, int> elem in gachaWeights)
        {
            if(randomResult < elem.Value)
            {
                return elem.Key;
            }
            else
            {
                randomResult -= elem.Value;
            }
        }
        return null;
    }
}
