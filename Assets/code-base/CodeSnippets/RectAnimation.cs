using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using NaughtyAttributes;

public class RectAnimation : MonoBehaviour
{


    public bool isOn;

    [Space]

    public bool useOffset = true;

    [ShowIf("useOffset")]
    public Vector2 moveOffset;

    [HideIf("useOffset")]
    public Vector2 movePosition;

    [Header("Animation Values")]

    public Ease ease;
    public float speed;

    [HideInInspector]
    public Vector2 defPosition;

    [HideInInspector]
    public RectTransform rect;

    void Awake()
    {
        rect = GetComponent<RectTransform>();

        defPosition = rect.anchoredPosition;

        if (isOn)
        {
            rect.anchoredPosition = getTargetPos();
        }
    }

    void OnValidate()
    {

    }

    void Update()
    {

    }

    Vector2 getTargetPos()
    {
        if (useOffset)
        {
            return defPosition + moveOffset;
        }
        else
        {
            return movePosition;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!rect)
        {
            rect = GetComponent<RectTransform>();
        }

        Vector2 memDefPosition = defPosition;

        defPosition = rect.position;

        Gizmos.color = Color.red;

        Gizmos.DrawSphere(defPosition, 15);

        Gizmos.color = Color.green;

        if (useOffset)
            Gizmos.DrawSphere(getTargetPos(), 15);
        else
            Gizmos.DrawSphere(getTargetPos() + rect.anchorMax, 15);



        defPosition = memDefPosition;
    }

    [Button]
    public void Trigger()
    {
        Vector2 targetPos = getTargetPos();

        isOn = !isOn;

        if (isOn)
        {

        }
        else
        {
            targetPos = defPosition;
        }

        rect.DOAnchorPos(targetPos, speed).SetEase(ease);
    }
}
