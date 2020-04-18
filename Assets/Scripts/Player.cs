using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Entity entity;

    public LayerMask groundLayer;

    float angle;

    public static Player main;

    private void Awake()
    {
        main = this;
    }

    void Start()
    {

    }

    Vector2 MousePos()
    {
        Camera cam = CameraController.main.cam;

        RaycastHit hit;

        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, 100000, groundLayer))
        {
            return hit.point.ToVector2();
        }

        return transform.position;
    }

    void Update()
    {
        Vector2 inp = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        entity.movementInput = inp;

        Vector2 localMP = MousePos() - transform.position.ToVector2();

        if (localMP != Vector2.zero)
            angle = -localMP.ToAngle() + 90;

        entity.angleInput = angle;
    }
}
