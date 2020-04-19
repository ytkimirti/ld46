using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float bigTextTime;

    [Space]

    public float targetTime;

    public float changeSpeed;

    public static TimeManager main;

    private void Awake()
    {
        main = this;
    }

    void Start()
    {

    }

    void Update()
    {
        targetTime = bigTextTime;

        Time.timeScale = Mathf.MoveTowards(Time.timeScale, targetTime, changeSpeed * Time.unscaledDeltaTime);

        Time.fixedDeltaTime = Time.timeScale * .02f;
    }
}
