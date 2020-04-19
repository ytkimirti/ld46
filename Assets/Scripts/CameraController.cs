using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector2 offset;
    public Vector2 newOffset;
    public bool isMenu;
    public float angle;
    [Space]
    public float lerpSpeed;

    [Header("References")]

    public Transform holder;
    public Camera cam;

    Vector3 targetPos;

    public static CameraController main;

    private void Awake()
    {
        main = this;
    }

    void Start()
    {

    }

    private void OnValidate()
    {
        UpdateHolder();
    }

    void UpdateHolder()
    {
        holder.localEulerAngles = new Vector3(angle, 0, 0);

        Vector2 currOffset = newOffset;

        if (isMenu)
            currOffset = offset;

        Vector3 targetPos = new Vector3(0, currOffset.y, currOffset.x);

        if (Application.isPlaying)
            holder.localPosition = Vector3.Lerp(holder.localPosition, targetPos, lerpSpeed * Time.deltaTime);
        else
            holder.localPosition = new Vector3(0, newOffset.y, newOffset.x);
    }

    void LateUpdate()
    {
        UpdateHolder();
    }

    void Update()
    {

    }
}
