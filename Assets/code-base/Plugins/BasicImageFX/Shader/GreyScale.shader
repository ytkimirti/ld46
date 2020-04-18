Shader "Custom/Mobile/GreyScal"
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
	uniform fixed _Amount;

	v2f vert( appdata_img v ) 
	{
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv =  v.texcoord.xy;	
		return o;
	} 

	fixed4 fragGreyScale(v2f i) : COLOR 
	{
		 half4 c = tex2D(_MainTex, i.uv);
		 c.rgb = dot(c.rgb, float3(0.3, 0.59, 0.11)*_Amount);
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
	      #pragma fragment fragGreyScale
	      #pragma fragmentoption ARB_precision_hint_fastest
		  #pragma target 3.0
	      ENDCG
	  	}
		
	}

	Fallback off
	}