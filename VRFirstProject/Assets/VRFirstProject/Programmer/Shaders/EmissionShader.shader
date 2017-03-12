Shader "Custom/EmissionShader" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_EmissionColor("Emission Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_EmissionTex("Emission Texture", 2D) = "white" {}
	}
		SubShader{
			Tags { "RenderType" = "Opaque" }
			LOD 200

			CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0
		half4 _EmissionColor;
		sampler2D _MainTex;
		sampler2D _EmissionTex;

		struct Input {
			float2 uv_MainTex;
		};
		float mod(float a, float b)
		{
			return a - floor(a / b) * b;
		}

		void surf(Input IN, inout SurfaceOutput o) {
			// Albedo comes from a texture tinted by color
			half4 c = tex2D(_MainTex, IN.uv_MainTex);
			float t = mod(IN.uv_MainTex.x - (_Time.y * 0.5), 1.0);
			float e = tex2D(_EmissionTex, IN.uv_MainTex).r * t * t;

			o.Albedo = c.rgb;
			o.Alpha = c.a;

			o.Emission = _EmissionColor * e;
		}
		ENDCG
	}
		FallBack "Diffuse"
}
