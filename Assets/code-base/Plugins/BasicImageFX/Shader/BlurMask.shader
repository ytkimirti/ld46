Shader "Custom/Mobile/BlurMask"
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "" {}
	}

	CGINCLUDE
	#include "UnityCG.cginc"
	struct v2fb {
		fixed4 pos : POSITION;
		fixed2 uv  : TEXCOORD0;
		fixed4 uv1 : TEXCOORD1;
		fixed4 uv2 : TEXCOORD2;
	};
	struct v2f {
		fixed4 pos : POSITION;
		fixed2 uv : TEXCOORD0;
	};


	sampler2D _MainTex;
	sampler2D _MaskTex;
	sampler2D _BlurTex;
	uniform fixed4 _ScrRes;

	v2fb vertBlur( appdata_img v ) 
	{
		v2fb o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv =  v.texcoord.xy;	
		o.uv1.xy = v.texcoord.xy + fixed2(-_ScrRes.x, -_ScrRes.y);
		o.uv1.zw = v.texcoord.xy + fixed2(-_ScrRes.x, _ScrRes.y);
		o.uv2.xy = v.texcoord.xy + fixed2(_ScrRes.x, -_ScrRes.y);
		o.uv2.zw = v.texcoord.xy + fixed2(_ScrRes.x, _ScrRes.y);
		return o;
	} 
	v2f vert(appdata_img v)
	{
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = v.texcoord.xy;
		return o;
	}

	fixed4 frag(v2fb i) : COLOR 
	{
		fixed4 result = tex2D(_MainTex, i.uv);
		result += tex2D(_MainTex, i.uv1.xy);
		result += tex2D(_MainTex, i.uv1.zw);
		result += tex2D(_MainTex, i.uv2.xy);
		result += tex2D(_MainTex, i.uv2.zw);
		return result / 4.0;
	}
	fixed4 fragBlur(v2f i) : COLOR
	{
		fixed4 m = tex2D(_MaskTex, i.uv);
		fixed4 c = tex2D(_MainTex, i.uv);
		fixed4 b = tex2D(_BlurTex, i.uv);
		return lerp(c,b	, m.r);
	}


	ENDCG 
		
	Subshader 
	{
		Pass 
		{
		  ZTest Always Cull Off ZWrite Off
		  Fog { Mode off }      
	      CGPROGRAM
	      #pragma vertex vertBlur
	      #pragma fragment frag
	      #pragma fragmentoption ARB_precision_hint_fastest
		  #pragma target 3.0
	      ENDCG
	  	}
		Pass
		{
		  ZTest Always Cull Off ZWrite Off
		  Fog { Mode off }
		  CGPROGRAM
		  #pragma vertex vert
		  #pragma fragment fragBlur
		  #pragma fragmentoption ARB_precision_hint_fastest
		  #pragma target 3.0
		  ENDCG
		}
		
	}

	Fallback off
	}