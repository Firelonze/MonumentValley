Shader "FlatLighting/FlatLightingWithUnityShadows" {
	Properties {
		_LightNegativeX ("Light -X", Color) = (1,1,1,1)
		_LightNegative2X ("Light2 -X", Color) = (1,1,1,1)
		_GradientOriginOffsetNegativeX ("Gradient Width", Float) = 3.0
		_GradientWidthNegativeX ("Gradient Offset", Float) = 0.0

		_LightPositiveX ("Light X", Color) = (1,1,1,1)
		_LightPositive2X ("Light2 X", Color) = (1,1,1,1)
		_GradientOriginOffsetPositiveX ("Gradient Width", Float) = 3.0
		_GradientWidthPositiveX ("Gradient Offset", Float) = 0.0

		_LightNegativeZ ("Light -Z", Color) = (1,1,1,1)
		_LightNegative2Z ("Light2 -Z", Color) = (1,1,1,1)
		_GradientOriginOffsetNegativeZ ("Gradient Width", Float) = 3.0
		_GradientWidthNegativeZ ("Gradient Offset", Float) = 0.0

		_LightPositiveZ ("Light Z", Color) = (1,1,1,1)
		_LightPositive2Z ("Light2 Z", Color) = (1,1,1,1)
		_GradientOriginOffsetPositiveZ ("Gradient Width", Float) = 3.0
		_GradientWidthPositiveZ ("Gradient Offset", Float) = 0.0


		_LightNegativeY ("Light -Y", Color) = (1,1,1,1)
		_LightNegative2Y ("Light2 -Y", Color) = (1,1,1,1)
		_GradientOriginOffsetNegativeY ("Gradient Width", Float) = 3.0
		_GradientWidthNegativeY ("Gradient Offset", Float) = 0.0

		_LightPositiveY ("Light Y", Color) = (1,1,1,1)
		_LightPositive2Y ("Light2 Y", Color) = (1,1,1,1)
		_GradientOriginOffsetPositiveY ("Gradient Width", Float) = 3.0
		_GradientWidthPositiveY ("Gradient Offset", Float) = 0.0

		_MainTex("Main Texture", 2D) = "white" {}
		_LightAndTextureMix("Texture Power", Range(0.0, 1.0)) = 1.0

		_GradienColorGoal ("Gradient Goal Color", Color) = (1,1,1,1)
		_GradientBlending ("Gradient Blending" , Range(0.0, 1.0)) = 0.0
		_GradientUnitAxis ("Gradient Axis", Vector) = (0,1,0,0)
		_GradientWidth ("Gradient Width", Float) = 3.0
		_GradientOffset ("Gradient Offset", Float) = 0.0

		_FogColor ("Fog Color", Color) = (0,0,0,1)
		_FogBlendingWidth ("Fog Blending Width", Float) = 0.5
		_FogCameraOffset ("Fog Camera Offset", Float) = 100.0

		_UVChannel ("Lightmap UV Channel", Int) = 0
		_CustomLightmap ("Lightmap (Greyscale)", 2D) = "white" {}
		_ShadowTint ("Shadow Tint", Color) = (1,1,1,1)
		_ShadowBoost ("Shadow Boost", Range(0.0, 1.0)) = 0.0

		_Ambient_Light ("Ambient Light", Color) = (1,1,1,1)

		_BlendedLightColor ("Blended Light Color", Color) = (1,1,1,1)
		_BlendedLightIntensities ("Blended Light Intensities", Vector) = (0,1,0,0)
		[Toggle] _BlendedLightSmoothness ("Blended Light Smoothness", Float) = 0.0

		_UnityShadowPower ("Unity Shadow Power", Range(0.0, 1.0)) = 0.2
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" 
				"FlatLightingTag"="FlatLighting/FlatLightingWithUnityShadows"
				"FlatLightingDisableBatching"="False"
			}

		LOD 100

		Pass
		{
			Tags { "LightMode"="ForwardBase" }

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_instancing
			#pragma multi_compile_fwdbase nolightmap nodynlightmap novertexlight
			#pragma shader_feature __ FL_VERTEX_COLOR
			#pragma shader_feature FL_COLORS_WORLD FL_COLORS_LOCAL
			#pragma shader_feature FL_SYMETRIC_COLORS_ON FL_SYMETRIC_COLORS_OFF
			#pragma shader_feature __ FL_GRADIENT_AXIS_ON_X
			#pragma shader_feature __ FL_GRADIENT_AXIS_ON_Y
			#pragma shader_feature __ FL_GRADIENT_AXIS_ON_Z
			#pragma shader_feature __ FL_AMBIENT_LIGHT
			#pragma shader_feature __ FL_GRADIENT_LOCAL FL_GRADIENT_WORLD
			#pragma shader_feature __ FL_DIRECTIONAL_LIGHT
			#pragma shader_feature __ FL_SPOT_LIGHT
			#pragma shader_feature __ FL_POINT_LIGHT
			#pragma shader_feature __ FL_BLEND_LIGHT_SOURCES
			#pragma shader_feature __ FL_RECEIVESHADOWS
			#pragma shader_feature __ FL_LIGHTMAPPING FL_UNITY_LIGHTMAPPING
			#pragma shader_feature __ FL_MAIN_TEXTURE
			#pragma shader_feature __ FL_FOG

			#define FL_UNITY_SHADOWS

			#include "./cginc/FlatLightingCommon.cginc"

			ENDCG
		}

		// Forward additive pass (only needed if you care about more lights than 1 directional).
        // Can remove if no point/spot light support needed.
        Pass
        {
            Tags { "LightMode" = "ForwardAdd" }
            ZWrite Off 
            Blend One One
            BlendOp RevSub

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // Include shadowing support for point/spot
            #pragma multi_compile_fwdadd_fullshadows
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"

            uniform fixed _UnityShadowPower;

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldPos : TEXCOORD0;
                SHADOW_COORDS(1)
            };

            v2f vert (appdata_full v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos (v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                TRANSFER_SHADOW(o); // pass shadow coordinates to pixel shader
                return o;
            }

            fixed4 frag (v2f IN) : SV_Target
            {
            	// compute shadow attenuation (1.0 = fully lit, 0.0 = fully shadowed)
                fixed atten = _UnityShadowPower - SHADOW_ATTENUATION(IN);
                return fixed4(atten, atten, atten, 0);
            }

            ENDCG
        }

        // Support for casting shadows from this shader. Remove if not needed.
        UsePass "VertexLit/SHADOWCASTER"
	}

	CustomEditor "FlatLightingUnityShadowShaderEditor"
}
