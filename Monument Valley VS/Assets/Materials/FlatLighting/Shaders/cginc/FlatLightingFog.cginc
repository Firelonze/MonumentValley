#ifndef FLAT_LIGHTING_FOG_INCLUDED
#define FLAT_LIGHTING_FOG_INCLUDED

#if defined(FL_FOG) 
	uniform half4 _FogColor;
	uniform half _FogBlendingWidth;
	uniform half _FogCameraOffset;

	inline fixed4 CustomFog(half4 vertex, fixed4 flatLightingColor) {

		half4 worldVertex = vertex;
		#if defined(FL_COLORS_LOCAL)
			worldVertex = mul(unity_ObjectToWorld, vertex);
		#endif
		
		half dist = distance(_WorldSpaceCameraPos, worldVertex);
		// Built-in fog starts at near plane, so match that by
		// subtracting the near value. Not a perfect approximation
		// if near plane is very large, but good enough.
		dist -= _ProjectionParams.y;

		half fogMin = _FogCameraOffset - _FogBlendingWidth;
		half fogMax = _FogCameraOffset + _FogBlendingWidth;
		half fog = smoothstep(fogMin, fogMax, dist);

		half4 fogColor = _FogColor.a * _FogColor + (1 - _FogColor.a) * flatLightingColor;
		return lerp(flatLightingColor, fogColor, fog);
	}
#endif

#endif // FLAT_LIGHTING_FOG_INCLUDED