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
float dist0 = sdSphere(float3(pos.x - 0, pos.y -  0.5, pos.z - 0), 1);
float dist1 = sdSphere(float3(pos.x - 0, pos.y -  -0.5, pos.z - 0), 1);
dist = min(dist0,dist1);
				return dist;
			}

			float4 rayMarch(float3 pos, float3 rayDir, int StepNum){
				int fase = 0;
				float t = 0;
				float d = getSdf(pos);
				float3 col = 0;

				while(fase < StepNum && abs(d) > 0.001){
					t += d;
					pos += rayDir * d;
					d = getSdf(pos);
					fase++;
				}
				if(step(StepNum,fase)){
					return float4(1,1,1,0);
				}else{
					return float4(col,1);
				}				
			}
			

			fixed4 frag(v2f i) : SV_Target
			{
				float3 col = float3(1,1,1);
				// レイの初期位置(ピクセルのワールド座標)
				float3 pos = i.pos.xyz;
				// レイの進行方向
				float3 rayDir = normalize(pos.xyz - _WorldSpaceCameraPos);

				int StepNum = 30;
				return rayMarch(pos,rayDir,StepNum);
			}
			ENDCG
		}
	}
}
