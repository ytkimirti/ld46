using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Fader : MonoBehaviour
{

    public bool fadeAtStart = true;
    public float fadeSpeed;
    Image image;

    public static Fader main;

    private void Awake()
    {
        main = this;


        image = GetComponent<Image>();
    }

    void Start()
    {
        if (fadeAtStart)
            FadeOut();
    }

    public void FadeIn()
    {
        image.DOKill();
        image.enabled = true;
        image.color = new Color(0, 0, 0, 0);
        image.DOColor(Color.black, fadeSpeed).SetUpdate(true);
    }

    public void FadeOut()
    {
        image.DOKill();
        image.enabled = true;
        image.color = Color.black;
        image.DOColor(new Color(0, 0, 0, 0), fadeSpeed).OnComplete(Disable).SetUpdate(true);
    }

    void Disable()
    {
        image.enabled = false;
    }
}
