using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class SpriteFlasher : MonoBehaviour
{
    public bool isMultiple = false;
    public bool isUnscaledTime = false;

    [HideIf("isMultiple")]
    public SpriteRenderer spriteRenderer;
    [ShowIf("isMultiple")]
    public SpriteRenderer[] spriteRenderers;

    [Space]

    public float flashTime;
    public int flashCount;

    [Space]

    public Color flashedColor = Color.white;

    [Space]

    List<Color> defColors;
    bool isFlashing;

    void Start()
    {

    }

    void Update()
    {

    }

    [Button]
    public void Flash()
    {
        Flash(flashTime, flashCount);
    }

    public void Flash(int _flashCount)
    {
        Flash(flashTime, _flashCount);
    }

    public void Flash(float _flashTime, int _flashCount)
    {
        if (isFlashing)
            return;

        StartCoroutine(FlashEnum(_flashTime, _flashCount));
    }

    IEnumerator FlashEnum(float _flashTime, int _flashCount)
    {
        isFlashing = true;

        for (int i = 0; i < _flashCount; i++)
        {
            FlashSprite();

            if (isUnscaledTime)
                yield return new WaitForSecondsRealtime(_flashTime);
            else
                yield return new WaitForSeconds(_flashTime);

            UnflashSprite();

            if (isUnscaledTime)
                yield return new WaitForSecondsRealtime(_flashTime);
            else
                yield return new WaitForSeconds(_flashTime);
        }

        isFlashing = false;
    }



    void FlashSprite()
    {
        defColors = new List<Color>();

        if (isMultiple)
        {
            foreach (SpriteRenderer spriteRen in spriteRenderers)
            {
                defColors.Add(spriteRen.color);
                spriteRen.color = flashedColor;
            }
        }
        else
        {
            defColors.Add(spriteRenderer.color);
            spriteRenderer.color = flashedColor;
        }
    }

    void UnflashSprite()
    {
        if (isMultiple)
        {
            for (int i = 0; i < spriteRenderers.Length; i++)
            {
                spriteRenderers[i].color = defColors[i];
            }
        }
        else
        {
            spriteRenderer.color = defColors[0];
        }
    }
}
