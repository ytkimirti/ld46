﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool isGameStarted;
    public bool isGameOver;

    bool gameWon;

    public int dedCount;

    [Space]

    public float fishJumpHeight;

    [Header("References")]

    public Transform waterTrans;
    public float waterRadius;

    public Splasher waterSplasher;
    public Splasher panSplasher;

    public DedCounter dedCounter;

    public ParticleSystem winParticle;

    public Edges retroFX;

    [Space]

    float gameTimer;
    public TextMeshProUGUI timeToCookText;
    public TextMeshProUGUI lastWaveText;
    public GameObject gameLostGO;
    public GameObject gameWonGO;
    public Animator gameOverAnim;

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
        int wave = Spawner.main.currWaveID + 1;

        if (wave != 1)
            winParticle.Play();

        if (wave == 5)
        {
            retroFX.enabled = false;
        }

        if (wave == 11)
        {
            gameWon = true;
            GameOver();
        }
    }

    public void OnNextWaveAction()
    {
        int wave = Spawner.main.currWaveID + 1;

        if (wave == 4)
        {
            CameraShaker.Instance.ShakeOnce(2.5f, 5, 0, 1f);

            retroFX.enabled = true;
            winParticle.Play();
        }
    }

    public void OnFishDed()
    {
        if (isGameOver)
            return;

        dedCount++;

        dedCounter.UpdateCount(dedCount);

        if (dedCount != 3)
        {
            Time.timeScale = 0.1f;
        }
        else
        {
            GameOver();
        }
    }

    public void OnStartPressed()
    {
        if (!CameraController.main.isMenu)
        {
            return;
        }

        CameraController.main.isMenu = false;

        Invoke("StartGame", 2f);
    }

    public void StartGame()
    {
        if (isGameStarted)
            return;

        isGameStarted = true;

        CameraController.main.isMenu = false;
    }

    public void GameOver()//-loose
    {
        if (isGameOver)
            return;

        isGameOver = true;

        CameraShaker.Instance.ShakeOnce(2, 5, 0, 1f);

        TimeManager.main.endGameTime = 0.002f;
        TimeManager.main.changeSpeed = TimeManager.main.changeSpeed / 4;

        if (gameWon)
        {
            winParticle.Play();
        }

        StartCoroutine(gameOverEnum());

        print("LOOSE");
    }

    IEnumerator gameOverEnum()
    {
        yield return new WaitForSecondsRealtime(4f);

        OpenGameOverMenu();
    }

    public void OpenGameOverMenu()
    {
        if (gameWon)
        {
            gameOverAnim.SetTrigger("GameWon");
        }
        else
        {
            gameOverAnim.SetTrigger("GameOver");
        }

        int wave = Spawner.main.currWaveID + 1;

        timeToCookText.text = "Time took to cook: " + Mathf.Round(gameTimer).ToString() + "s";

        lastWaveText.text = wave.ToString();
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
        if (!isGameOver)
            gameTimer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.K))
        {
            currFishes[Random.Range(0, currFishes.Count)]?.Die();
        }

        if (!isGameStarted && Input.GetKeyDown(KeyCode.Mouse0) && Input.mousePosition.y < Screen.height / 3)
        {
            OnStartPressed();
        }
    }
}
