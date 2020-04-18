using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class BigText : MonoBehaviour
{
    public float waitTime;
    public float flashSpeed;

    public Color defFlasherCol;

    [Header("References")]

    public Image flasher;
    public TextMeshPro text;

    void Start()
    {
        Show(new string[6] { "THEY", "ARE", "SICK", "SICK", "HEAL", "THEM" });
    }

    public void Show(string[] texts)
    {
        StartCoroutine(showEnum(texts));
    }

    IEnumerator showEnum(string[] texts)
    {
        string lastText = "";

        for (int i = 0; i < texts.Length; i++)
        {
            if (lastText != texts[i])
            {

                text.text = texts[i];

                flasher.color = Color.white;

                flasher.DOColor(defFlasherCol, flashSpeed);

                float defTextScale = text.transform.localScale.x;

                text.transform.localScale = Vector3.one * defTextScale * 1.8f;

                text.transform.DOScale(Vector3.one * defTextScale, flashSpeed);

                lastText = texts[i];

            }

            yield return new WaitForSeconds(waitTime);
        }

        yield return new WaitForSeconds(1);

        Color newCol = flasher.color;

        newCol.a = 0;

        flasher.DOColor(newCol, 0.5f);

        text.DOColor(newCol, 0.5f);
    }
}
