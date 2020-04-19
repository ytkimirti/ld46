using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{
    public float blinkTime;
    public float blinkTimeRandomness;
    public float blinkDelay;
    float blinkTimer;
    float currBlinkTime;

    public int currEyeState;
    public float rotateSpeed;
    float randomOffset;

    public Sprite[] eyeSprites;
    public SpriteRenderer eyeSpriteRen;
    public Transform eyeBall;
    public SpriteMask spriteMask;

    void Start()
    {
        randomOffset = Random.Range(0, 10);
    }

    public void Blink()
    {
        blinkTimer = 0;
    }

    void Update()
    {
        blinkTimer -= Time.deltaTime;

        int newEyeState = currEyeState;

        if (currEyeState != 2)
        {
            if (blinkTimer <= 0)
            {
                blinkTimer = Random.Range(blinkTime - blinkTimeRandomness, blinkTime + blinkTimeRandomness);
                currBlinkTime = blinkTimer;
            }
            else if (blinkTimer >= currBlinkTime - blinkDelay)
            {
                newEyeState = 1;
            }
        }

        if (newEyeState == 1 || newEyeState == 2 || newEyeState == 3)
        {
            eyeBall.gameObject.SetActive(false);
        }
        else
        {
            eyeBall.gameObject.SetActive(true);
            eyeBall.transform.localEulerAngles = new Vector3(0, 0, Time.deltaTime * Mathf.PerlinNoise(Time.time + randomOffset, 0) * rotateSpeed);
        }

        eyeSpriteRen.sprite = eyeSprites[newEyeState];
        spriteMask.sprite = eyeSpriteRen.sprite;
    }
}
