using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class MeshFlasher : MonoBehaviour
{
    public bool isMultiple = false;

    [HideIf("isMultiple")]
    public MeshRenderer meshRenderer;
    [ShowIf("isMultiple")]
    public MeshRenderer[] meshes;

    [Space]

    public float flashTime;
    public int flashCount;

    [Space]

    public Material flashedMaterial;

    List<Material[]> defMaterials;
    bool isFlashing;

    void Start()
    {

    }

    void Update()
    {

    }

    [Button]
    public void Flash()
    {
        Flash(flashTime, flashCount);
    }

    public void Flash(int _flashCount)
    {
        Flash(flashTime, _flashCount);
    }

    public void Flash(float _flashTime, int _flashCount)
    {
        if (isFlashing)
            return;

        StartCoroutine(FlashEnum(_flashTime, _flashCount));
    }

    IEnumerator FlashEnum(float _flashTime, int _flashCount)
    {
        isFlashing = true;

        for (int i = 0; i < _flashCount; i++)
        {
            FlashMeshes();

            yield return new WaitForSeconds(_flashTime);

            UnflashMesh();

            yield return new WaitForSeconds(_flashTime);
        }

        isFlashing = false;
    }


    void FlashMesh(MeshRenderer mesh)
    {
        defMaterials.Add(mesh.sharedMaterials);

        Material[] whiteMats = new Material[mesh.sharedMaterials.Length];

        for (int i = 0; i < whiteMats.Length; i++)
        {
            whiteMats[i] = flashedMaterial;
        }

        mesh.sharedMaterials = whiteMats;
    }

    void FlashMeshes()
    {
        defMaterials = new List<Material[]>();

        if (isMultiple)
        {
            foreach (MeshRenderer mesh in meshes)
            {
                FlashMesh(mesh);
            }
        }
        else
        {
            FlashMesh(meshRenderer);
        }
    }

    void UnflashMesh()
    {
        if (isMultiple)
        {
            for (int i = 0; i < meshes.Length; i++)
            {
                meshes[i].sharedMaterials = defMaterials[i];
            }
        }
        else
        {
            meshRenderer.sharedMaterials = defMaterials[0];
        }
    }
}
