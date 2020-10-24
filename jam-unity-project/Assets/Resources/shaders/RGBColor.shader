// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "CustomSaders/RGBColor"
{
	Properties
	{
		_ColorRed("ColorRed", Color) = (0,0,0,0)
		_ColorGreen("ColorGreen", Color) = (0,0,0,0)
		_ColorBlue("ColorBlue", Color) = (0,0,0,0)
		_Metalic("Metalic", Float) = 0
		_Smooth("Smooth", Float) = 0
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float4 vertexColor : COLOR;
		};

		uniform float4 _ColorRed;
		uniform float4 _ColorGreen;
		uniform float4 _ColorBlue;
		uniform float _Metalic;
		uniform float _Smooth;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Albedo = ( ( i.vertexColor.r * _ColorRed ) + ( i.vertexColor.g * _ColorGreen ) + ( i.vertexColor.b * _ColorBlue ) ).rgb;
			o.Metallic = _Metalic;
			o.Smoothness = _Smooth;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17800
0;88;1486;817;1184;172.5;1;True;False
Node;AmplifyShaderEditor.VertexColorNode;1;-1077,103;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;5;-859,-13;Inherit;False;Property;_ColorRed;ColorRed;0;0;Create;True;0;0;False;0;0,0,0,0;1,0.1819646,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;6;-860,164;Inherit;False;Property;_ColorGreen;ColorGreen;1;0;Create;True;0;0;False;0;0,0,0,0;0.504717,0.9175266,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;7;-862,334;Inherit;False;Property;_ColorBlue;ColorBlue;2;0;Create;True;0;0;False;0;0,0,0,0;0.1843137,0.1058824,0.1214845,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;2;-603,67;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;3;-601,161;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;-606,253;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;8;-308,140;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;9;-345,253.5;Inherit;False;Property;_Metalic;Metalic;3;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;10;-364,399.5;Inherit;False;Property;_Smooth;Smooth;4;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;-7,147;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;CustomSaders/RGBColor;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;2;0;1;1
WireConnection;2;1;5;0
WireConnection;3;0;1;2
WireConnection;3;1;6;0
WireConnection;4;0;1;3
WireConnection;4;1;7;0
WireConnection;8;0;2;0
WireConnection;8;1;3;0
WireConnection;8;2;4;0
WireConnection;0;0;8;0
WireConnection;0;3;9;0
WireConnection;0;4;10;0
ASEEND*/
//CHKSM=64CFE75070A24436F3D4445FF6B1C81A6A0252BD