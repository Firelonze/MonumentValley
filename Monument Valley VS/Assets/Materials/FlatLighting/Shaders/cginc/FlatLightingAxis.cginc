#ifndef FLAT_LIGHTING_AXIS_INCLUDED
#define FLAT_LIGHTING_AXIS_INCLUDED

static const fixed3 _POSITIVE_X = fixed3(1,0,0);
static const fixed3 _NEGATIVE_X = fixed3(-1,0,0);
static const fixed3 _NEGATIVE_Y = fixed3(0,-1,0);
static const fixed3 _POSITIVE_Y = fixed3(0,1,0);
static const fixed3 _NEGATIVE_Z = fixed3(0,0,-1);
static const fixed3 _POSITIVE_Z = fixed3(0,0,1);	

UNITY_INSTANCING_BUFFER_START(Props_axis_light)
	UNITY_DEFINE_INSTANCED_PROP(fixed4, _LightPositiveX)
	UNITY_DEFINE_INSTANCED_PROP(fixed4, _LightNegativeX)
	UNITY_DEFINE_INSTANCED_PROP(fixed4, _LightNegativeZ)
	UNITY_DEFINE_INSTANCED_PROP(fixed4, _LightPositiveZ)
	UNITY_DEFINE_INSTANCED_PROP(fixed4, _LightNegativeY)
	UNITY_DEFINE_INSTANCED_PROP(fixed4, _LightPositiveY)
	UNITY_DEFINE_INSTANCED_PROP(fixed, _LightAndTextureMix)
UNITY_INSTANCING_BUFFER_END(Props_axis_light)


inline fixed4 mix_tree_axis_color(half3 normal, fixed4 ColorX, fixed4 ColorY, fixed4 ColorZ) {
	half3 blendWeights = abs(normalize(normal));
	blendWeights = saturate(blendWeights / (blendWeights.x + blendWeights.y + blendWeights.z));
	return (ColorX * blendWeights.x) + (ColorY * blendWeights.y) + (ColorZ * blendWeights.z);
}

inline fixed4 mix_six_axis_color(half3 normal, 
	fixed4 ColorPositiveX, 
	fixed4 ColorPositiveY, 
	fixed4 ColorPositiveZ,
	fixed4 ColorNegativeX, 
	fixed4 ColorNegativeY,
	fixed4 ColorNegativeZ) {

	half3 blendWeights = normalize(normal);
	blendWeights = (blendWeights / (abs(blendWeights.x) + abs(blendWeights.y) + abs(blendWeights.z)));
	return (ColorPositiveX * max(0, blendWeights.x)) + 
			(ColorPositiveY * max(0, blendWeights.y)) + 
			(ColorPositiveZ * max(0, blendWeights.z)) +
			(ColorNegativeX * abs(min(0, blendWeights.x))) + 
			(ColorNegativeY * abs(min(0, blendWeights.y))) + 
			(ColorNegativeZ * abs(min(0, blendWeights.z)));
}

#if defined (FL_GRADIENT_AXIS_ON_X)
UNITY_INSTANCING_BUFFER_START(Props_axis_gradient_lightX)
	UNITY_DEFINE_INSTANCED_PROP(fixed4, _LightPositive2X)
	UNITY_DEFINE_INSTANCED_PROP(half, _GradientOriginOffsetPositiveX)
	UNITY_DEFINE_INSTANCED_PROP(half, _GradientWidthPositiveX)

	#if defined(FL_SYMETRIC_COLORS_OFF)
		UNITY_DEFINE_INSTANCED_PROP(fixed4, _LightNegative2X)
		UNITY_DEFINE_INSTANCED_PROP(half, _GradientOriginOffsetNegativeX)
		UNITY_DEFINE_INSTANCED_PROP(half, _GradientWidthNegativeX)
	#endif
UNITY_INSTANCING_BUFFER_END(Props_axis_gradient_lightX)
#endif

#if defined (FL_GRADIENT_AXIS_ON_Y)
UNITY_INSTANCING_BUFFER_START(Props_axis_gradient_lightY)
	UNITY_DEFINE_INSTANCED_PROP(fixed4, _LightPositive2Y)
	UNITY_DEFINE_INSTANCED_PROP(half, _GradientOriginOffsetPositiveY)
	UNITY_DEFINE_INSTANCED_PROP(half, _GradientWidthPositiveY)

	#if defined(FL_SYMETRIC_COLORS_OFF)
		UNITY_DEFINE_INSTANCED_PROP(fixed4, _LightNegative2Y)
		UNITY_DEFINE_INSTANCED_PROP(half, _GradientOriginOffsetNegativeY)
		UNITY_DEFINE_INSTANCED_PROP(half, _GradientWidthNegativeY)
	#endif
UNITY_INSTANCING_BUFFER_END(Props_axis_gradient_lightY)
#endif

#if defined (FL_GRADIENT_AXIS_ON_Z)
UNITY_INSTANCING_BUFFER_START(Props_axis_gradient_lightZ)
	UNITY_DEFINE_INSTANCED_PROP(fixed4, _LightPositive2Z)
	UNITY_DEFINE_INSTANCED_PROP(half, _GradientOriginOffsetPositiveZ)
	UNITY_DEFINE_INSTANCED_PROP(half, _GradientWidthPositiveZ)

	#if defined(FL_SYMETRIC_COLORS_OFF)
		UNITY_DEFINE_INSTANCED_PROP(fixed4, _LightNegative2Z)
		UNITY_DEFINE_INSTANCED_PROP(half, _GradientOriginOffsetNegativeZ)
		UNITY_DEFINE_INSTANCED_PROP(half, _GradientWidthNegativeZ)
	#endif
UNITY_INSTANCING_BUFFER_END(Props_axis_gradient_lightZ)
#endif

#if defined(FL_GRADIENT_AXIS_ON_X) || defined(FL_GRADIENT_AXIS_ON_Y) || defined(FL_GRADIENT_AXIS_ON_Z)
	inline fixed4 gradient_axis_color(
		fixed3 axisGradient,
		half3 vertexNormal,
		half4 vertexGradientPosition,
		half4 gradientCenterPosition,
		fixed4 color1, 
		fixed4 color2,
		half gradientOffset, 
		half gradientWidth) {

		half gradientCenter = length(axisGradient * gradientCenterPosition) + gradientOffset;
		half minGradient = gradientCenter - gradientWidth;
		half maxGradient = gradientCenter + gradientWidth;
		fixed gradientInfluence = dot(axisGradient, vertexGradientPosition);
		fixed gradient = smoothstep(minGradient, maxGradient, gradientInfluence);
		fixed4 gradientColor = lerp(color2, color1, gradient);

		return gradientColor;
	}
#endif

inline fixed4 fl_axis_light(half3 normal, half4 vertex) {
	fixed4 flatLightColor = fixed4(0,0,0,0);

	//Local by default
	half4 centerGradientPosition = half4(0.0, 0.0, 0.0, 0.0);
	#if (defined(FL_GRADIENT_AXIS_ON_X) || defined(FL_GRADIENT_AXIS_ON_Y) || defined(FL_GRADIENT_AXIS_ON_Z)) && defined(FL_COLORS_WORLD)
		centerGradientPosition = mul(unity_ObjectToWorld, half4(0.0, 0.0, 0.0, 0.0));
	#endif

		fixed4 positiveX = UNITY_ACCESS_INSTANCED_PROP(Props_axis_light, _LightPositiveX);
		fixed4 positiveZ = UNITY_ACCESS_INSTANCED_PROP(Props_axis_light, _LightPositiveZ);
		fixed4 positiveY = UNITY_ACCESS_INSTANCED_PROP(Props_axis_light, _LightPositiveY);


	#if defined(FL_SYMETRIC_COLORS_OFF)
		fixed4 ColorPositiveX, ColorNegativeX;
		fixed4 ColorPositiveY, ColorNegativeY;
		fixed4 ColorPositiveZ, ColorNegativeZ;

		fixed4 negativeX = UNITY_ACCESS_INSTANCED_PROP(Props_axis_light, _LightNegativeX);
		fixed4 negativeZ = UNITY_ACCESS_INSTANCED_PROP(Props_axis_light, _LightNegativeZ);
		fixed4 negativeY = UNITY_ACCESS_INSTANCED_PROP(Props_axis_light, _LightNegativeY);

		#if defined(FL_GRADIENT_AXIS_ON_X)
			fixed4 positiveX2 = UNITY_ACCESS_INSTANCED_PROP(Props_axis_gradient_lightX, _LightPositive2X);
			half positiveXGradientOrigin = UNITY_ACCESS_INSTANCED_PROP(Props_axis_gradient_lightX, _GradientOriginOffsetPositiveX);
			half positiveXGradientWidth = UNITY_ACCESS_INSTANCED_PROP(Props_axis_gradient_lightX, _GradientWidthPositiveX);

			fixed4 negativeX2 = UNITY_ACCESS_INSTANCED_PROP(Props_axis_gradient_lightX, _LightNegative2X);
			half negativeXGradientOrigin = UNITY_ACCESS_INSTANCED_PROP(Props_axis_gradient_lightX, _GradientOriginOffsetNegativeX);
			half negativeXGradientWidth = UNITY_ACCESS_INSTANCED_PROP(Props_axis_gradient_lightX, _GradientWidthNegativeX);

			ColorPositiveX = gradient_axis_color(_POSITIVE_Y, normal, vertex, centerGradientPosition,
				positiveX, positiveX2, positiveXGradientOrigin, positiveXGradientWidth);
			ColorNegativeX = gradient_axis_color(_POSITIVE_Y, normal, vertex, centerGradientPosition, 
				negativeX, negativeX2, negativeXGradientOrigin, negativeXGradientWidth);
		#else
			ColorPositiveX = positiveX;
			ColorNegativeX = negativeX;
		#endif

		#if defined(FL_GRADIENT_AXIS_ON_Y)
			fixed4 positiveY2 = UNITY_ACCESS_INSTANCED_PROP(Props_axis_gradient_lightY, _LightPositive2Y);
			half positiveYGradientOrigin = UNITY_ACCESS_INSTANCED_PROP(Props_axis_gradient_lightY, _GradientOriginOffsetPositiveY);
			half positiveYGradientWidth = UNITY_ACCESS_INSTANCED_PROP(Props_axis_gradient_lightY, _GradientWidthPositiveY);

			fixed4 negativeY2 = UNITY_ACCESS_INSTANCED_PROP(Props_axis_gradient_lightY, _LightNegative2Y);
			half negativeYGradientOrigin = UNITY_ACCESS_INSTANCED_PROP(Props_axis_gradient_lightY, _GradientOriginOffsetNegativeY);
			half negativeYGradientWidth = UNITY_ACCESS_INSTANCED_PROP(Props_axis_gradient_lightY, _GradientWidthNegativeY);

			ColorPositiveY = gradient_axis_color(_POSITIVE_Z, normal, vertex, centerGradientPosition, 
				positiveY, positiveY2, positiveYGradientOrigin, positiveYGradientWidth);
			ColorNegativeY = gradient_axis_color(_POSITIVE_Z, normal, vertex, centerGradientPosition, 
				negativeY, negativeY2, negativeYGradientOrigin, negativeYGradientWidth);
		#else
			ColorPositiveY = positiveY;
			ColorNegativeY = negativeY;
		#endif

		#if defined(FL_GRADIENT_AXIS_ON_Z)
			fixed4 positiveZ2 = UNITY_ACCESS_INSTANCED_PROP(Props_axis_gradient_lightZ, _LightPositive2Z);
			half positiveZGradientOrigin = UNITY_ACCESS_INSTANCED_PROP(Props_axis_gradient_lightZ, _GradientOriginOffsetPositiveZ);
			half positiveZGradientWidth = UNITY_ACCESS_INSTANCED_PROP(Props_axis_gradient_lightZ, _GradientWidthPositiveZ);

			fixed4 negativeZ2 = UNITY_ACCESS_INSTANCED_PROP(Props_axis_gradient_lightZ, _LightNegative2Z);
			half negativeZGradientOrigin = UNITY_ACCESS_INSTANCED_PROP(Props_axis_gradient_lightZ, _GradientOriginOffsetNegativeZ);
			half negativeZGradientWidth = UNITY_ACCESS_INSTANCED_PROP(Props_axis_gradient_lightZ, _GradientWidthNegativeZ);

			ColorPositiveZ = gradient_axis_color(_POSITIVE_Y, normal, vertex, centerGradientPosition, 
				positiveZ, positiveZ2, positiveZGradientOrigin, positiveZGradientWidth);
			ColorNegativeZ = gradient_axis_color(_POSITIVE_Y, normal, vertex, centerGradientPosition, 
				negativeZ, negativeZ2, negativeZGradientOrigin, negativeZGradientWidth);
		#else
			ColorPositiveZ = positiveZ;
			ColorNegativeZ = negativeZ;
		#endif

		flatLightColor = mix_six_axis_color(normal, ColorPositiveX, ColorPositiveY, ColorPositiveZ,
													ColorNegativeX, ColorNegativeY, ColorNegativeZ);
	#else
		fixed4 ColorPositiveX;
		fixed4 ColorPositiveY;
		fixed4 ColorPositiveZ;

		#if defined(FL_GRADIENT_AXIS_ON_X)
			fixed4 positiveX2 = UNITY_ACCESS_INSTANCED_PROP(Props_axis_gradient_lightX, _LightPositive2X);
			half positiveXGradientOrigin = UNITY_ACCESS_INSTANCED_PROP(Props_axis_gradient_lightX, _GradientOriginOffsetPositiveX);
			half positiveXGradientWidth = UNITY_ACCESS_INSTANCED_PROP(Props_axis_gradient_lightX, _GradientWidthPositiveX);

			ColorPositiveX = gradient_axis_color(_POSITIVE_Y, normal, vertex, centerGradientPosition,
				positiveX, positiveX2, positiveXGradientOrigin, positiveXGradientWidth);
		#else
			ColorPositiveX = positiveX;
		#endif

		#if defined(FL_GRADIENT_AXIS_ON_Y)
			fixed4 positiveY2 = UNITY_ACCESS_INSTANCED_PROP(Props_axis_gradient_lightY, _LightPositive2Y);
			half positiveYGradientOrigin = UNITY_ACCESS_INSTANCED_PROP(Props_axis_gradient_lightY, _GradientOriginOffsetPositiveY);
			half positiveYGradientWidth = UNITY_ACCESS_INSTANCED_PROP(Props_axis_gradient_lightY, _GradientWidthPositiveY);

			ColorPositiveY = gradient_axis_color(_POSITIVE_Z, normal, vertex, centerGradientPosition,
				positiveY, positiveY2, positiveYGradientOrigin, positiveYGradientWidth);
		#else
			ColorPositiveY = positiveY;
		#endif

		#if defined(FL_GRADIENT_AXIS_ON_Z)
			fixed4 positiveZ2 = UNITY_ACCESS_INSTANCED_PROP(Props_axis_gradient_lightZ, _LightPositive2Z);
			half positiveZGradientOrigin = UNITY_ACCESS_INSTANCED_PROP(Props_axis_gradient_lightZ, _GradientOriginOffsetPositiveZ);
			half positiveZGradientWidth = UNITY_ACCESS_INSTANCED_PROP(Props_axis_gradient_lightZ, _GradientWidthPositiveZ);

			ColorPositiveZ = gradient_axis_color(_POSITIVE_Y, normal, vertex, centerGradientPosition,
				positiveZ, positiveZ2, positiveZGradientOrigin, positiveZGradientWidth);
		#else
			ColorPositiveZ = positiveZ;
		#endif

		flatLightColor = mix_tree_axis_color(normal, ColorPositiveX, ColorPositiveY, ColorPositiveZ);
	#endif

	return flatLightColor;
}

UNITY_DECLARE_TEX2D(_MainTex);

inline fixed4 fl_axis_apply_texture(fixed4 flatLight, half2 uv) {
       fixed4 mainTexture = UNITY_SAMPLE_TEX2D(_MainTex, uv);
       return lerp(flatLight, mainTexture, UNITY_ACCESS_INSTANCED_PROP(Props_axis_light, _LightAndTextureMix));
}

#endif // FLAT_LIGHTING_AXIS_INCLUDED