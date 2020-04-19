using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    Hand hand;

    void Start()
    {
        hand = GetComponent<Hand>();
    }

    void Update()
    {
        hand.input = Input.mousePosition;

        hand.isHolding = Input.GetKey(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            hand.AttemptCatch();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            hand.AttempDrop();
        }
    }
}
