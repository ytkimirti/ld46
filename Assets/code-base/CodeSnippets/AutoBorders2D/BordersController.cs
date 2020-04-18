using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BordersController : MonoBehaviour
{
    public Vector3 offset;

    public bool stretch;

    public Transform borderR, borderL, borderU, borderD;

    public bool corners;

    public Transform cornerUR, cornerUL, cornerDR, cornerDL;

    public bool background;

    public Transform bg;

    public static BordersController main;

    void Awake()
    {
        main = this;
    }

    void Start()
    {

    }

    public void Rearrange(Vector2 scale)
    {
        borderR.transform.localPosition = Vector2.right * (scale.x - offset.x);
        borderL.transform.localPosition = Vector2.right * (-scale.x + offset.x);

        borderU.transform.localPosition = Vector2.up * (scale.y - offset.y);
        borderD.transform.localPosition = Vector2.up * (-scale.y + offset.y);

        borderR.transform.localPosition = new Vector3(borderR.transform.localPosition.x, borderR.transform.localPosition.y, offset.z);
        borderL.transform.localPosition = new Vector3(borderL.transform.localPosition.x, borderL.transform.localPosition.y, offset.z);
        borderU.transform.localPosition = new Vector3(borderU.transform.localPosition.x, borderU.transform.localPosition.y, offset.z);
        borderD.transform.localPosition = new Vector3(borderD.transform.localPosition.x, borderD.transform.localPosition.y, offset.z);


        if (stretch)
        {
            borderR.localScale = new Vector3(1, scale.y - offset.y, 1);
            borderL.localScale = new Vector3(1, scale.y - offset.y, 1);
            borderU.localScale = new Vector3(1, scale.x - offset.x, 1);
            borderD.localScale = new Vector3(1, scale.x - offset.x, 1);
        }

        if (corners)
        {
            cornerUR.localPosition = new Vector2(scale.x - offset.x, scale.y - offset.y);
            cornerUL.localPosition = new Vector2(-scale.x + offset.x, scale.y - offset.y);
            cornerDR.localPosition = new Vector2(scale.x - offset.x, -scale.y + offset.y);
            cornerDL.localPosition = new Vector2(-scale.x + offset.x, -scale.y + offset.y);
        }

        if (background)
        {
            bg.localScale = new Vector3(scale.x - offset.x, scale.y - offset.y, 1);
        }
    }

    void Update()
    {

    }
}
