using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class AIHand : MonoBehaviour
{
    public Transform dropPos;
    public float handSpeed;
    Hand hand;

    List<int> currTasks = new List<int>();

    public static AIHand main;

    private void Awake()
    {
        main = this;
    }

    void Start()
    {
        hand = GetComponent<Hand>();

        currTasks = new List<int>();

        StartCoroutine(doTasks());
    }

    void Update()
    {

    }

    public void ThrowFishes(int count)
    {
        print("I HAVE SUMMONED " + count + " TIMES");
        currTasks.Add(count);
    }

    IEnumerator doTasks()
    {
        while (true)
        {
            //Wait until you have a task like a good boy
            while (currTasks.Count == 0)
            {
                Hide();
                yield return new WaitForEndOfFrame();
            }

            yield return StartCoroutine(throwFishEnum(currTasks[0]));

            currTasks.RemoveAt(0);

            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator throwFishEnum(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Fish fish = FindFish();

            while (!fish)
            {
                fish = FindFish();
                Hide();
                yield return new WaitForSeconds(0.2f * handSpeed);
            }

            hand.input = fish.transform.position;

            yield return new WaitForSeconds(0.5f * handSpeed);

            while (!fish)
            {
                fish = FindFish();
                yield return new WaitForSeconds(0.2f * handSpeed);
            }

            hand.Catch(fish);

            hand.isHolding = true;

            yield return new WaitForSeconds(0.5f * handSpeed);

            hand.input = dropPos.position;

            yield return new WaitForSeconds(0.5f * handSpeed);

            hand.AttempDrop();

            yield return new WaitForSeconds(0.2f * handSpeed);
        }
    }

    public void Hide()
    {
        hand.input.x = 5;
        hand.input.y = 2;
    }

    public Fish FindFish()
    {
        Fish[] waterFishes = GameManager.main.currFishes.Where(x => x.state == "water").ToArray();

        if (waterFishes.Length == 0)
            return null;

        return waterFishes[0];
    }
}
