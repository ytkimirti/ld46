using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Fish : MonoBehaviour
{
    public bool isDed;
    [Space]
    public float health;
    public float damagePerJump;
    public float healPerSecond;
    public float damageOutsidePerSecond;
    [Space]
    public string state;
    [Space]
    [ReadOnly]
    public float jumpVel;
    public float jumpVelRandomness;
    public float randomHorizontalVel;
    public float randomAngularVel;
    [Space]

    public int currEye;
    public float swimForce;

    public LayerMask areasLayer;

    [ReadOnly]
    public Hand currHand;

    public Eye eyeR, eyeL;

    public GameObject fishVisualHolder;
    public GameObject dedVisualHolder;
    public int dedLayer;

    Vector3 currWaterTarget;

    public MeshRenderer meshRen;

    float waterTimer;
    public float waterLerpSpeed;

    Rigidbody rb;

    float memMatColor = 1;

    MeshFlasher flasher;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        flasher = GetComponentInChildren<MeshFlasher>();

        jumpVel = Mathf.Sqrt(2 * -Physics.gravity.y * GameManager.main.fishJumpHeight);

        GameManager.main.currFishes.Add(this);

        if (M.Change(30))
        {
            currEye = 3;
        }
    }

    void FixedUpdate()
    {
        if (isDed)
            return;

        if (state == "")
        {
            state = FindArea();

            //When entering water from outside
            if (state == "water")
            {
                flasher.Flash(2);
                GameManager.main.waterSplasher.Play(transform.position);
            }
        }


        if (state == "water")
        {
            waterTimer -= Time.deltaTime;

            if (waterTimer < 0)
            {
                waterTimer = Random.Range(1, 3);

                currWaterTarget = randomPosInsideWater();
            }

            rb.drag = 1.2f;
            rb.useGravity = false;

            Vector3 dir = (currWaterTarget - transform.position).normalized;

            rb.AddForce(transform.forward * swimForce);
            //rb.AddForce(dir * 0.3f * swimForce);

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir, Vector3.up), waterLerpSpeed * Time.fixedDeltaTime);

            state = FindArea();
        }
        else
        {
            rb.useGravity = true;
            rb.drag = 0;
        }
    }

    void UpdateMaterialColor(float mix)
    {
        if (flasher.isFlashing)
            return;

        foreach (Material mat in meshRen.materials)
        {
            mat.SetFloat("_Mix", 1 - mix);
        }
    }

    void Update()
    {
        if (isDed)
            return;

        if (health < 30)
        {
            currEye = 4;
        }
        if (currEye == 4 && health > 30)
        {
            currEye = 0;
        }

        eyeR.currEyeState = currEye;
        eyeL.currEyeState = currEye;

        if (state == "water")
        {
            health += healPerSecond * Time.deltaTime;
        }
        else
        {
            health -= damageOutsidePerSecond * Time.deltaTime;
        }

        CheckHealth();
    }

    void CheckHealth()
    {
        if (health < 0)
        {
            Die();
            return;
        }

        health = Mathf.Clamp(health, 0, 100);

        if (Mathf.Round(health / 10) != memMatColor)
        {
            UpdateMaterialColor(health / 100);
            memMatColor = Mathf.Round(health / 10);
        }
    }

    public void Die()
    {
        if (isDed)
            return;

        fishVisualHolder.SetActive(false);
        dedVisualHolder.SetActive(true);

        gameObject.layer = dedLayer;

        isDed = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(currWaterTarget, 0.1f);
    }

    Vector3 randomPosInsideWater()
    {
        return GameManager.main.waterTrans.position + Random.insideUnitSphere * GameManager.main.waterRadius;
    }

    public void GetHolded(Hand hand)
    {
        state = "holded";

        flasher.Flash();

        currHand = hand;

        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.parent = hand.visualTrans;
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.up * 90;
    }

    public void GetDropped()
    {
        transform.parent = null;
        state = FindArea();

        rb.isKinematic = false;
    }

    string FindArea()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, 0.1f, areasLayer);

        if (cols.Length == 0)
        {
            return "";
        }

        return cols[0].name;
    }

    public void Jump(Vector3 center)
    {
        if (state == "pan")
        {
            float newJumpVel = Random.Range(jumpVel - jumpVelRandomness, jumpVel + jumpVelRandomness);

            Vector2 toCenterDir = (center.ToVector2() * Random.Range(-0.1f, 1f) - transform.position.ToVector2()).normalized;



            if (isDed)
                rb.velocity = rb.velocity.ToVector2().ToVector3() + Vector3.up * newJumpVel;
            else
            {
                rb.velocity = (toCenterDir * randomHorizontalVel).ToVector3() + Vector3.up * newJumpVel;

                ParticleManager.main.play(1, transform.position);

                GameManager.main.panSplasher.Play(transform.position);
            }
            eyeR.Blink();
            eyeL.Blink();

            rb.angularVelocity = Random.insideUnitSphere * randomAngularVel;

            health -= damagePerJump;

            CheckHealth();

            flasher.Flash();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        string otherTag = other.collider.gameObject.tag;

        if (otherTag == "Pan")
        {
            Jump(other.transform.position);
            state = "pan";
        }
        else if (otherTag == "Ground")
        {
            state = "ground";
        }
    }
}
