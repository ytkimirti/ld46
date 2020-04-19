Shader "YTKIMIRTISHADERSTM/MobileDiffuseDIColor"
{
    Properties
    {
        _Color("Color",COLOR)=(0.5,0.5,0.5,1.0)
        _Color2("Color2",COLOR)=(0.5,0.5,0.5,1.0)
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Mix("Color Mix",Range(0,1)) = 0.1
    }
 
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 150
        CGPROGRAM
        #pragma surface surf Lambert noforwardadd
 
        sampler2D _MainTex;
        fixed4 _Color;
        fixed4 _Color2;
        float _Mix;

 
        struct Input
        {
            float2 uv_MainTex;
        };
 
        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * lerp(_Color,_Color2,_Mix);
            o.Albedo = c.rgb;
            //o.Alpha = c.a;
        }
        ENDCG
    }
    Fallback "Mobile/VertexLit"
}