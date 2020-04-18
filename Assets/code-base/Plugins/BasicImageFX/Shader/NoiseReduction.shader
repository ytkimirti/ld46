Shader "Custom/Mobile/NoiseReduction"
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "" {}
	}

	CGINCLUDE
	#include "UnityCG.cginc"
	struct v2f {
		fixed4 pos : POSITION;
		fixed2 uv  : TEXCOORD0;
	};

	sampler2D _MainTex;
	uniform fixed4 _ScrRes;
	v2f vert( appdata_img v ) 
	{
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv =  v.texcoord.xy;	
		return o;
	} 

	fixed4 fragNoise(v2f i) : COLOR 
	{
		fixed3 result = tex2D(_MainTex, i.uv);
		result += tex2D(_MainTex, i.uv + fixed2(-_ScrRes.x, -_ScrRes.y));
		result += tex2D(_MainTex, i.uv + fixed2(-_ScrRes.x, 0));
		result += tex2D(_MainTex, i.uv + fixed2(-_ScrRes.x, _ScrRes.y));
		result += tex2D(_MainTex, i.uv + fixed2(0,-_ScrRes.y));
		result += tex2D(_MainTex, i.uv + fixed2(0, _ScrRes.y));
		result += tex2D(_MainTex, i.uv + fixed2(_ScrRes.x, -_ScrRes.y));
		result += tex2D(_MainTex, i.uv + fixed2(_ScrRes.x, 0));
		result += tex2D(_MainTex, i.uv + fixed2(_ScrRes.x, _ScrRes.y));
		return fixed4(result/9.0,1.0);
	}

	ENDCG 
		
	Subshader 
	{
		Pass 
		{
		  ZTest Always Cull Off ZWrite Off
		  Fog { Mode off }      
	      CGPROGRAM
	      #pragma vertex vert
	      #pragma fragment fragNoise
	      #pragma fragmentoption ARB_precision_hint_fastest
		  #pragma target 3.0
	      ENDCG
	  	}
	}

	Fallback off
	}