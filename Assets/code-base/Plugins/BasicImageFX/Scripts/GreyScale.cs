using UnityEngine;

public class GreyScale : MonoBehaviour{
    [Range(1, 5)]
    public float amount = 2f;
    public Shader Shader;
	private Material material;
    static readonly int Amount = Shader.PropertyToID("_Amount");
    public void Start()
    {
        CreateMaterial();
    }
      
    public void CreateMaterial()
    {
        if (Shader == null)
        {
            material = null;
            return;
        }
        material = new Material(Shader);
        material.hideFlags = HideFlags.DontSave;
    }
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetFloat(Amount, amount);
        Graphics.Blit(source, destination, material, 0);
    }
}
