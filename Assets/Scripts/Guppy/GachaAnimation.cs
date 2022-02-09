using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaAnimation : MonoBehaviour
{
    public Transform targetTransform;

    private bool gachaAnimPlaying;
    private Queue<GachaItem> gachaResults;
    private float animDuration = 0.0f;
    private GachaItem currentGachaItem;

    private GameObject currentLive2DObject;

    // Start is called before the first frame update
    void Start()
    {
        gachaResults = new Queue<GachaItem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gachaAnimPlaying)
        {
            if(animDuration <= 0.0f)
            {
                NextGachaItem();
            }
            animDuration -= Time.deltaTime;
        }
    }

    public void AddGachaItem(GachaItem item)
    {
        gachaResults.Enqueue(item);
    }

    public void StartAnimation()
    {
        gachaAnimPlaying = true;
        NextGachaItem();
    }

    public void NextGachaItem()
    {
        if(gachaResults.Count > 0)
        {
            currentGachaItem = gachaResults.Dequeue();
            animDuration = currentGachaItem.live2DShowDuration;
            SetLive2DObject(currentGachaItem.live2DModel);
        }
        else
        {
            gachaAnimPlaying = false;
            ClearLive2DObject();
        }
    }

    public bool IsAnimPlaying()
    {
        return gachaAnimPlaying;
    }

    private void SetLive2DObject(GameObject live2DObject)
    {
        if (currentLive2DObject)
        {
            Destroy(currentLive2DObject);
        }
        currentLive2DObject = Instantiate(live2DObject, targetTransform);
    }

    private void ClearLive2DObject()
    {
        Destroy(currentLive2DObject);
        currentLive2DObject = null;
    }
}
