using UnityEngine;

public class ImageFilter : MonoBehaviour{
    [Range(-180, 180)]
    public int Hue = 0;
    [Range(0,1)]
    public float Contrast = 0f;
    [Range(-1, 1)]
    public float Brightness = 0f;
    [Range(-1,1)]
    public float Saturation = 0f;
    public Shader Shader;
	private Material material;
    static readonly int hueCos = Shader.PropertyToID("_HueCos");
    static readonly int hueSin = Shader.PropertyToID("_HueSin");
    static readonly int hueVector = Shader.PropertyToID("_HueVector");
    static readonly int contrast = Shader.PropertyToID("_Contrast");
    static readonly int brightness = Shader.PropertyToID("_Brightness");
    static readonly int saturation = Shader.PropertyToID("_Saturation");
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
        material.SetFloat(hueCos, Mathf.Cos(Mathf.Deg2Rad*Hue));
        material.SetFloat(hueSin, Mathf.Sin(Mathf.Deg2Rad* Hue));
        material.SetVector(hueVector, new Vector3(0.57735f, 0.57735f, 0.57735f));
        material.SetFloat(contrast, Contrast+1f);
        material.SetFloat(brightness, Brightness/2);
        material.SetFloat(saturation, Saturation+1f);
        material.SetVector(scrResString, new Vector2(1.0f / scrWidth, 1.0f / scrHeight));
        Graphics.Blit(source, destination, material, 0);
    }
}
