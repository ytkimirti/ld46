using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isLoosed;
    public bool isGameOver;

    [Space]

    public float fishJumpHeight;

    [Header("References")]

    public Transform waterTrans;
    public float waterRadius;

    public Splasher waterSplasher;
    public Splasher panSplasher;

    public List<Fish> currFishes = new List<Fish>();

    //SINGLETON
    public static GameManager main;

    private void Awake()
    {
        main = this;
    }

    void Start()//-start
    {

    }

    public void OnNextWave()
    {

    }

    public void Loose()//-loose
    {
        if (isGameOver)
            return;

        isLoosed = true;

        Invoke("RestartGame", 2f);

        print("LOOSE");

        GameOver();
    }

    public void Win()//-win
    {
        if (isGameOver)
            return;

        isLoosed = false;

        print("WIN");

        GameOver();
    }

    void GameOver()//-gameover
    {
        if (isGameOver)
            return;

        isGameOver = true;

        //Game over code
    }

    public void RestartGame()//-restart
    {
        if (Fader.main)
        {
            Fader.main.FadeIn();

            Invoke("ReloadScene", Fader.main.fadeSpeed);
        }
        else
        {
            ReloadScene();
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }

    private void OnDrawGizmos()
    {

    }

    void Update()//-update
    {

    }
}
