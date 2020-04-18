using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    public bool isBurning;
    public float flameAmount;
    public float smokeAmount;

    [Header("Visual")]
    public float coreSinSpeed;
    public float coreSinScale;

    public Transform flameVisual;

    Flamable flamable;

    void Start()
    {
        flamable = GetComponent<Flamable>();
    }

    void Update()
    {
        isBurning = flamable.isBurning;

        smokeAmount = Mathf.Clamp01(flamable.currFlame / (flamable.burnThreshold * flamable.maxFlame));
        flameAmount = Mathf.Clamp01(flamable.currFlame / flamable.maxFlame);

        flameVisual.gameObject.SetActive(isBurning);

        float flameScale = Mathf.Clamp01(flameAmount + Mathf.Sin(Time.time * coreSinSpeed) * coreSinScale);

        flameVisual.localScale = Vector3.one * flameScale;
    }
}
