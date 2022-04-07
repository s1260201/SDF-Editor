Shader "SDFE/Sample"
{
	Properties
	{

	}
	SubShader
	{
                //衝突しないピクセルは透明
		Tags{ "Queue" = "Transparent" "LightMode"="ForwardBase"}
		LOD 100

		Pass
		{
			ZWrite On
                        //アルファ値が機能するために必要
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 pos : POSITION1;
				float4 vertex : SV_POSITION;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
                                //ローカル→ワールド座標に変換
				o.pos = mul(unity_ObjectToWorld, v.vertex);
				o.uv = v.uv;
				return o;
			}

			// *Sphere
			float sdSphere(float3 p, float r)
			{
				return length(p) - r;
			}

			// *Box
			float sdBox(float3 p, float3 b){
				float3 q = abs(p) - b;
				return length(max(q,0.0)) + min(max(q.x,max(q.y,q.z)),0.0);
			}

			// *RoundBox
			float sdRoundBox(float3 p, float3 b, float r){
				float3 q = abs(p) - b;
				return length(max(q,0.0)) + min(max(q.x,max(q.y,q.z)),0.0) - r;
			}

			float getSdf(float3 pos){
				
				float dist = 0;
float dist0 = sdSphere(float3(pos.x - 0, pos.y -  1, pos.z - 0), 1);
float dist1 = sdSphere(float3(pos.x - 1, pos.y -  0, pos.z - 0), 0.5);
