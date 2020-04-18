Shader "Custom/Mobile/ImageFilter"
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
	uniform fixed _HueCos;
	uniform fixed _HueSin;
	uniform fixed3 _HueVector;
	uniform fixed _Contrast;
	uniform fixed _Brightness;
	uniform fixed _Saturation;
	uniform fixed4 _ScrRes;
	uniform fixed _Sharpen;
	v2f vert( appdata_img v ) 
	{
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv =  v.texcoord.xy;	
		return o;
	} 

	fixed4 fragFilter(v2f i) : COLOR 
	{
		fixed4 c = tex2D(_MainTex, i.uv);
		c.rgb = c * _HueCos + cross(_HueVector,c.rgb)*_HueSin + _HueVector * dot(_HueVector,c.rgb)*(1 - _HueCos);
		c.rgb = (c.rgb - 0.5f) * (_Contrast)+0.5f + _Brightness;
		c.rgb = lerp(dot(c.rgb, float3(0.299, 0.587, 0.114)), c.rgb, _Saturation);
		return c;
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
	      #pragma fragment fragFilter
	      #pragma fragmentoption ARB_precision_hint_fastest
		  #pragma target 3.0
	      ENDCG
	  	}
	}

	Fallback off
	}