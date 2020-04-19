using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool usePosInput;
    public Vector3 input;
    public bool isHolding;
    [Space]
    public GameObject defaultPlane;
    public LayerMask planesLayer;
    public LayerMask fishLayer;
    public Transform visualTrans;
    Vector3 visualPos;
    Fish currFish;

    public float lerpSpeed;

    MeshFlasher flasher;

    Camera cam;

    void Start()
    {
        flasher = GetComponentInChildren<MeshFlasher>();
        cam = CameraController.main.cam;
    }

    void LateUpdate()
    {
        transform.position = findPos(!isHolding);

        visualPos = Vector3.Lerp(visualPos, transform.position, lerpSpeed * Time.deltaTime);

        visualTrans.position = visualPos;
    }

    public void AttempDrop()
    {
        if (currFish)
        {
            currFish.GetDropped();
            currFish = null;
        }
    }

    public void AttemptCatch()
    {
        RaycastHit hit;

        if (Physics.Raycast(cam.ScreenPointToRay(input), out hit, Mathf.Infinity, fishLayer))
        {
            Fish fish = hit.collider.gameObject.GetComponent<Fish>();

            if (fish)
            {
                Catch(fish);
            }
        }
    }

    public void Catch(Fish fish)
    {
        //print("Catching fish");
        flasher.Flash();

        currFish = fish;

        fish.GetHolded(this);
    }

    public Vector3 findPos(bool isDefault)
    {
        if (usePosInput)
        {
            float zDistance = isDefault ? defaultPlane.transform.position.z : 0;

            return input + Vector3.forward * zDistance;
        }
        else
        {
            defaultPlane.SetActive(isDefault);

            RaycastHit hit;

            if (Physics.Raycast(cam.ScreenPointToRay(input), out hit, Mathf.Infinity, planesLayer))
            {
                return hit.point;
            }
        }

        return Vector3.zero;
    }
}
