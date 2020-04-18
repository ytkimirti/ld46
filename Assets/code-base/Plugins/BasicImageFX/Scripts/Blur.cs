using UnityEngine;

public class Blur : MonoBehaviour{
    [Range(0, 10)]
    public float BlurAmount = 2f;
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
        material.SetVector(scrResString, new Vector2(BlurAmount / scrWidth, BlurAmount / scrHeight));
        Graphics.Blit(source, destination, material, 0);
    }
}
