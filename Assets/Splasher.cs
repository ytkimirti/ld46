using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splasher : MonoBehaviour
{
    ParticleSystem particle;
    float defHeight;

    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        defHeight = transform.position.y;
    }

    public void Play(Vector3 pos)
    {
        transform.position = new Vector3(pos.x, defHeight, pos.z);
        particle.Play();
    }

    void Update()
    {

    }
}
