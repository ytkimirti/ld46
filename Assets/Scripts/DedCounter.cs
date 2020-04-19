using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DedCounter : MonoBehaviour
{
    public Color dedColor;
    public Image[] fishImages;
    int memCount;

    void Start()
    {

    }

    void Update()
    {

    }

    public void UpdateCount(int count)
    {
        if (memCount != count)
        {
            Image targetImage = fishImages[count - 1];

            targetImage.DOColor(dedColor, 0.25f).SetUpdate(true);

            targetImage.transform.DOScale(1.3f, 0.25f).SetUpdate(true);

            targetImage.transform.DOScale(1, 0.4f).SetDelay(1.6f).SetUpdate(true);
        }

        memCount = count;
    }
}
