// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "PebbleShader"
{
	Properties
	{
		_PebbleColorTop("PebbleColorTop", Color) = (0,0,0,0)
		_PebbleColorBottom("PebbleColorBottom", Color) = (0,0,0,0)
		_Color0("Color 0", Color) = (0,0,0,0)
		_Min01("Min01", Float) = 0
		_Min02("Min02", Float) = 0
		_Max01("Max01", Float) = 0
		_Max02("Max02", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Off
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha noshadow 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _PebbleColorTop;
		uniform float4 _PebbleColorBottom;
		uniform float _Min01;
		uniform float _Max01;
		uniform float4 _Color0;
		uniform float _Min02;
		uniform float _Max02;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 lerpResult4 = lerp( _PebbleColorTop , _PebbleColorBottom , (0.0 + (i.uv_texcoord.x - _Min01) * (1.0 - 0.0) / (_Max01 - _Min01)));
			float4 lerpResult6 = lerp( lerpResult4 , _Color0 , (0.0 + (i.uv_texcoord.x - _Min02) * (1.0 - 0.0) / (_Max02 - _Min02)));
			float4 temp_output_18_0 = ( lerpResult6 / 2.0 );
			o.Albedo = temp_output_18_0.rgb;
			o.Emission = temp_output_18_0.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16100
0;509.6;1086;275;318.5657;123.5478;1.696458;True;True
Node;AmplifyShaderEditor.TextureCoordinatesNode;3;-1234.232,274.2413;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;8;-768.9092,41.61677;Float;False;Property;_Min01;Min01;3;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;10;-764.6577,144.7049;Float;False;Property;_Max01;Max01;5;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;1;-865.4829,-372.7143;Float;False;Property;_PebbleColorTop;PebbleColorTop;0;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;2;-872.9588,-201.923;Float;False;Property;_PebbleColorBottom;PebbleColorBottom;1;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;16;-761.983,465.2107;Float;False;Property;_Min02;Min02;4;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-757.7316,568.299;Float;False;Property;_Max02;Max02;6;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;13;-477.7493,72.5416;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;4;-190.61,-171.5238;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TFHCRemapNode;15;-485.7339,263.9481;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;5;-177.8887,6.12889;Float;False;Property;_Color0;Color 0;2;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;6;233.8388,-8.303467;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;19;224.0554,155.5391;Float;False;Constant;_Float1;Float 1;7;0;Create;True;0;0;False;0;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;14;-494.9249,659.4865;Float;False;Constant;_Float0;Float 0;5;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;18;482.6639,4.869088;Float;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;21;775.9626,-47.77193;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;PebbleShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;False;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;13;0;3;1
WireConnection;13;1;8;0
WireConnection;13;2;10;0
WireConnection;4;0;1;0
WireConnection;4;1;2;0
WireConnection;4;2;13;0
WireConnection;15;0;3;1
WireConnection;15;1;16;0
WireConnection;15;2;17;0
WireConnection;6;0;4;0
WireConnection;6;1;5;0
WireConnection;6;2;15;0
WireConnection;18;0;6;0
WireConnection;18;1;19;0
WireConnection;21;0;18;0
WireConnection;21;2;18;0
ASEEND*/
//CHKSM=B67FE9D8DFA43FFDBA32352794956A8503618A9B