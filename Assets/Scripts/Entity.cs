using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Status")]

    public bool isDed;

    [Header("Input")]

    public Vector2 movementInput;
    public float angleInput;

    [Header("Movement")]

    public float movementSpeed;
    public float movementSmoothing;
    Vector2 smoothVel;

    [Header("Rotation")]

    public bool rotationByInput;
    public float rotationLerpSpeed;
    float targetRotation;

    [Header("References")]
    Rigidbody rb;

    [Header("DEBUG")]
    public bool showCollisions;

    Vector3[] gizmoBalls;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.centerOfMass = Vector3.zero;
    }

    void Update()
    {

    }

    Vector2 memPos;

    private void FixedUpdate()
    {
        Movement();

        Rotation();
    }

    void Movement()
    {
        Vector2 targetVel = movementInput * movementSpeed;

        rb.velocity = Vector2.SmoothDamp(rb.velocity.ToVector2(), targetVel, ref smoothVel, movementSmoothing).ToVector3();
    }

    void Rotation()
    {
        if (rotationByInput)
        {
            if (movementInput != Vector2.zero)
            {
                targetRotation = -movementInput.ToAngle() + 90;
            }
        }
        else
        {
            targetRotation = angleInput;
        }

        float newAngle = Mathf.LerpAngle(transform.localEulerAngles.y, targetRotation, rotationLerpSpeed);

        transform.localEulerAngles = Vector3.up * newAngle;
    }
}
