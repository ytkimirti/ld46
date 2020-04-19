using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector2 offset;
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

        holder.localPosition = new Vector3(0, offset.y, offset.x);
    }

    void FixedUpdate()
    {
        UpdateHolder();
    }

    void Update()
    {

    }
}
