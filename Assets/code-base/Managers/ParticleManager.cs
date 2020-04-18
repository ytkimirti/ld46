using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    public ParticleSystem[] particles;

    public static ParticleManager main;

    void Awake()
    {
        main = this;
    }

    void playParticleSystemAndChild(ParticleSystem p)
    {

        p.Play();

        if (p.transform.childCount != 0)
        {
            p.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        }
    }



    public void play(int particleId)
    {
        ParticleSystem p = particles[particleId];
        playParticleSystemAndChild(p);
    }

    public void play(int particleId, Vector3 pos)
    {
        ParticleSystem p = particles[particleId];

        p.transform.position = pos;

        playParticleSystemAndChild(p);

    }

    public void play(int particleId, Vector3 pos, Vector3 rot)
    {
        ParticleSystem p = particles[particleId];

        p.transform.position = pos;
        p.transform.eulerAngles = rot;

        playParticleSystemAndChild(p);

    }

    public void play(int particleId, Vector3 pos, Color col)
    {
        ParticleSystem p = particles[particleId];

        p.transform.position = pos;

        var main = p.main;
        main.startColor = new ParticleSystem.MinMaxGradient(col);

        playParticleSystemAndChild(p);
    }

    public void play(int particleId, Vector3 pos, Vector3 rot, Color col)
    {
        ParticleSystem p = particles[particleId];

        p.transform.position = pos;
        p.transform.eulerAngles = rot;

        var main = p.main;
        main.startColor = new ParticleSystem.MinMaxGradient(col);

        playParticleSystemAndChild(p);
    }
}
