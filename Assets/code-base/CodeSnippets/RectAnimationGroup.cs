using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class RectAnimationGroup : MonoBehaviour
{
    public RectAnimation[] rectAnimations;
    //TODO: Add disabling when animation ends

    void Start()
    {

    }

    void Update()
    {

    }

    [Button]
    public void FillListWithChilderen()
    {
        rectAnimations = transform.GetComponentsInChildren<RectAnimation>();
    }

    [Button]
    public void Trigger()
    {
        foreach (RectAnimation anim in rectAnimations)
        {
            anim.Trigger();
        }
    }
}
