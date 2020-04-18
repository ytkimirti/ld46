using UnityEngine;

public class Edges : MonoBehaviour{
    public Shader Shader;
	private Material material;
    static readonly int scrResString = Shader.PropertyToID("_ScrRes");
    static readonly int scrWidth = Screen.width;
    static readonly int scrHeight = Screen.height;
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
        material.SetVector(scrResString, new Vector2(1.0f / scrWidth, 1.0f / scrHeight));
        Graphics.Blit(source, destination, material, 0);
    }
}
