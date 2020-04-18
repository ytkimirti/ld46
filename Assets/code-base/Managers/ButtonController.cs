using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{

    public bool playSoftSound;

    [Header("Shining")]

    public AnimationCurve shineCurve;

    float shineCurveTimer;

    bool shining = false;

    public bool isShining;

    public float shineSpeed;
    public float shineTime;
    float shineTimer;

    float memShineX;

    [Header("ScaleEffect")]

    public AnimationCurve scaleCurve;

    float scaleCurveTimer;

    bool scaling = false;

    public float scaleEffectAmount;
    public float scaleEffectSpeed;
    Vector2 memScale;
    [Space]
    public RectTransform trans;
    public RectTransform shineTrans;

    void Start()
    {
        memScale = trans.sizeDelta;

        if (isShining)
            memShineX = shineTrans.anchoredPosition.x;
    }

    void Update()
    {

        float deltaTime = Time.deltaTime / Time.timeScale;

        if (isShining)
        {
            shineTimer += deltaTime;
            if (shineTimer > shineTime)
            {
                shine();
                shineTimer = 0;
            }
        }

        if (scaling)
        {
            scaleCurveTimer += deltaTime * scaleEffectSpeed;

            if (scaleCurveTimer > 1)
            {
                scaling = false;
                scaleCurveTimer = 0;
                trans.sizeDelta = memScale;
            }
            else
            {
                float amount = scaleCurve.Evaluate(scaleCurveTimer) * scaleEffectAmount;

                trans.sizeDelta = new Vector2(memScale.x + amount, memScale.y + amount);
            }
        }

        if (shining)
        {
            shineCurveTimer += deltaTime * shineSpeed;

            if (shineCurveTimer > 1)
            {
                shining = false;
                shineCurveTimer = 0;
                shineTrans.anchoredPosition = new Vector2(memShineX, shineTrans.anchoredPosition.y);
            }
            else
            {
                shineTrans.anchoredPosition = new Vector2(Mathf.Lerp(memShineX, -memShineX, shineCurve.Evaluate(shineCurveTimer)), shineTrans.anchoredPosition.y);

            }
        }
    }

    void shine()
    {
        if (shining)
            return;

        shining = true;
    }

    public void OnClicked()
    {
        scaleCurveTimer = 0;

        scaling = true;

        if (playSoftSound)
            AudioManager.main.Play("click_soft");
        else
            AudioManager.main.Play("click");
    }
}
