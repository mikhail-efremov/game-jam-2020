// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "CustomFx/FxRGBPremult"
{
	Properties
	{
		_Texture("Texture", 2D) = "white" {}
		_ColorR("ColorR", Color) = (0,0,0,0)
		_ColorG("ColorG", Color) = (0,0,0,0)
		_ColorB("ColorB", Color) = (0,0,0,0)
		_BrightnessR("BrightnessR", Float) = 0
		_BrightnessG("BrightnessG", Float) = 0
		_BrightnessB("BrightnessB", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IsEmissive" = "true"  }
		Cull Off
		ZWrite Off
		Blend One OneMinusSrcAlpha , One OneMinusSrcAlpha
		
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha noshadow nofog 
		struct Input
		{
			float4 vertexColor : COLOR;
			float2 uv_texcoord;
		};

		uniform float4 _ColorR;
		uniform sampler2D _Texture;
		uniform float4 _Texture_ST;
		uniform float _BrightnessR;
		uniform float4 _ColorG;
		uniform float _BrightnessG;
		uniform float4 _ColorB;
		uniform float _BrightnessB;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 uv_Texture = i.uv_texcoord * _Texture_ST.xy + _Texture_ST.zw;
			float4 tex2DNode1 = tex2D( _Texture, uv_Texture );
			float4 temp_output_11_0 = ( ( _ColorR * tex2DNode1.r * _BrightnessR * _ColorR.a ) + ( _ColorG * tex2DNode1.g * _BrightnessG * _ColorG.a ) + ( tex2DNode1.b * _ColorB * _BrightnessB * _ColorB.a ) );
			o.Emission = ( i.vertexColor * temp_output_11_0 ).rgb;
			o.Alpha = ( i.vertexColor.a * temp_output_11_0 * tex2DNode1.a ).r;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17800
-1675;51;1486;817;1088.365;897.8936;1.971935;True;False
Node;AmplifyShaderEditor.SamplerNode;1;-1386.886,52.15259;Inherit;True;Property;_Texture;Texture;1;0;Create;True;0;0;False;0;-1;None;c3d2f6f76c817b34bb766f646ab2425c;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;5;-1093.886,-310.8474;Inherit;False;Property;_ColorR;ColorR;2;0;Create;True;0;0;False;0;0,0,0,0;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;8;-1088.886,-138.8474;Inherit;False;Property;_BrightnessR;BrightnessR;5;0;Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;9;-1044.886,-8.847414;Inherit;False;Property;_BrightnessG;BrightnessG;6;0;Create;True;0;0;False;0;0;3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;7;-1009.886,344.1526;Inherit;False;Property;_ColorB;ColorB;4;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;10;-1018.886,259.1526;Inherit;False;Property;_BrightnessB;BrightnessB;7;0;Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;6;-1071.886,41.15258;Inherit;False;Property;_ColorG;ColorG;3;0;Create;True;0;0;False;0;0,0,0,0;1,0.527639,0.06132078,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;2;-769.8856,-138.8474;Inherit;False;4;4;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;3;-686.8856,59.1526;Inherit;False;4;4;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;-704.8856,274.1526;Inherit;False;4;4;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;11;-342.7834,63.46868;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.VertexColorNode;14;-216.6212,-213.5245;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;13;-128,423.5;Inherit;False;Property;_Premult;Premult;8;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;95,-200.5;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;127.3719,181.0858;Inherit;False;3;3;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;369,-80;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;CustomFx/FxRGBPremult;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;False;False;False;False;False;Off;2;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;True;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;3;1;False;-1;10;False;-1;3;1;False;-1;10;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;2;0;5;0
WireConnection;2;1;1;1
WireConnection;2;2;8;0
WireConnection;2;3;5;4
WireConnection;3;0;6;0
WireConnection;3;1;1;2
WireConnection;3;2;9;0
WireConnection;3;3;6;4
WireConnection;4;0;1;3
WireConnection;4;1;7;0
WireConnection;4;2;10;0
WireConnection;4;3;7;4
WireConnection;11;0;2;0
WireConnection;11;1;3;0
WireConnection;11;2;4;0
WireConnection;15;0;14;0
WireConnection;15;1;11;0
WireConnection;12;0;14;4
WireConnection;12;1;11;0
WireConnection;12;2;1;4
WireConnection;0;2;15;0
WireConnection;0;9;12;0
ASEEND*/
//CHKSM=1D2A5FDB218D8845B60539922281DE6025F8DA2D