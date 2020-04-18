using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldDetector : MonoBehaviour
{
    public float moveForce;

    public bool useX, useY, useZ;

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        if (!enabled)
            return;



        if (other.tag == "Forcefield")
        {
            Vector3 forceFieldCenter = other.transform.position;

            Vector3 localVec = (transform.position - forceFieldCenter).normalized;

            if (!useX) forceFieldCenter.x = 0;
            if (!useY) forceFieldCenter.y = 0;
            if (!useZ) forceFieldCenter.z = 0;

            Vector3 force = moveForce * localVec;

            rb.AddForce(force);
        }
    }
}
