using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Flamable : MonoBehaviour
{

    public bool isDed;
    public bool isSource;
    [Space]
    public float currFlame = 0;
    public bool isBurning = false;

    [Space]

    public bool igniteAtStart = false;

    [Space]

    public float maxFlame = 100f;

    [Range(0, 1)]
    public float burnThreshold = 0.2f;

    public float selfDampSpeed = 4f;
    //How fast this will ignite others and turn itself to ash
    public float burnSpeed = 14f;

    [Range(0, 1)]
    public float burnNearFlamesPercent;

    [Space]

    public float radius = 3f;
    public LayerMask flamableLayer;

    Flamable[] nearFlames;

    void Start()
    {
        if (isSource)
        {
            currFlame = maxFlame;
        }

        if (igniteAtStart)
        {
            Ignite();
        }
    }

    void Update()
    {
        if (isDed)
            return;

        if (isBurning)
        {
            if (isSource)
                currFlame -= burnSpeed * Time.deltaTime;
            else
                currFlame += burnSpeed * Time.deltaTime;
        }
        else
        {
            if (currFlame > 0)
                currFlame -= selfDampSpeed * Time.deltaTime;
        }

        UpdateNearFlames();

        BurnFlames(nearFlames);

        CheckFlame();
    }

    public void Burn(float amount)
    {
        currFlame += amount * Time.deltaTime;
    }

    void CheckFlame()
    {
        if (!isBurning && currFlame > burnThreshold * maxFlame)
        {
            Ignite();
        }

        if (isBurning)
        {
            if (isSource && currFlame < 0)
            {
                Die();
            }
            else if (!isSource && currFlame > maxFlame)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        if (isDed)
            return;

        print("Ma flames ded");
        isDed = true;
        isBurning = false;
        currFlame = 0;
    }

    public void Ignite()
    {
        if (isBurning)
            return;

        isBurning = true;
        print("AHH " + name + " BURNIN");
    }

    void UpdateNearFlames()
    {
        nearFlames = findNearFlames();

        nearFlames = nearFlames.Where(x => x && !x.isBurning && !x.isDed).Select(x => x).ToArray();
    }

    Flamable[] findNearFlames()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, radius, flamableLayer);

        return cols.Where(x => x && x.gameObject.GetInstanceID() != this.gameObject.GetInstanceID()).Select(x => x.gameObject.GetComponent<Flamable>()).ToArray();
    }

    void BurnFlames(Flamable[] flames)
    {
        foreach (Flamable flame in flames)
        {
            if (flame && !flame.isBurning && !flame.isDed)
            {
                flame.Burn(burnSpeed * burnNearFlamesPercent);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (isDed)
        {
            Gizmos.color = Color.grey;

            Gizmos.DrawWireSphere(transform.position, 0.2f);
        }
        else if (isBurning || igniteAtStart)
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(transform.position, radius);
        }
        else
        {
            Gizmos.color = Color.green;

            Gizmos.DrawWireSphere(transform.position, 0.2f);
        }
    }
}
