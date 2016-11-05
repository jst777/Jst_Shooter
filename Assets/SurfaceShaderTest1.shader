Shader "Custom/RimLightWithDetailTexture" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_BumpMap ("Bumpmap", 2D) = "bump" {}
		_RimColor ("Rim Color", Color) = (1,1,1,1)
		_RimPower ("Rim Power", Range(0.1,8.0)) = 1.0
		_Detail ("Detail", 2D) = "gray" {} //detail texture = Secondary Maps (or Detail maps) allow you to overlay a second set of textures on top of the main textures listed above.
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _BumpMap;
		sampler2D _Detail;

		struct Input {
			float2 uv_MainTex;
			float2 uv_BumpMap;
			float3 viewDir;
			//float2 uv_Detail;
			float4 screenPos;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

	    float4 _RimColor;
        float _RimPower;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			//o.Albedo *= tex2D(_Detail, IN.uv_Detail) * 2;

			//스크린 좌표를 UV좌표로 써서 따라 다님 
			float2 screenUV = IN.screenPos.xy / IN.screenPos.w;
			//screenUV *= float2(8,4);
			o.Albedo *= tex2D(_Detail, screenUV) * 4;
			o.Alpha = c.a;

			o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));

			half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
          o.Emission = _RimColor.rgb * pow (rim, _RimPower);
		}
		ENDCG
	}
	FallBack "Diffuse"
}
