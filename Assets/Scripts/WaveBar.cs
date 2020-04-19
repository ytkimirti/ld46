using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class WaveBar : MonoBehaviour
{
    public float maxTime;
    [Space]
    public float delay;
    public float showSpeed;
    public float showRest;
    public float barLerpSpeed;
    [Space]
    public Transform barTrans;
    public Transform textShowPos;
    public TextMeshProUGUI barText;
    float textDefPosY;
    public bool moveBar;

    void Start()
    {
        textDefPosY = barText.transform.position.y;

        moveBar = false;
    }

    void Update()
    {
        if (moveBar)
        {
            float targetScale = Spawner.main.waveTimer / maxTime;

            barTrans.localScale = new Vector3(Mathf.Lerp(barTrans.localScale.x, targetScale, Time.deltaTime * barLerpSpeed), 1, 1);
        }
    }

    public void Show(int currWave)
    {
        barText.text = "WAVE " + currWave.ToString();

        StartCoroutine(ShowEnum());
    }

    IEnumerator ShowEnum()
    {
        moveBar = true;

        yield return new WaitForSeconds(delay);

        barText.transform.DOMoveY(textShowPos.position.y, showSpeed);

        yield return new WaitForSecondsRealtime(showSpeed + showRest);

        barText.transform.DOMoveY(textDefPosY, showSpeed);

        moveBar = true;
    }
}
