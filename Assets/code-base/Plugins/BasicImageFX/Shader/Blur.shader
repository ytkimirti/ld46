Shader "Custom/Mobile/Blur"
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
		fixed4 uv1 : TEXCOORD1;
		fixed4 uv2 : TEXCOORD2;
	};


	sampler2D _MainTex;
	sampler2D _MaskTex;
	uniform fixed4 _ScrRes;

	v2f vert( appdata_img v ) 
	{
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv =  v.texcoord.xy;	
		o.uv1.xy = v.texcoord.xy + fixed2(-_ScrRes.x, -_ScrRes.y);
		o.uv1.zw = v.texcoord.xy + fixed2(-_ScrRes.x, _ScrRes.y);
		o.uv2.xy = v.texcoord.xy + fixed2(_ScrRes.x, -_ScrRes.y);
		o.uv2.zw = v.texcoord.xy + fixed2(_ScrRes.x, _ScrRes.y);
		return o;
	} 

	fixed4 fragEdge(v2f i) : COLOR 
	{
		fixed4 result = tex2D(_MainTex, i.uv);
		result += tex2D(_MainTex, i.uv1.xy);
		result += tex2D(_MainTex, i.uv1.zw);
		result += tex2D(_MainTex, i.uv2.xy);
		result += tex2D(_MainTex, i.uv2.zw);
		return result / 4.0;
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
	      #pragma fragment fragEdge
	      #pragma fragmentoption ARB_precision_hint_fastest
		  #pragma target 3.0
	      ENDCG
	  	}
		
	}

	Fallback off
	}