using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.Linq;

public class Spawner : MonoBehaviour
{
    [SerializeField, System.Serializable]
    public class Wave
    {
        public float waveWait;
        public int newFishCount;
        public string[] waveText;
        [Space]
        public float handWaitTime = 5;
        public float handWaitTimeRandomness = 2;
        [Space]
        public float handSpeed = 1;
        [Range(1, 5)]
        public int maxFishDrop = 1;
    }

    [SerializeField]
    public Wave[] waves;
    public int currWaveID = -1;

    [ReadOnly]
    public Wave currWave;

    [ReadOnly]
    public float waveTimer;

    [ReadOnly]
    public float handTimer;

    public float slowMoDelay;

    [Space]

    public float spawnTime;
    public float spawnTimeRandomness;
    [ReadOnly]
    public float spawnTimer;

    [Space]
    public Transform spawnTrans;
    public GameObject fishPrefab;
    public WaveBar waveBar;

    bool isFirstFrame;

    public static Spawner main;

    private void Awake()
    {
        main = this;
    }

    void Start()
    {
        spawnTimer = 5;
        currWaveID = -1;

        isFirstFrame = true;

        AudioManager.main.SetMusic(0, true);
    }

    void Update()
    {
        if (!GameManager.main.isGameStarted || GameManager.main.isGameOver)
            return;

        waveTimer -= Time.deltaTime;

        if (waveTimer <= 0 || (Input.GetKeyDown(KeyCode.Space) && false))
        {
            NextWave();
        }

        handTimer -= Time.deltaTime;

        if (handTimer <= 0)
        {
            handTimer = Random.Range(currWave.handWaitTime - currWave.handWaitTimeRandomness, currWave.handWaitTime + currWave.handWaitTimeRandomness);

            if (!isFirstFrame)
                AIHand.main.ThrowFishes(Random.Range(1, currWave.maxFishDrop));
        }

        spawnTimer -= Time.deltaTime;

        if (spawnTimer < 0)
        {
            spawnTimer = Random.Range(spawnTime - spawnTimeRandomness, spawnTime + spawnTimeRandomness);

            if (!isFirstFrame)
                SpawnFish();
        }

        isFirstFrame = false;
    }

    public void NextWave()
    {
        if (currWaveID == waves.Length)
            return;

        if (currWaveID != -1 && GameManager.main.isInfiniteGameplay)
            return;

        currWaveID++;

        if (currWaveID != 10)
        {
            StartWave(waves[currWaveID]);

            waveTimer = waves[currWaveID].waveWait;

            waveBar.maxTime = currWave.waveWait;

            waveBar.moveBar = false;
        }

        GameManager.main.OnNextWave();
    }

    public void StartWave(Wave wave)
    {
        print("WAVE " + currWaveID + " STARTED, ");

        currWave = wave;

        StartCoroutine(dropNewFishes(wave.newFishCount));

        AIHand.main.ThrowFishes(wave.newFishCount);

        if (currWaveID > 5)
        {
            AIHand.main.hand.lerpSpeed += 1;
        }

        AIHand.main.handSpeed = currWave.handSpeed;

        //Filler texts
        string fillerStr = currWave.waveText[Random.Range(0, currWave.waveText.Length)];

        List<string> texts = fillerStr.Split(',').ToList();

        texts.Insert(0, "WAVE " + (currWaveID + 1).ToString());
        texts.Insert(0, "WAVE " + (currWaveID + 1).ToString());

        //textss = texts.ToArray();

        BigText.main.Show(texts.ToArray(), true, slowMoDelay);
    }

    IEnumerator dropNewFishes(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnFish();
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void SpawnFish()
    {
        Instantiate(fishPrefab, spawnTrans.position, Random.rotation);
    }
}
