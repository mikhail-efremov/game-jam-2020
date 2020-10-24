// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "CustomFx/FxLightning"
{
	Properties
	{
		_Texture("Texture ", 2D) = "white" {}
		_ColorR("ColorR", Color) = (0,0,0,0)
		_ColorG("ColorG", Color) = (0,0,0,0)
		_ColorB("ColorB", Color) = (0,0,0,0)
		_Dissolv("Dissolv", 2D) = "white" {}
		_Displ("Displ", 2D) = "white" {}
		_Smoo("Smoo", Float) = 1
		_SpeedDisp("SpeedDisp", Vector) = (0,0,0,0)
		_TilingDisp("TilingDisp", Vector) = (0.9,0,0,0)
		_PowerDisl("PowerDisl", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] _tex4coord2( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IsEmissive" = "true"  }
		Cull Off
		ZWrite Off
		Blend One One , One OneMinusSrcAlpha
		
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha noshadow nofog 
		#undef TRANSFORM_TEX
		#define TRANSFORM_TEX(tex,name) float4(tex.xy * name##_ST.xy + name##_ST.zw, tex.z, tex.w)
		struct Input
		{
			float2 uv_texcoord;
			float4 uv2_tex4coord2;
			float4 vertexColor : COLOR;
		};

		uniform float4 _ColorR;
		uniform sampler2D _Texture;
		uniform sampler2D _Displ;
		uniform float2 _SpeedDisp;
		uniform float2 _TilingDisp;
		uniform float _PowerDisl;
		uniform float4 _ColorG;
		uniform float4 _ColorB;
		uniform float _Smoo;
		uniform sampler2D _Dissolv;
		uniform float4 _Dissolv_ST;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float4 tex2DNode1 = tex2D( _Texture, ( float4( i.uv_texcoord, 0.0 , 0.0 ) + ( tex2D( _Displ, ( ( i.uv_texcoord + ( _SpeedDisp * _Time.y ) ) + ( i.uv_texcoord * _TilingDisp ) ) ) * _PowerDisl ) ).rg );
			float4 temp_cast_2 = (i.uv2_tex4coord2.x).xxxx;
			float4 temp_cast_3 = (_Smoo).xxxx;
			float2 uv_Dissolv = i.uv_texcoord * _Dissolv_ST.xy + _Dissolv_ST.zw;
			float4 smoothstepResult11 = smoothstep( temp_cast_2 , temp_cast_3 , tex2D( _Dissolv, uv_Dissolv ));
			float4 temp_output_12_0 = ( ( ( _ColorR * tex2DNode1.r * _ColorR.a ) + ( _ColorG * tex2DNode1.g * _ColorG.a ) + ( tex2DNode1.b * _ColorB * _ColorB.a ) ) * saturate( smoothstepResult11 ) * i.vertexColor * i.vertexColor.a );
			o.Emission = temp_output_12_0.rgb;
			o.Alpha = temp_output_12_0.r;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17800
-1675;51;1486;817;591.8451;158.518;1;True;False
Node;AmplifyShaderEditor.Vector2Node;30;-2098.171,-6.15991;Inherit;False;Property;_SpeedDisp;SpeedDisp;9;0;Create;True;0;0;False;0;0,0;0.2,0.3;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleTimeNode;31;-2180.171,124.8401;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;29;-1986.171,189.8401;Inherit;False;Property;_TilingDisp;TilingDisp;10;0;Create;True;0;0;False;0;0.9,0;2,2;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;24;-2074.819,-375.8545;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;-1884.171,18.84012;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;25;-1742.119,-317.9544;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;26;-1773.419,269.5456;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;28;-1558.171,-163.1599;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;34;-1428.171,89.84008;Inherit;False;Property;_PowerDisl;PowerDisl;11;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;23;-1396.218,-321.9545;Inherit;True;Property;_Displ;Displ;6;0;Create;True;0;0;False;0;-1;cd460ee4ac5c1e746b7a734cc7cc64dd;dcae7c2a535e4a449a0abe3c2c0367dd;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;32;-1017.171,-251.1599;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;33;-847.1711,-356.1599;Inherit;False;2;2;0;FLOAT2;0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;6;-592,-389.5;Inherit;False;Property;_ColorR;ColorR;2;0;Create;True;0;0;False;0;0,0,0,0;0.8867924,0.4205589,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-833,-68.5;Inherit;True;Property;_Texture;Texture ;1;0;Create;True;0;0;False;0;-1;61d32c53e39eb2c48af81bdeabfa6651;fff9407b8fdd2634dab5a3fc07666a8c;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;8;-667,138.5;Inherit;False;Property;_ColorB;ColorB;4;0;Create;True;0;0;False;0;0,0,0,0;1,0.509158,0.2971698,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;13;-895.8,889.2001;Inherit;True;1;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;7;-532,-131.5;Inherit;False;Property;_ColorG;ColorG;3;0;Create;True;0;0;False;0;0,0,0,0;1,0.3456931,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;22;-820.5251,728.9264;Inherit;False;Property;_Smoo;Smoo;8;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;10;-1232.9,670.8;Inherit;True;Property;_Dissolv;Dissolv;5;0;Create;True;0;0;False;0;-1;cd460ee4ac5c1e746b7a734cc7cc64dd;9203d87e138705a499c5cb9dfd5fa90b;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;2;-315,-289.5;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;-283,151.5;Inherit;False;3;3;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SmoothstepOpNode;11;-324.8,731.2001;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;3;-274,-69.5;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;16;79.2,652.2001;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;9;25,-23.5;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.VertexColorNode;35;59.15491,216.482;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;14;-585.0001,910.9001;Inherit;False;Property;_Vector0;Vector 0;7;0;Create;True;0;0;False;0;0.31,0.57;0,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;246,-104.5;Inherit;True;4;4;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;657,-150;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;CustomFx/FxLightning;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;False;False;False;False;False;Off;2;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;True;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;4;1;False;-1;1;False;-1;3;1;False;-1;10;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;27;0;30;0
WireConnection;27;1;31;0
WireConnection;25;0;24;0
WireConnection;25;1;27;0
WireConnection;26;0;24;0
WireConnection;26;1;29;0
WireConnection;28;0;25;0
WireConnection;28;1;26;0
WireConnection;23;1;28;0
WireConnection;32;0;23;0
WireConnection;32;1;34;0
WireConnection;33;0;24;0
WireConnection;33;1;32;0
WireConnection;1;1;33;0
WireConnection;2;0;6;0
WireConnection;2;1;1;1
WireConnection;2;2;6;4
WireConnection;4;0;1;3
WireConnection;4;1;8;0
WireConnection;4;2;8;4
WireConnection;11;0;10;0
WireConnection;11;1;13;1
WireConnection;11;2;22;0
WireConnection;3;0;7;0
WireConnection;3;1;1;2
WireConnection;3;2;7;4
WireConnection;16;0;11;0
WireConnection;9;0;2;0
WireConnection;9;1;3;0
WireConnection;9;2;4;0
WireConnection;12;0;9;0
WireConnection;12;1;16;0
WireConnection;12;2;35;0
WireConnection;12;3;35;4
WireConnection;0;2;12;0
WireConnection;0;9;12;0
ASEEND*/
//CHKSM=088D0A615F0C35456F627E54604B4C123EF7EAC0