using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GlowLoop : MonoBehaviour
{
    public Image image;
    public TextMeshPro text;
    public TextMeshProUGUI uiText;
    public SpriteRenderer sprite;

    [Space]

    public Color targetColor;

    public Color emptyColor;

    private bool isThereARenderer;
    public float speed;

    void Start()
    {
        if (image) image.DOColor(targetColor, speed).SetLoops(-1, LoopType.Yoyo);

        if (text) text.DOColor(targetColor, speed).SetLoops(-1, LoopType.Yoyo);

        if (uiText) uiText.DOColor(targetColor, speed).SetLoops(-1, LoopType.Yoyo);

        if (sprite) sprite.DOColor(targetColor, speed).SetLoops(-1, LoopType.Yoyo);
    }

    public void Restart()
    {
        image.DOKill();

        if (image) image.DOColor(targetColor, speed).SetLoops(-1, LoopType.Yoyo);

        if (text) text.DOColor(targetColor, speed).SetLoops(-1, LoopType.Yoyo);

        if (uiText) uiText.DOColor(targetColor, speed).SetLoops(-1, LoopType.Yoyo);

        if (sprite) sprite.DOColor(targetColor, speed).SetLoops(-1, LoopType.Yoyo);
    }

    void Update()
    {

    }
}
