using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using EZCameraShake;

public class BigText : MonoBehaviour
{
    public float waitTime;
    public float flashSpeed;
    public float slowMoSpeed = 0.01f;

    public Color defFlasherCol;

    [Header("References")]

    public Image flasher;
    public TextMeshProUGUI text;

    public static BigText main;

    private void Awake()
    {
        main = this;
    }

    void Start()
    {
        //Show(new string[6] { "THEY", "ARE", "SICK", "SICK", "HEAL", "THEM" });
    }

    public void Show(string[] texts, bool useSlowMo, float delay)
    {
        StopAllCoroutines();

        StartCoroutine(showEnum(texts, useSlowMo, delay));
    }

    IEnumerator showEnum(string[] texts, bool useSlowMo, float delay)
    {

        if (useSlowMo)
        {
            CameraShaker.Instance.ShakeOnce(2, 5, 0, 1f);
            yield return new WaitForSecondsRealtime(delay);
            TimeManager.main.bigTextTime = slowMoSpeed;
            yield return new WaitForSecondsRealtime(1);
        }

        flasher.gameObject.SetActive(true);

        text.gameObject.SetActive(true);

        text.color = Color.white;

        string lastText = "";

        for (int i = 0; i < texts.Length; i++)
        {
            if (lastText != texts[i])
            {
                text.text = texts[i];

                flasher.color = Color.white;

                flasher.DOColor(defFlasherCol, flashSpeed).SetUpdate(true);

                float defTextScale = text.transform.localScale.x;

                text.transform.localScale = Vector3.one * defTextScale * 1.4f;

                text.transform.DOScale(Vector3.one * defTextScale, flashSpeed).SetUpdate(true);

                lastText = texts[i];

            }

            yield return new WaitForSecondsRealtime(waitTime);
        }

        yield return new WaitForSecondsRealtime(1);

        GameManager.main.OnNextWaveAction();

        Color newCol = flasher.color;

        newCol.a = 0;

        flasher.DOColor(newCol, 0.5f).SetUpdate(true);

        text.DOColor(newCol, 0.5f).SetUpdate(true);

        TimeManager.main.bigTextTime = 1;

        Spawner.main.waveBar.Show(Spawner.main.currWaveID + 1);

        yield return new WaitForSecondsRealtime(0.5f);

        flasher.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
    }
}
